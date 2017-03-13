using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTPProxyServerTcpListener
{
    public partial class Form1 : Form
    {
        private TcpListener _server;
        private bool _listening;

        private delegate void MessageHandler(string message);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnToggleProxy_Click(object sender, EventArgs e)
        {
            if (!_listening) Listener();
            else Stop();
        }

        private void Stop()
        {
            _listening = false;
            _server.Stop();
        }

        private async void Listener()
        {
            _listening = true;
            var port = Int32.Parse(txtPort.Text);
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();
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
                {
//                    Console.Write(context);   
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
                        PrintMessage(response.ContentType);
                        var body = "";
                        var s = response.GetResponseStream();
                        if (s == null) return;
                        using (var reader = new StreamReader(s))
                        {
                            body = reader.ReadToEnd();
                        }
                        PrintMessage(body);
                        //                        Console.Write(request);
                        // voer request uit
                        // if get, lees body uit met readtoendasync
                    }
                    catch (Exception e)
                    {
//                        Console.WriteLine(e);
                        throw;
                    }
                    
//                    request.Proxy = new WebProxy();
//                    request = new Request(content);
                }
            }
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
    }

    internal class Request
    {
        public Request(string content)
        {
            var lines = RequestToList(content);

            Method = Split(lines[0], 0);
            Url = Split(lines[0], 1);
            HttpVersion = Split(lines[0], 2);
            UserAgent = Split(lines[1]);
            Accept = Split(lines[2]);
            AcceptLanguage = Split(lines[3]);
            AcceptEncoding = Split(lines[4]);
            Con = Split(lines[5]);
        }

        public string Method { get; set; }
        public string Url { get; set; }
        public string HttpVersion { get; set; }
        public string UserAgent { get; set; }
        public string Accept { get; set; }
        public string AcceptLanguage { get; set; }
        public string AcceptEncoding { get; set; }
        public string Con { get; set; }

        private List<string> RequestToList(string text)
        {
            string[] seperators = { "\r\n" };
            return text.Split(seperators, StringSplitOptions.None).ToList();
        }

        private string Split(string line, int index = 1, char x = ' ')
        {
            return line.Split(x)[index];
        }

        private CacheItem CreateCache(string url)
        {
            var req = WebRequest.Create(url) as HttpWebRequest;
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

        public class CacheItem
        {
            public CacheItem(HttpWebResponse response, string body)
            {
                Response = response;
                Body = body;
            }
            public HttpWebResponse Response { get; set; }
            public string Body { get; set; }

        }
        //GET http://theradavist.com/ HTTP/1.1
        //Host: theradavist.com
        //User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64; rv:51.0) Gecko/20100101 Firefox/51.0
        //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
        //Accept-Language: en-GB,en;q=0.5
        //Accept-Encoding: gzip, deflate
        //Connection: keep-alive
        //Upgrade-Insecure-Requests: 1
    }
}
