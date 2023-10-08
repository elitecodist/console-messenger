using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BatcaveSocketAsync
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public string NewClient { get; set; }

        public ClientConnectedEventArgs(string _newClient)
        {
            NewClient = _newClient;
        }
    }

    public class MsgReceivedEventArgs : EventArgs
    {
        public string MessagingClient { get; set; }
        public string MsgReceived { get; set; }

        public MsgReceivedEventArgs(string _messagingClient, string _msgReceived)
        {
            MessagingClient = _messagingClient;
            MsgReceived = _msgReceived;
        }
    }
}