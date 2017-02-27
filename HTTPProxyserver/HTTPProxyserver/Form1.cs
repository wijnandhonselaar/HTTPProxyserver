using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTPProxyserver
{
    public partial class Form1 : Form
    {
        private HttpListener server = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnToggleProxy_Click(object sender, EventArgs e)
        {
            if (server == null) Listener();
            else Stop();
        }

        private void Stop()
        {
            server.Close();
            server = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        private void Listener()
        {
            server.Start();
            while (true)
            {
                server.
            }
        }
    }
}
