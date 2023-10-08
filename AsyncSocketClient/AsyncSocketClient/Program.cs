using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatcaveSocketAsync; 

namespace AsyncSocketClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BatcaveSocketClient client = new BatcaveSocketClient();
            client.RaiseMsgReceivedEvent += HandleMsgReceived;
            Console.WriteLine("*** Console Messaging App by elitecodist ***");
            Console.WriteLine();
            Console.WriteLine("Enter a valid IP Address OR (<HOST>[host name]): ");

            string inputIPaddy = Console.ReadLine();

            Console.WriteLine("Enter a valid port number (0-65535): ");

            string inputPort = Console.ReadLine();

            if (inputIPaddy.StartsWith("<HOST>"))
            {
                inputIPaddy = inputIPaddy.Replace("<HOST>", string.Empty);
                inputIPaddy = Convert.ToString(BatcaveSocketClient.ResolveHostNameToIPAddy(inputIPaddy));
            }

            if (string.IsNullOrEmpty(inputIPaddy))
            {
                Console.WriteLine("No IP Address was supplied.");
                Environment.Exit(0);
            }

            if (!client.SetServerIPAddy(inputIPaddy) ||
                    !client.SetPortNum(inputPort))
            {
                Console.WriteLine("Inavlid IP addresss or port number supplied: ({0}:{1})");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }

            client.ConnectToServer();

            string inputStr = null;

            do
            {
                inputStr = Console.ReadLine();

                if (inputStr.Trim() != "<EXIT>")
                {
                    client.SendToServer(inputStr);
                }
                else if (inputStr.Equals("<EXIT>"))
                {
                    client.CloseAndDisconnect();
                }

            } while (inputStr != "<EXIT>");
        }

        private static void HandleMsgReceived(object sender, MsgReceivedEventArgs e)
        {
            Console.WriteLine(
                string.Format("({0}) Msg Received: {1}{2}",
                    DateTime.Now,
                    e.MsgReceived,
                    Environment.NewLine));
        }
    }
}
