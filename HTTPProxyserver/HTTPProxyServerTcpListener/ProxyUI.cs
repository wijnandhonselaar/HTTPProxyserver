using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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
        private ConcurrentDictionary<MD5, CacheItem> _cache = new ConcurrentDictionary<MD5, CacheItem>();

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
                string context = null;
                var bytes = stream.Read(buffer, 0, buffer.Length);
                while (bytes > 0)
                {
                    context += Encoding.ASCII.GetString(buffer);
                    Array.Clear(buffer, 0, buffer.Length);
                    buffer = new byte[bufferSize];
                    bytes = stream.Read(buffer, 0, buffer.Length);
                }
                if (context != null)
                    try
                    {
                        var request = new Request(context);
                        PrintMessage(request.Method);
                        PrintMessage(request.Url);
                        var webRequest = WebRequest.Create(request.Url) as HttpWebRequest;
                        if (webRequest == null) return;
                        AddHeaders(webRequest, request);
                        var response = await webRequest.GetResponseAsync() as HttpWebResponse;
                        if (response == null) return;
                        SendResponse(response, stream);
                        stream.Close();
                        client.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
            }
        }

        private void AddHeaders(HttpWebRequest webRequest, Request request)
        {
            webRequest.Accept = request.Accept;
//            webRequest.Connection = request.Con;
            webRequest.UserAgent = request.UserAgent;
        }

        private bool AcceptRequest(string contentType)
        {
            var invalid = new List<string> { "image", "video", "audio" };
            return invalid.All(x => !contentType.Contains(x));
        }

        private void SendResponse(HttpWebResponse httpRes, Stream stream)
        {
            if (AcceptRequest(httpRes.ContentType) && httpRes.Method == "GET")
            {
                var resStream = httpRes.GetResponseStream();
                if (resStream == null) return;
                var reader = new StreamReader(resStream);
                var responseBody = reader.ReadToEnd();
                var response = MapToResponse(httpRes, responseBody);
                var buffer = Encoding.ASCII.GetBytes(response);
                stream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                httpRes.GetResponseStream().CopyTo(stream);
            }
        }

        private string ToHeaderProperty(string name, string value)
        {
            return name + ": " + value + "\r\n";
        }

        private CacheItem CreateCache(HttpWebRequest req)
        {
            var resp = req?.GetResponse() as HttpWebResponse;
            if (resp == null) return null;

            var body = "";
            var stream = resp.GetResponseStream();
            if (stream == null) return null;
            using (var reader = new StreamReader(stream))
            {
                body = reader.ReadToEnd();
            }
            return new CacheItem(resp, body);
        }

        private string MapToResponse(HttpWebResponse httpRes, string body)
        {
            var response = "";
            // status line
            response += "HTTP/" + httpRes.ProtocolVersion + " " + httpRes.StatusCode.GetHashCode() + " " +
                        httpRes.StatusDescription + "\r\n";
            // Date
            response += ToHeaderProperty("Date", httpRes.Headers["Date"]);
            // Server
            response += ToHeaderProperty("Server", httpRes.Server);
            // Content-Type
            response += ToHeaderProperty("Content-Type", httpRes.ContentType);
            // Content-Length
            response += ToHeaderProperty("Content-Length", httpRes.ContentLength.ToString());
            // Connection
            response += ToHeaderProperty("Connection", "keep-alive");
            // Keep-Alive
            response += ToHeaderProperty("Keep-Alive", "");
            // Body
            response += "\r\n";
            response += body;
            return response;
        }
    }
}