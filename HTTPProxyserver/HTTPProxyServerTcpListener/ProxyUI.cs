using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Pipes;
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
        private delegate void LogHeaderHandler(string head);
        private CacheService cs = new CacheService();
        private MapperService ms = new MapperService();
        private HelperService hs = new HelperService();
        private CommuncationService comService = new CommuncationService();

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
            {
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
        }

        private async Task ProcessRequest(TcpClient client)
        {
            var stream = client.GetStream();
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
                    var request = ms.ToHead(context);
                    byte[] response = {};
                    if ((!request.ContainsKey("Cache-Control") || request["Cache-Control"] != "no-cache") && !cs.IsCached(request, out response))
                    {
                        if (cbContentFilter.Checked && hs.IsImage(request["Content-Type"]))
                            request["Url"] = "http://reactiongifs.me/wp-content/uploads/2013/08/office-dwight-mad.gif";

                        response = await GetWebResponse(request, stream);
                    }
                    else
                    {
                        PrintMessage("Got: " + request["Url"] + " from cache.");
                    }
                    if(response != null) comService.SendResponse(response, stream);
                    stream.Close();
                    client.Close();
                }
            }
        }

        public async Task<byte[]> GetWebResponse(Dictionary<string, string> request, NetworkStream stream)
        {
            var response = new byte[] {};
            HttpWebRequest webRequest = WebRequest.Create(request["Url"]) as HttpWebRequest;
            if (webRequest == null)
                return null;
            //                        AddHeaders(webRequest, request);
            var webResponse = await webRequest.GetResponseAsync() as HttpWebResponse;
            if (webResponse == null)
                return null;
            if (!hs.HackRequest(webResponse, stream))
            {
                response = GetResponse(webResponse);
            }
            return response;
        }

        private byte[] GetResponse(HttpWebResponse httpRes)
        {
            var head = ms.GetHead(httpRes);
            if (cbLogRespHeaders.Checked)
                LogHeaders(head);
            var responseBody = comService.Response(httpRes, head);
            return ms.MapToResponse(head, responseBody);
        }

        private void LogHeaders(string head)
        {
            var lines = Request.RequestToList(head);
            foreach(var line in lines)
                PrintMessage(line);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }
    }
}