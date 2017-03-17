namespace HTTPProxyServerTcpListener
{
    partial class ProxyUi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnToggleProxy = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLogClient = new System.Windows.Forms.CheckBox();
            this.cbLogContentOut = new System.Windows.Forms.CheckBox();
            this.cbLogContentIn = new System.Windows.Forms.CheckBox();
            this.cbLogRespHeaders = new System.Windows.Forms.CheckBox();
            this.cbLogReqHeaders = new System.Windows.Forms.CheckBox();
            this.cbAccessAuthentication = new System.Windows.Forms.CheckBox();
            this.cbChangeHeaders = new System.Windows.Forms.CheckBox();
            this.cbContentFilter = new System.Windows.Forms.CheckBox();
            this.cbChangedContent = new System.Windows.Forms.CheckBox();
            this.txtBufferSize = new System.Windows.Forms.TextBox();
            this.txtCache = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.container1 = new System.Windows.Forms.GroupBox();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.btnClearCache = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.container1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnToggleProxy
            // 
            this.btnToggleProxy.Location = new System.Drawing.Point(6, 93);
            this.btnToggleProxy.Name = "btnToggleProxy";
            this.btnToggleProxy.Size = new System.Drawing.Size(183, 57);
            this.btnToggleProxy.TabIndex = 17;
            this.btnToggleProxy.Text = "Start / Stop proxy";
            this.btnToggleProxy.UseVisualStyleBackColor = true;
            this.btnToggleProxy.Click += new System.EventHandler(this.btnToggleProxy_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClearCache);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnToggleProxy);
            this.groupBox2.Location = new System.Drawing.Point(434, 325);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 156);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(195, 93);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(185, 57);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear log";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Buffersize";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 48);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Cache timout in seconds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "proxy port";
            // 
            // cbLogClient
            // 
            this.cbLogClient.AutoSize = true;
            this.cbLogClient.Location = new System.Drawing.Point(288, 284);
            this.cbLogClient.Name = "cbLogClient";
            this.cbLogClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbLogClient.Size = new System.Drawing.Size(92, 17);
            this.cbLogClient.TabIndex = 29;
            this.cbLogClient.Text = "Logging client";
            this.cbLogClient.UseVisualStyleBackColor = true;
            // 
            // cbLogContentOut
            // 
            this.cbLogContentOut.AutoSize = true;
            this.cbLogContentOut.Location = new System.Drawing.Point(251, 261);
            this.cbLogContentOut.Name = "cbLogContentOut";
            this.cbLogContentOut.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbLogContentOut.Size = new System.Drawing.Size(129, 17);
            this.cbLogContentOut.TabIndex = 28;
            this.cbLogContentOut.Text = "Logging content OUT";
            this.cbLogContentOut.UseVisualStyleBackColor = true;
            // 
            // cbLogContentIn
            // 
            this.cbLogContentIn.AutoSize = true;
            this.cbLogContentIn.Location = new System.Drawing.Point(260, 238);
            this.cbLogContentIn.Name = "cbLogContentIn";
            this.cbLogContentIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbLogContentIn.Size = new System.Drawing.Size(120, 17);
            this.cbLogContentIn.TabIndex = 27;
            this.cbLogContentIn.Text = "Logging concent IN";
            this.cbLogContentIn.UseVisualStyleBackColor = true;
            // 
            // cbLogRespHeaders
            // 
            this.cbLogRespHeaders.AutoSize = true;
            this.cbLogRespHeaders.Checked = true;
            this.cbLogRespHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLogRespHeaders.Location = new System.Drawing.Point(229, 215);
            this.cbLogRespHeaders.Name = "cbLogRespHeaders";
            this.cbLogRespHeaders.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbLogRespHeaders.Size = new System.Drawing.Size(151, 17);
            this.cbLogRespHeaders.TabIndex = 26;
            this.cbLogRespHeaders.Text = "Logging response headers";
            this.cbLogRespHeaders.UseVisualStyleBackColor = true;
            // 
            // cbLogReqHeaders
            // 
            this.cbLogReqHeaders.AutoSize = true;
            this.cbLogReqHeaders.Checked = true;
            this.cbLogReqHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLogReqHeaders.Location = new System.Drawing.Point(237, 192);
            this.cbLogReqHeaders.Name = "cbLogReqHeaders";
            this.cbLogReqHeaders.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbLogReqHeaders.Size = new System.Drawing.Size(143, 17);
            this.cbLogReqHeaders.TabIndex = 25;
            this.cbLogReqHeaders.Text = "Logging request headers";
            this.cbLogReqHeaders.UseVisualStyleBackColor = true;
            // 
            // cbAccessAuthentication
            // 
            this.cbAccessAuthentication.AutoSize = true;
            this.cbAccessAuthentication.Location = new System.Drawing.Point(164, 140);
            this.cbAccessAuthentication.Name = "cbAccessAuthentication";
            this.cbAccessAuthentication.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbAccessAuthentication.Size = new System.Drawing.Size(215, 17);
            this.cbAccessAuthentication.TabIndex = 24;
            this.cbAccessAuthentication.Text = "Turn basic access authentication on/off";
            this.cbAccessAuthentication.UseVisualStyleBackColor = true;
            // 
            // cbChangeHeaders
            // 
            this.cbChangeHeaders.AutoSize = true;
            this.cbChangeHeaders.Location = new System.Drawing.Point(211, 117);
            this.cbChangeHeaders.Name = "cbChangeHeaders";
            this.cbChangeHeaders.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbChangeHeaders.Size = new System.Drawing.Size(168, 17);
            this.cbChangeHeaders.TabIndex = 23;
            this.cbChangeHeaders.Text = "Turn changing headers on/off";
            this.cbChangeHeaders.UseVisualStyleBackColor = true;
            // 
            // cbContentFilter
            // 
            this.cbContentFilter.AutoSize = true;
            this.cbContentFilter.Location = new System.Drawing.Point(239, 94);
            this.cbContentFilter.Name = "cbContentFilter";
            this.cbContentFilter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbContentFilter.Size = new System.Drawing.Size(141, 17);
            this.cbContentFilter.TabIndex = 22;
            this.cbContentFilter.Text = "Turn content filter on/off";
            this.cbContentFilter.UseVisualStyleBackColor = true;
            // 
            // cbChangedContent
            // 
            this.cbChangedContent.AutoSize = true;
            this.cbChangedContent.Location = new System.Drawing.Point(223, 71);
            this.cbChangedContent.Name = "cbChangedContent";
            this.cbChangedContent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbChangedContent.Size = new System.Drawing.Size(156, 17);
            this.cbChangedContent.TabIndex = 21;
            this.cbChangedContent.Text = "Check for changed content";
            this.cbChangedContent.UseVisualStyleBackColor = true;
            // 
            // txtBufferSize
            // 
            this.txtBufferSize.Location = new System.Drawing.Point(193, 166);
            this.txtBufferSize.Name = "txtBufferSize";
            this.txtBufferSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBufferSize.Size = new System.Drawing.Size(187, 20);
            this.txtBufferSize.TabIndex = 20;
            this.txtBufferSize.Text = "2048";
            // 
            // txtCache
            // 
            this.txtCache.Location = new System.Drawing.Point(193, 45);
            this.txtCache.Name = "txtCache";
            this.txtCache.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCache.Size = new System.Drawing.Size(187, 20);
            this.txtCache.TabIndex = 19;
            this.txtCache.Text = "300";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(193, 19);
            this.txtPort.Name = "txtPort";
            this.txtPort.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPort.Size = new System.Drawing.Size(187, 20);
            this.txtPort.TabIndex = 18;
            this.txtPort.Text = "8080";
            // 
            // container1
            // 
            this.container1.Controls.Add(this.label3);
            this.container1.Controls.Add(this.label2);
            this.container1.Controls.Add(this.label1);
            this.container1.Controls.Add(this.cbLogClient);
            this.container1.Controls.Add(this.cbLogContentOut);
            this.container1.Controls.Add(this.cbLogContentIn);
            this.container1.Controls.Add(this.cbLogRespHeaders);
            this.container1.Controls.Add(this.cbLogReqHeaders);
            this.container1.Controls.Add(this.cbAccessAuthentication);
            this.container1.Controls.Add(this.cbChangeHeaders);
            this.container1.Controls.Add(this.cbContentFilter);
            this.container1.Controls.Add(this.cbChangedContent);
            this.container1.Controls.Add(this.txtBufferSize);
            this.container1.Controls.Add(this.txtCache);
            this.container1.Controls.Add(this.txtPort);
            this.container1.Location = new System.Drawing.Point(434, 12);
            this.container1.Name = "container1";
            this.container1.Size = new System.Drawing.Size(386, 307);
            this.container1.TabIndex = 21;
            this.container1.TabStop = false;
            this.container1.Text = "Proxy Settings";
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lstLog.Location = new System.Drawing.Point(12, 12);
            this.lstLog.Name = "lstLog";
            this.lstLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstLog.Size = new System.Drawing.Size(402, 472);
            this.lstLog.TabIndex = 20;
            // 
            // btnClearCache
            // 
            this.btnClearCache.Location = new System.Drawing.Point(193, 19);
            this.btnClearCache.Name = "btnClearCache";
            this.btnClearCache.Size = new System.Drawing.Size(185, 57);
            this.btnClearCache.TabIndex = 19;
            this.btnClearCache.Text = "Clear cache";
            this.btnClearCache.UseVisualStyleBackColor = true;
            this.btnClearCache.Click += new System.EventHandler(this.btnClearCache_Click);
            // 
            // ProxyUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 497);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.container1);
            this.Controls.Add(this.lstLog);
            this.Name = "ProxyUi";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.container1.ResumeLayout(false);
            this.container1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnToggleProxy;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbLogClient;
        private System.Windows.Forms.CheckBox cbLogContentOut;
        private System.Windows.Forms.CheckBox cbLogContentIn;
        private System.Windows.Forms.CheckBox cbLogRespHeaders;
        private System.Windows.Forms.CheckBox cbLogReqHeaders;
        private System.Windows.Forms.CheckBox cbAccessAuthentication;
        private System.Windows.Forms.CheckBox cbChangeHeaders;
        private System.Windows.Forms.CheckBox cbContentFilter;
        private System.Windows.Forms.CheckBox cbChangedContent;
        private System.Windows.Forms.TextBox txtBufferSize;
        private System.Windows.Forms.TextBox txtCache;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.GroupBox container1;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Button btnClearCache;
    }
}

