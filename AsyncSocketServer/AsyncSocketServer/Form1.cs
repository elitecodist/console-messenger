using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatcaveSocketAsync;

namespace AsyncSocketServer
{
    public partial class Form1 : Form
    {
        BatcaveSocketServer mServer;
        public Form1()
        {
            InitializeComponent();
            mServer = new BatcaveSocketServer();
            mServer.RaiseClientConnectedEvent += HandleClientConnected;
            mServer.RaiseMsgReceivedEvent += HandleMsgReceived;
        }

        private void btnAcceptIncomingAsync_Click(object sender, EventArgs e)
        {
            mServer.StartListeningForIncomingConnection();
        }

        private void btnMsgAll_Click(object sender, EventArgs e)
        {
            mServer.SendToAll(textBox1.Text.Trim());
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }

        void HandleClientConnected(object sender, ClientConnectedEventArgs ccea)
        {
            txtConsole.AppendText(string.Format("({0}) New client connected: {1}{2}",
                DateTime.Now, ccea.NewClient, Environment.NewLine));
        }

        void HandleMsgReceived(object sender, MsgReceivedEventArgs mrea)
        {
            txtConsole.AppendText(string.Format("({0}) {1}: {2}{3}",
                DateTime.Now, mrea.MessagingClient, mrea.MsgReceived, Environment.NewLine));
            txtConsole.AppendText(Environment.NewLine);
        }
    }
}
