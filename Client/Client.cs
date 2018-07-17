using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Clients
{
  public  class Client
    {
        static void Main(string[] args)
        {
            try
            {
               SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
      public  static string SendMessage()
        {
            string ip = "10.0.0.50";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 1100);
            Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(iPEndPoint);
            Console.WriteLine("Enter Command: ");
            byte[] recbyte = new byte[1024];
            string cmd = Console.ReadLine();
            byte[] cmdBytes = Encoding.UTF8.GetBytes(cmd);
            socket.Send(cmdBytes);
            int recCount = socket.Receive(recbyte);
            var listOfProc = Encoding.UTF8.GetString(recbyte);
            Console.WriteLine(listOfProc);
          
            if (cmd.IndexOf("<TheEnd>") == -1)
                SendMessage();

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return listOfProc;

        }
    }
}
