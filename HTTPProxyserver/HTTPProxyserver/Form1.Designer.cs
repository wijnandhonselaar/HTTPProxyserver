namespace HTTPProxyserver
{
    partial class Form1
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
            this.lstLog = new System.Windows.Forms.ListBox();
            this.cbLogClient = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.cbLogContentOut = new System.Windows.Forms.CheckBox();
            this.cbLogContentIn = new System.Windows.Forms.CheckBox();
            this.cbLogRespHeaders = new System.Windows.Forms.CheckBox();
            this.cbLogReqHeaders = new System.Windows.Forms.CheckBox();
            this.cbAccessAuthentication = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.cbContentFilter = new System.Windows.Forms.CheckBox();
            this.cbChangedContent = new System.Windows.Forms.CheckBox();
            this.txtBufferSize = new System.Windows.Forms.TextBox();
            this.txtCache = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnToggleProxy = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbLogClient.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Location = new System.Drawing.Point(13, 13);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(402, 472);
            this.lstLog.TabIndex = 0;
            // 
            // cbLogClient
            // 
            this.cbLogClient.Controls.Add(this.label3);
            this.cbLogClient.Controls.Add(this.label2);
            this.cbLogClient.Controls.Add(this.label1);
            this.cbLogClient.Controls.Add(this.checkBox9);
            this.cbLogClient.Controls.Add(this.cbLogContentOut);
            this.cbLogClient.Controls.Add(this.cbLogContentIn);
            this.cbLogClient.Controls.Add(this.cbLogRespHeaders);
            this.cbLogClient.Controls.Add(this.cbLogReqHeaders);
            this.cbLogClient.Controls.Add(this.cbAccessAuthentication);
            this.cbLogClient.Controls.Add(this.checkBox4);
            this.cbLogClient.Controls.Add(this.cbContentFilter);
            this.cbLogClient.Controls.Add(this.cbChangedContent);
            this.cbLogClient.Controls.Add(this.txtBufferSize);
            this.cbLogClient.Controls.Add(this.txtCache);
            this.cbLogClient.Controls.Add(this.txtPort);
            this.cbLogClient.Location = new System.Drawing.Point(435, 13);
            this.cbLogClient.Name = "cbLogClient";
            this.cbLogClient.Size = new System.Drawing.Size(386, 374);
            this.cbLogClient.TabIndex = 18;
            this.cbLogClient.TabStop = false;
            this.cbLogClient.Text = "Proxy Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "T4, T8) Buffersize";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 48);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(159, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "P1.a) cache time out in seconds";
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
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(288, 284);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox9.Size = new System.Drawing.Size(92, 17);
            this.checkBox9.TabIndex = 29;
            this.checkBox9.Text = "Logging client";
            this.checkBox9.UseVisualStyleBackColor = true;
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
            this.cbAccessAuthentication.Location = new System.Drawing.Point(174, 140);
            this.cbAccessAuthentication.Name = "cbAccessAuthentication";
            this.cbAccessAuthentication.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbAccessAuthentication.Size = new System.Drawing.Size(205, 17);
            this.cbAccessAuthentication.TabIndex = 24;
            this.cbAccessAuthentication.Text = "P4.b) Zet basic access authentication";
            this.cbAccessAuthentication.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(190, 117);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox4.Size = new System.Drawing.Size(190, 17);
            this.checkBox4.TabIndex = 23;
            this.checkBox4.Text = "P4.a) Zet het wijzigen van headers";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // cbContentFilter
            // 
            this.cbContentFilter.AutoSize = true;
            this.cbContentFilter.Location = new System.Drawing.Point(255, 94);
            this.cbContentFilter.Name = "cbContentFilter";
            this.cbContentFilter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbContentFilter.Size = new System.Drawing.Size(125, 17);
            this.cbContentFilter.TabIndex = 22;
            this.cbContentFilter.Text = "P2.) Zet content filter";
            this.cbContentFilter.UseVisualStyleBackColor = true;
            // 
            // cbChangedContent
            // 
            this.cbChangedContent.AutoSize = true;
            this.cbChangedContent.Location = new System.Drawing.Point(124, 71);
            this.cbChangedContent.Name = "cbChangedContent";
            this.cbChangedContent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbChangedContent.Size = new System.Drawing.Size(256, 17);
            this.cbChangedContent.TabIndex = 21;
            this.cbChangedContent.Text = "P1.b) Zet check op gewijzigde content op server";
            this.cbChangedContent.UseVisualStyleBackColor = true;
            // 
            // txtBufferSize
            // 
            this.txtBufferSize.Location = new System.Drawing.Point(193, 166);
            this.txtBufferSize.Name = "txtBufferSize";
            this.txtBufferSize.Size = new System.Drawing.Size(187, 20);
            this.txtBufferSize.TabIndex = 20;
            // 
            // txtCache
            // 
            this.txtCache.Location = new System.Drawing.Point(193, 45);
            this.txtCache.Name = "txtCache";
            this.txtCache.Size = new System.Drawing.Size(187, 20);
            this.txtCache.TabIndex = 19;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(193, 19);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(187, 20);
            this.txtPort.TabIndex = 18;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnToggleProxy);
            this.groupBox2.Location = new System.Drawing.Point(435, 394);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 88);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // btnToggleProxy
            // 
            this.btnToggleProxy.Location = new System.Drawing.Point(6, 19);
            this.btnToggleProxy.Name = "btnToggleProxy";
            this.btnToggleProxy.Size = new System.Drawing.Size(183, 57);
            this.btnToggleProxy.TabIndex = 17;
            this.btnToggleProxy.Text = "Start / Stop proxy";
            this.btnToggleProxy.UseVisualStyleBackColor = true;
            this.btnToggleProxy.Click += new System.EventHandler(this.btnToggleProxy_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(195, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(185, 57);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear log";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 492);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbLogClient);
            this.Controls.Add(this.lstLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.cbLogClient.ResumeLayout(false);
            this.cbLogClient.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.GroupBox cbLogClient;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox cbLogContentOut;
        private System.Windows.Forms.CheckBox cbLogContentIn;
        private System.Windows.Forms.CheckBox cbLogRespHeaders;
        private System.Windows.Forms.CheckBox cbLogReqHeaders;
        private System.Windows.Forms.CheckBox cbAccessAuthentication;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox cbContentFilter;
        private System.Windows.Forms.CheckBox cbChangedContent;
        private System.Windows.Forms.TextBox txtBufferSize;
        private System.Windows.Forms.TextBox txtCache;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnToggleProxy;
    }
}

