using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTPProxyserver
{
    public partial class Form1 : Form
    {
        private HttpListener _server;

        private delegate void MessageHandler(string message);
        private bool _listening;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnToggleProxy_Click(object sender, EventArgs e)
        {
            if (_listening == false) Listener();
            else Stop();
        }

        private void Stop()
        {
            _listening = false;
            _server.Stop();
            _server = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        private async void Listener()
        {
//            _server = new HttpListener();
//            var prefixes = GetPrefixes();
//            foreach (var p in prefixes)
//                _server.Prefixes.Add(p);
//            _server.Start();
//
//            while (true)
//                try
//                {
//                    var ctx = await _server.GetContextAsync();
//                    Task.Run(() => ProcessRequest(ctx));
//                }
//                catch (HttpListenerException e)
//                {
//                    Console.WriteLine(e);
//                }
//        }
//
//        private IEnumerable<string> GetPrefixes()
//        {
//            var prefixes = new List<string>
//                        {
//                            "http://*:" + txtPort.Text + "/"
//                        };
//            return prefixes;
//        }
//
//        private void ProcessRequest(HttpListenerContext ctx)
//        {
//            var methodName = ctx.Request.Url.Segments.Length > 1
//                ? ctx.Request.Url.Segments[1].Replace("/", "")
//                : "get";
//            var strParams = ctx.Request.Url
//                .Segments
//                .Skip(2)
//                .Select(s => s.Replace("/", ""))
//                .ToList();
//
//            Console.Write(ctx.Request.Headers);
//
//            HttpWebRequest request = WebRequest.Create(ctx.Request.Url) as HttpWebRequest;
//            if (request == null) return;
//            request.Accept = ctx.Request.Headers.Get("Accept");
//            //            request.Connection = ctx.Request.Headers.Get("Connection");
//            request.ContentType = ctx.Request.ContentType;
//            request.ContentLength = ctx.Request.ContentLength64;
//            request.Host = ctx.Request.UserHostAddress;
//
//            HttpWebResponse response;
//            try
//            {
//                response = (HttpWebResponse)request.GetResponse();
//                PrintText("Host: " + response.Server);
//                PrintText("Status: " + response.StatusCode);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
                _listening = true;
            _server = new HttpListener();
            _server.Prefixes.Add("http://*:" + txtPort.Text + "/");
            _server.Start();

            PrintText("Listening for requests!");
            while (_listening)
            {
                try
                {
                    var context = await _server.GetContextAsync();
                    Task.Run(() => ProcessRequest(context));
                }
                catch (HttpListenerException e)
                {
                    PrintText("Listener has been stopped successfully");
                }
            }

        }

        private void ProcessRequest(HttpListenerContext context)
        {
            var buffer = new byte[2048];
            PrintText("Message!");
            
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            
        }

        private void PrintText(string text)
        {
            if (lstLog.InvokeRequired)
            {
                var d = new MessageHandler(PrintText);
                Invoke(d, text);
            }
            else
            {
                lstLog.Items.Add(text);
            }
        }

        private delegate void NewMessage(string message);
    }
}