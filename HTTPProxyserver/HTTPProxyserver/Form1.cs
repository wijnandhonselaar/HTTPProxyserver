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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTPProxyserver
{
    public partial class Form1 : Form
    {
        private TcpListener _server;


        private int _bufferSize = 2048;

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

        private async void ProcessRequest(TcpClient client)
        {

            var browserStream = client.GetStream();
            
            var buffer = new byte[_bufferSize];
            while (true)
            {
                var bufferLength = await browserStream.ReadAsync(buffer, 0, buffer.Length);
                var request = ToHead(Encoding.ASCII.GetString(buffer, 0, bufferLength));
                using (var host = new TcpClient(request["Host"], 80))
                {
                    var outsideStream = host.GetStream();
                    Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bufferLength));
                    outsideStream.Write(buffer, 0, bufferLength);
                    Array.Clear(buffer, 0, bufferLength);
                    buffer = new byte[_bufferSize];
                    bufferLength = await outsideStream.ReadAsync(buffer, 0, buffer.Length);
                    Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bufferLength));
                    browserStream.Write(buffer, 0, bufferLength);
                }
            }
            browserStream.Close();
            client.Close();

        }

        private void PrintMessage(string text)
        {
            if (lstLog.InvokeRequired)
            {
                var d = new MessageHandler(PrintMessage);
                Invoke(d, text);
            }
            else
            {
                lstLog.Items.Add(text);
            }
        }

        private Dictionary<string, string> ToHead(string context)
        {
            var request = new Dictionary<string, string>();
            var lines = RequestToList(context);
            for (var i = 0; i < lines.Count; i++)
            {
                if (i > 0)
                {
                    if (lines[i].Contains(':'))
                    {
                        request.Add(lines[i].Split(':')[0], lines[i].Split(':')[1].Trim());
                    }
                    else if (lines[i] == "\r\n")
                    {
                        var body = "";
                        for (var y = i + 1; y < lines.Count; y++)
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

        public static List<string> RequestToList(string text)
        {
            return text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }

    }
}