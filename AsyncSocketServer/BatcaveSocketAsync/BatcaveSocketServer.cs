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
    public class BatcaveSocketServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mTCPListener;

        List<TcpClient> mClients;

        public EventHandler<ClientConnectedEventArgs> RaiseClientConnectedEvent;
        public EventHandler<MsgReceivedEventArgs> RaiseMsgReceivedEvent;

        public bool KeepRunning { get; set; }

        public BatcaveSocketServer()
        {
            mClients = new List<TcpClient>();
        }

        protected virtual void OnRaiseClientConnectedEvent(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = RaiseClientConnectedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnRaiseMsgReceivedEvent(MsgReceivedEventArgs mrea)
        {
            EventHandler<MsgReceivedEventArgs> handler = RaiseMsgReceivedEvent;

            if (handler != null)
            {
                handler(this, mrea);
            }
        }

        public async void StartListeningForIncomingConnection(IPAddress ipaddy = null, int port = 23000)
        {
            if (ipaddy == null)
            {
                ipaddy = IPAddress.Any;
            }
            if (port <= 0)
            {
                port = 23000;
            }

            mIP = ipaddy;
            mPort = port;

            Debug.WriteLine(string.Format("IP Addy: {0} - Port: {1}", mIP.ToString(), mPort));

            mTCPListener = new TcpListener(mIP, mPort);

            try
            {
                mTCPListener.Start();

                KeepRunning = true;

                while (KeepRunning)
                {
                    var returnedByAccept = await mTCPListener.AcceptTcpClientAsync();

                    mClients.Add(returnedByAccept);

                    Debug.WriteLine(string.Format("Client has connected successfully. Count: {0} - {1}", 
                        mClients.Count, returnedByAccept.Client.RemoteEndPoint));

                    TakeCareOfTCPClient(returnedByAccept);

                    ClientConnectedEventArgs eaClientConnected;
                    eaClientConnected = new ClientConnectedEventArgs(
                        returnedByAccept.Client.RemoteEndPoint.ToString()
                        );
                    OnRaiseClientConnectedEvent(eaClientConnected);
                }


            } 
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void TakeCareOfTCPClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buff = new char[64];

                while (KeepRunning)
                {
                    Debug.WriteLine("#~#~# Ready to OPERATE OPERATE OP OP");

                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);

                    Debug.WriteLine($"Returned: {nRet}");

                    if (nRet == 0)
                    {
                        RemoveClient(paramClient);
                        Debug.WriteLine("We out");
                        break;
                    }

                    string receivedMsg = new string(buff);

                    Debug.WriteLine("Your words: " + receivedMsg);

                    OnRaiseMsgReceivedEvent(new MsgReceivedEventArgs(
                        paramClient.Client.RemoteEndPoint.ToString(),
                        receivedMsg
                        ));

                    Array.Clear(buff, 0, buff.Length);


                }
            } 
            catch (Exception ex)
            {
                RemoveClient(paramClient);
                Debug.WriteLine(ex.ToString());
            }
        }

        private void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient))
            {
                mClients.Remove(paramClient);
                Debug.WriteLine(string.Format("Client removed. Count: {0}", mClients.Count));
            }
        }

        public async void SendToAll(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            try
            {
                byte[] buffMsg = Encoding.ASCII.GetBytes(message);
                foreach(TcpClient c in mClients)
                {
                    c.GetStream().WriteAsync(buffMsg, 0, buffMsg.Length);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void StopServer()
        {
            try
            {
                if (mTCPListener != null)
                {
                    mTCPListener.Stop();
                }

                foreach (TcpClient c in mClients)
                {
                    c.Close();
                }

                mClients.Clear();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.ToString());
            }
        }
    }
}
