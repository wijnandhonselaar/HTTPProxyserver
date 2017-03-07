using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTPProxyserver
{
    public partial class Form1 : Form
    {
        private HttpListener _server;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnToggleProxy_Click(object sender, EventArgs e)
        {
            if (_server == null) Listener();
            else Stop();
        }

        private void Stop()
        {
            _server.Stop();
            _server = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        private async void Listener()
        {
            _server = new HttpListener();
            var prefixes = GetPrefixes();
            foreach (var p in prefixes)
                _server.Prefixes.Add(p);
            _server.Start();

            while (true)
                try
                {
                    var ctx = await _server.GetContextAsync();
                    Task.Run(() => ProcessRequest(ctx));
                }
                catch (HttpListenerException e)
                {
                    Console.WriteLine(e);
                }
        }

        private IEnumerable<string> GetPrefixes()
        {
            var prefixes = new List<string>
            {
                "http://*:" + txtPort.Text + "/"
            };
            return prefixes;
        }

        private void ProcessRequest(HttpListenerContext ctx)
        {
            var methodName = ctx.Request.Url.Segments.Length > 1
                ? ctx.Request.Url.Segments[1].Replace("/", "")
                : "get";
            var strParams = ctx.Request.Url
                .Segments
                .Skip(2)
                .Select(s => s.Replace("/", ""))
                .ToList();

            Console.Write(ctx.Request.Headers);

            HttpWebRequest request = WebRequest.Create(ctx.Request.Url) as HttpWebRequest;
            if (request == null) return;
            request.Accept = ctx.Request.Headers.Get("Accept");
//            request.Connection = ctx.Request.Headers.Get("Connection");
            request.ContentType = ctx.Request.ContentType;
            request.ContentLength = ctx.Request.ContentLength64;
            request.Host = ctx.Request.UserHostAddress;

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                PrintMessage("Host: " + response.Server);
                PrintMessage("Status: " + response.StatusCode);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void PrintMessage(string message)
        {
            if (lstLog.InvokeRequired)
            {
                var d = new NewMessage(PrintMessage);
                Invoke(d, message);
            }
            lstLog.Items.Add(message);
        }

        private delegate void NewMessage(string message);
    }
}