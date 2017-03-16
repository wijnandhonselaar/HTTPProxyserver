using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTPProxyServerTcpListener
{
    public partial class ProxyUI : Form
    {
        private bool _listening;
        private TcpListener _server;
        private delegate void MessageHandler(string message);
        private ConcurrentDictionary<string, CacheItem> _cache = new ConcurrentDictionary<string, CacheItem>();

        public ProxyUI()
        {
            InitializeComponent();
        }

        private void btnToggleProxy_Click(object sender, EventArgs e)
        {
            if (!_listening) Listener();
            else Stop();
        }

        private void PrintMessage(string message)
        {
            if (lstLog.InvokeRequired)
            {
                var d = new MessageHandler(PrintMessage);
                Invoke(d, message);
            }
            else
            {
                lstLog.Items.Add(message);
                lstLog.TopIndex = lstLog.Items.Count - 1;
            }
        }

        private void Stop()
        {
            _listening = false;
            _server.Stop();
        }

        private async void Listener()
        {
            _listening = true;
            var port = int.Parse(txtPort.Text);
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();
            PrintMessage("Listener has been started");
            while (_listening)
                try
                {
                    var client = await _server.AcceptTcpClientAsync();
                    Task.Run(() => ProcessRequest(client));
                }
                catch (ObjectDisposedException e)
                {
                    PrintMessage("Listener has been stopped successfully");
                }
        }

        private async Task ProcessRequest(TcpClient client)
        {
            var stream = client.GetStream();
            // Send Partial or whole URI
            // Keep connection open if a lot of request go to the same base URI
            // Check MD5-hash of response content
            // HTTP: The Definitive Guide
            if (client.Connected && stream.DataAvailable)
            {
                int bufferSize;
                if(!int.TryParse(txtBufferSize.Text, out bufferSize))
                    bufferSize = 2048;

                var buffer = new byte[bufferSize];
                await stream.ReadAsync(buffer, 0, bufferSize);
                var context = Encoding.UTF8.GetString(buffer);
                if (context != null)
                {
                    if (cbLogReqHeaders.Checked) LogHeaders(context);
                    var request = ToHead(context);
                    HttpWebResponse webResponse = null;
                    var response = "";
                    if (!IsCached(request, out response))
                    {
                        if (cbContentFilter.Checked && IsImage(request["Url"]))
                            request["Url"] = "http://reactiongifs.me/wp-content/uploads/2013/08/office-dwight-mad.gif";
                        var webRequest = WebRequest.Create(request["Url"]) as HttpWebRequest;
                        if (webRequest == null)
                            return;
                        //                        AddHeaders(webRequest, request);
                        webResponse = await webRequest.GetResponseAsync() as HttpWebResponse;
                        if (webResponse == null)
                            return;
                        if (!HackRequest(webResponse, stream))
                        {
                            response = GetResponse(webResponse, stream);
                        }
                    }
                    if(response != null) SendResponse(response, stream);
                    stream.Close();
                    client.Close();
                }
            }
        }

        private bool AcceptRequest(string contentType)
        {
            var invalid = new List<string> { "image", "video", "audio" };
            return invalid.All(x => !contentType.Contains(x));
        }

        private bool HackRequest(HttpWebResponse webResponse, NetworkStream stream)
        {
            if (webResponse.Method == "GET") return false;
            var responseStream = webResponse.GetResponseStream();
            responseStream?.CopyTo(stream);
            return true;
        }

        private string GetResponse(HttpWebResponse httpRes, NetworkStream stream)
        {
            byte[] responseBody;
            var head = GetHead(httpRes);
            var resStream = httpRes.GetResponseStream();
            if (resStream == null) return null;
            if (new[] {"image", "video"}.Any(c => httpRes.ContentType.Contains(c)))
            {
                head = null;
                var reader = new BinaryReader(resStream);
                responseBody = reader.ReadBytes((int) httpRes.ContentLength);
            }
            else
            {
                var reader = new StreamReader(resStream);
                responseBody = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            }
            AddToCache(httpRes, head, responseBody);
            return MapToResponse(head, responseBody);
        }

        private void SendResponse(string response, Stream stream)
        {
            var buffer = Encoding.UTF8.GetBytes(response);
            stream.Write(buffer, 0, buffer.Length);
        }

        private bool IsCached(Dictionary<string, string> request, out string response)
        {
            response = "";
            if (!_cache.ContainsKey(request["Url"])) return false;
            CacheItem cached;
            _cache.TryGetValue(request["Url"], out cached);
            if ((DateTime.Now - cached.Date).TotalSeconds > cached.MaxAge) return false;
            response = cached.Head + cached.Body;
            PrintMessage("Got: " + request["Url"] + " from cache.");
            PrintMessage(response);
            
            return true;
        }

        private Dictionary<string, string> ToHead(string context)
        {
            var request = new Dictionary<string, string>();
            var lines = Request.RequestToList(context);
            for (var i = 0; i < lines.Count; i++)
            {
                if (i > 0)
                {
                    if (lines[i].Contains(':'))
                    {
                        request.Add(lines[i].Split(':')[0], lines[i].Split(':')[1].Trim());
                    }
                    else if(lines[i] == "\r\n")
                    {
                        var body = "";
                        for (var y = i+1; y < lines.Count; y++)
                        {
                            body += lines[y];
                        }
                        request.Add("Body", body);
                        i = lines.Count;
                    }
                }
                else
                {
                    var statusLine = lines[i].Split(' ');
                    request.Add("Method", statusLine[0]);
                    request.Add("Url", statusLine[1]);
                    request.Add("HttpVersion", statusLine[2]);
                }
            }
            return request;
        }

        private bool IsImage(string url)
        {
            var options = new[] {"jpg", "png", "jpeg"};
            foreach (var x in options)
            {
                if (url.Contains(x)) return true;
            }
            return false;
        }

        private void AddHeaders(HttpWebRequest webRequest, Dictionary<string, string> request)
        {
            webRequest.Accept = request["Accept"];
//            webRequest.Connection = request.Con;
            webRequest.UserAgent = request["User-Agent"];
        }

        private string ToHeaderProperty(string name, string value)
        {
            return name + ": " + value + "\r\n";
        }

        private string MapToResponse(string head, object body)
        {
            var response = head;
            response += "\r\n";
            response += body;
            return response;
        }

        private string GetHead(HttpWebResponse httpRes)
        {
            var head = "";
            // status line
            head += "HTTP/" + httpRes.ProtocolVersion + " " + httpRes.StatusCode.GetHashCode() + " " +
                        httpRes.StatusDescription + "\r\n";
            // Date
            head += ToHeaderProperty("Date", httpRes.Headers["Date"]);
            // Server
            head += ToHeaderProperty("Server", httpRes.Server);
            // Content-Type
            head += ToHeaderProperty("Content-Type", httpRes.ContentType);
            // Content-Length
            head += ToHeaderProperty("Content-Length", httpRes.ContentLength.ToString());
            if (cbLogRespHeaders.Checked)
                LogHeaders(head);
            return head;
        }

        private void LogHeaders(string head)
        {
            var lines = Request.RequestToList(head);
            foreach(var line in lines)
                PrintMessage(line);
        }

        private void AddToCache(HttpWebResponse httpRes, string head, byte[] content)
        {
            var maxAge = int.Parse(txtCache.Text);
            var type = httpRes.ContentType;
            var date = DateTime.Now;
            var item = new CacheItem(maxAge, type, date, head, content);
            _cache.AddOrUpdate(httpRes.ResponseUri.AbsoluteUri, item, (key, oldValue) => item);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }
    }
}