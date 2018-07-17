using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows;

namespace Clients
{
    public class Client
    {
        public static string SendMessage(string command)
        {
            
            string ip = "10.0.0.50";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 1100);
            Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(iPEndPoint);
            Console.WriteLine(command
                );
            byte[] recbyte = new byte[1024];
            byte[] cmdBytes = Encoding.Unicode.GetBytes(command);
            socket.Send(cmdBytes);
            int recCount = socket.Receive(recbyte);
            var listOfProc = Encoding.Unicode.GetString(recbyte, 0, recCount);
          

            //if (listOfProc.IndexOf("<TheEnd>") == -1)
            //    SendMessage();

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return listOfProc;

        }
    }
}
