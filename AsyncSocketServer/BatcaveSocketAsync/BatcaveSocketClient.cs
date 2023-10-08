 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BatcaveSocketAsync
{
    public class BatcaveSocketClient
    {
        IPAddress mServerIPaddy;
        int mServerPort;
        TcpClient mClient;

        public event EventHandler<MsgReceivedEventArgs> RaiseMsgReceivedEvent;

        public BatcaveSocketClient()
        {
            mClient = null;
            mServerPort = -1;
            mServerIPaddy = null;
        }

        public IPAddress ServerIPAddy
        {
            get
            {
                return mServerIPaddy;
            }
        }

        public int ServerPort
        {
            get
            {
                return mServerPort;
            }
        }

        public bool SetServerIPAddy(string _IPAddyServer)
        {
            IPAddress ipaddy = null;

            if (!IPAddress.TryParse(_IPAddyServer, out ipaddy))
            {
                Console.WriteLine("Invalid IP address supplied.");
                return false;
            }

            mServerIPaddy = ipaddy;

            return true;
        }

        protected virtual void OnRaiseMsgReceivedEvent(MsgReceivedEventArgs mrea)
        {
            EventHandler<MsgReceivedEventArgs> handler = RaiseMsgReceivedEvent;
            if (handler != null)
            {
                handler(this, mrea);
            }     
            
        }

        public bool SetPortNum(string _ServerPort)
        {
            int portNum = 0;

            if (!int.TryParse(_ServerPort.Trim(), out portNum))
            {
                Console.WriteLine("Invalid port number supplied.");
                return false;
            }

            if (portNum <= 0 || portNum > 65535)
            {
                Console.WriteLine("Port number must be b/t 0 and 65535.");
                return false;
            }

            mServerPort = portNum;

            return true;
        }

        public async Task ConnectToServer()
        {
            if (mClient == null)
            {
                mClient = new TcpClient();
            }

            try
            {
                await mClient.ConnectAsync(mServerIPaddy, mServerPort);
                Console.WriteLine(string.Format("Connected to server: {0}:{1}",
                    mServerIPaddy, mServerPort));
                Console.WriteLine();
                Console.WriteLine("######################## Start of Chat Log ########################");

                ReadDataAsync(mClient);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        private async void ReadDataAsync(TcpClient mClient)
        {
            try
            {
                StreamReader clientStreamReader = new StreamReader(mClient.GetStream());
                char[] buff = new char[64];
                int readByteCount = 0;

                while (true)
                {
                    readByteCount = await clientStreamReader.ReadAsync(buff, 0, buff.Length);
                    
                    if (readByteCount <= 0)
                    {
                        Console.WriteLine("Disconnected from server.");
                        mClient.Close();
                        break;
                    }
                    Console.WriteLine(string.Format("Received bytes: {0} - Message: {1}",
                        readByteCount, new string(buff)));

                    OnRaiseMsgReceivedEvent(
                        new MsgReceivedEventArgs(
                            mClient.Client.RemoteEndPoint.ToString(),
                            new string(buff)));

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public async Task SendToServer(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
            {
                Console.WriteLine("Empty string supplied.");
                return;
            }

            if (mClient != null)
            {
                if (mClient.Connected)
                {
                    StreamWriter clientStreamWriter = new StreamWriter(mClient.GetStream());
                    clientStreamWriter.AutoFlush = true;

                    await clientStreamWriter.WriteAsync(inputStr);
                    Console.WriteLine("Data sent");
                }
            }
        }

        public void CloseAndDisconnect()
        {
            if (mClient != null)
            {
                if (mClient.Connected)
                {
                    mClient.Close();
                }
            }
        }

        public static IPAddress ResolveHostNameToIPAddy(string hostName)
        {
            IPAddress[] returnAddy = null;

            try
            {
                returnAddy = Dns.GetHostAddresses(hostName);

                foreach(IPAddress addy in returnAddy)
                {
                    if (addy.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return addy;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
