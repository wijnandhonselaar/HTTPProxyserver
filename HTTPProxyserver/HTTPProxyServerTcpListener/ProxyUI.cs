using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTPProxyServerTcpListener
{
    public partial class ProxyUi : Form
    {
        private bool _listening;
        private TcpListener _server;
        private delegate void MessageHandler(string message);
        private CacheService _cs = new CacheService();
        private MapperService _ms = new MapperService();
        private HelperService _hs = new HelperService();
        private CommuncationService _comService = new CommuncationService();

        public ProxyUi()
        {
            InitializeComponent();
        }

        private void btnToggleProxy_Click(object sender, EventArgs e)
        {
            if (!_listening) Listener();
            else Stop();
        }

        /// <summary>
        /// Print message from any thread to the GUI
        /// </summary>
        /// <param name="message"></param>
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

        /// <summary>
        /// Stop the listener
        /// </summary>
        private void Stop()
        {
            _listening = false;
            _server.Stop();
        }

        /// <summary>
        /// Starts the listener and runs until stopped
        /// Listens for incoming requests from the client (browser(s))
        /// </summary>
        private async void Listener()
        {
            _listening = true;
            var port = int.Parse(txtPort.Text);
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();
            PrintMessage("Listener has been started");
            while (_listening)
            {
                try
                {
                    var client = await _server.AcceptTcpClientAsync();
                    // By using a ... expression, the parameter doesn't have to be of type Object
                    // Process every request to be able to handle multiple at once
                    Task.Run(() => ProcessRequest(client));
                }
                catch (ObjectDisposedException e)
                {
                    PrintMessage("Listener has been stopped successfully");
                }
            }
        }

        /// <summary>
        /// Is fired when the listener receives a request.
        /// Process request inits the entire run of creating, manipulating the request 
        /// and getting the corrisponding response
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private async Task ProcessRequest(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                if (client.Connected && stream.DataAvailable)
                {
                    int bufferSize;
                    if (!int.TryParse(txtBufferSize.Text, out bufferSize))
                        bufferSize = 2048;

                    var buffer = new byte[bufferSize];
                    await stream.ReadAsync(buffer, 0, bufferSize);
                    var context = Encoding.UTF8.GetString(buffer);

                    // Log request headers if enabled
                    if (cbLogReqHeaders.Checked) LogHeaders(context);

                    var request = _ms.ToHead(context);
                    byte[] response = { };
                    if (cbChangedContent.Checked || (!request.ContainsKey("Cache-Control") || request["Cache-Control"] != "no-cache" || request["Cache-Control"] != "max-age=0")
                        && !_cs.IsCached(request, out response))
                    {
                        if (cbContentFilter.Checked && _hs.IsImage(request["Url"]))
                            request["Url"] = "http://reactiongifs.me/wp-content/uploads/2013/08/office-dwight-mad.gif";

                        response = await GetWebResponse(request, stream);
                    }
                    else
                    {
                        PrintMessage("Got: " + request["Url"] + " from cache.");
                    }
                    if (response != null) _comService.SendResponse(response, stream);
                }
            }
            client.Close();
        }
        /// <summary>
        /// Get webResponse sets header options on the webRequest and returns the response from the webRequest.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<byte[]> GetWebResponse(Dictionary<string, string> request, NetworkStream stream)
        {
            var response = new byte[] {};
            var webRequest = WebRequest.Create(request["Url"]) as HttpWebRequest;
            if (webRequest == null)
                return null;
            // Set request headers
            webRequest.Method = request["Method"];
            webRequest.Accept = request["Accept"];
            webRequest.UserAgent = request["User-Agent"];

            // if POST, set headers about the content of the request
            if (webRequest.Method == "POST")
            {
                webRequest.ContentType = request["Content-Type"];
                webRequest.ContentLength = int.Parse(request["Content-Length"]);
                // if there is data, add the body to the request.
                //if (request.ContainsKey("body"))
                //{
                //    using (var s = webRequest.GetRequestStream())
                //    {
                //        var b = Encoding.UTF8.GetBytes(request["body"]);
                //        s.Write(b, 0, b.Length);
                //    }
                //}
            }

            // Hide User data (UserAgent & Host and require login
            if (cbChangeHeaders.Checked) _hs.HideMe(webRequest, request);
            var webResponse = await webRequest.GetResponseAsync() as HttpWebResponse;
            if (webResponse == null)
                return null;

            // if request Content-Type is Audo or Video, pass the whole stream.
            // Otherwise handle the response manually
            if (!_hs.HackRequest(webResponse, stream))
            {
                response = GetResponse(webResponse);
            }
            return response;
        }

        /// <summary>
        /// Appends head and content (body) and return the complete request as a byte[]
        /// </summary>
        /// <param name="webResponse"></param>
        /// <returns>response</returns>
        private byte[] GetResponse(HttpWebResponse webResponse)
        {
            var head = _ms.GetHead(webResponse);

            // Log Response headers if enabled
            if (cbLogRespHeaders.Checked) LogHeaders(head);
            var responseBody = _comService.GetResponseBody(webResponse, head);

            // Log content if enabled
            if(cbLogContentIn.Checked) PrintMessage(Encoding.UTF8.GetString(responseBody));

            // Get max cache age from interface
            var maxAge = int.Parse(txtCache.Text);
            _cs.AddToCache(webResponse, head, responseBody, maxAge);
            return _ms.MapToResponse(head, responseBody);
        }

        /// <summary>
        /// Receivces the head as a string
        /// Logs each line from the head
        /// </summary>
        /// <param name="head"></param>
        private void LogHeaders(string head)
        {
            var lines = _ms.RequestToList(head);
            foreach(var line in lines)
                PrintMessage(line);
        }

        /// <summary>
        /// Clears the GUI log (lstLog)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        /// <summary>
        /// Clears the cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearCache_Click(object sender, EventArgs e)
        {
            _cs.ClearCache();
            PrintMessage("###################################################################");
            PrintMessage("######################## Cache is cleared. ########################");
            PrintMessage("###################################################################");
        }
    }
}