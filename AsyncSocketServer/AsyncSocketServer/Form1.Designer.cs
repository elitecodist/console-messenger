namespace AsyncSocketServer
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
            this.btnAcceptIncomingAsync = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMsgAll = new System.Windows.Forms.Button();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAcceptIncomingAsync
            // 
            this.btnAcceptIncomingAsync.Location = new System.Drawing.Point(133, 335);
            this.btnAcceptIncomingAsync.Name = "btnAcceptIncomingAsync";
            this.btnAcceptIncomingAsync.Size = new System.Drawing.Size(262, 50);
            this.btnAcceptIncomingAsync.TabIndex = 0;
            this.btnAcceptIncomingAsync.Text = "Start Server";
            this.btnAcceptIncomingAsync.UseVisualStyleBackColor = true;
            this.btnAcceptIncomingAsync.Click += new System.EventHandler(this.btnAcceptIncomingAsync_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(322, 418);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(353, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Message:";
            // 
            // btnMsgAll
            // 
            this.btnMsgAll.Location = new System.Drawing.Point(133, 400);
            this.btnMsgAll.Name = "btnMsgAll";
            this.btnMsgAll.Size = new System.Drawing.Size(162, 38);
            this.btnMsgAll.TabIndex = 4;
            this.btnMsgAll.Text = "Message All";
            this.btnMsgAll.UseVisualStyleBackColor = true;
            this.btnMsgAll.Click += new System.EventHandler(this.btnMsgAll_Click);
            // 
            // btnStopServer
            // 
            this.btnStopServer.Location = new System.Drawing.Point(416, 335);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(259, 50);
            this.btnStopServer.TabIndex = 5;
            this.btnStopServer.Text = "Stop Server";
            this.btnStopServer.UseVisualStyleBackColor = true;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(12, 12);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(776, 317);
            this.txtConsole.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.btnMsgAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnAcceptIncomingAsync);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAcceptIncomingAsync;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMsgAll;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.TextBox txtConsole;
    }
}

