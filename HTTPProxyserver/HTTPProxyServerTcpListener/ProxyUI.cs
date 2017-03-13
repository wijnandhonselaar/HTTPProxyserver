using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
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
                var buffer = new byte[2014];
                string context = null;
                var bytes = stream.Read(buffer, 0, buffer.Length);
                while (bytes > 0)
                {
                    context += Encoding.ASCII.GetString(buffer);
                    Array.Clear(buffer, 0, buffer.Length);
                    buffer = new byte[1024];
                    bytes = stream.Read(buffer, 0, buffer.Length);
                }
                if (context != null)
                    try
                    {
                        var request = new Request(context);
                        PrintMessage(request.Method);
                        PrintMessage(request.Url);
                        // handmatig een WebRequst aanmaken op TCP niveau
                        var webRequest = WebRequest.Create(request.Url) as HttpWebRequest;
                        if (webRequest == null) return;
                        var response = await webRequest.GetResponseAsync() as HttpWebResponse;
                        if (response == null) return;
                        var body = "";
                        var s = response.GetResponseStream();
                        if (s == null) return;
                        using (var reader = new StreamReader(s))
                        {
                            body = reader.ReadToEnd();
                        }
                        SendResponse(webRequest, response, stream, body);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
            }
        }

        private void SendResponse(HttpWebRequest webReq, HttpWebResponse httpRes, Stream stream, string body)
        {
            var response = MapToResponse(webReq, httpRes, body);
            
            var buffer = Encoding.ASCII.GetBytes(response);
            stream.Write(buffer, 0, buffer.Length);
        }

        private string MapToResponse(HttpWebRequest webReq, HttpWebResponse httpRes, string body)
        {
            var response = "";
            if (webReq.Method != WebRequestMethods.Http.Get) return response;
            // status line
            response += "HTTP/" + webReq.ProtocolVersion + " " + httpRes.StatusCode.GetHashCode() + " " +
                        httpRes.StatusDescription + "\r\n";
            // Date
            PrintMessage(webReq.Date.ToLongTimeString());
            response += "Date: Sun, 18 Oct 2009 10:47:06 GMT";
            // Server
            response += ToHeaderProperty("Server", httpRes.Server);
            // Content-Type
            response += ToHeaderProperty("Content-Type", httpRes.ContentType);
            // Content-Length
            response += ToHeaderProperty("Content-Length", httpRes.ContentLength.ToString());
            // Connection
            response += ToHeaderProperty("Connection", webReq.Connection);
            // Keep-Alive
            response += ToHeaderProperty("Keep-Alive", webReq.KeepAlive.ToString());
            // Body
            response += "\r\n";
            response += body;
            return response;
        }

        private string ToHeaderProperty(string name, string value)
        {
            return name + ": " + value + "\r\n";
        }
    }
}