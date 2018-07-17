using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Json;
using NiL.JS;

namespace Server
{
   public  class Server
    {
        static void Main(string[] args)
        {
            string ip = "10.0.0.50";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 1100);
            Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            string bytes = "";
          
            Console.WriteLine(bytes);
            try
            {

                socket.Bind(iPEndPoint);
                socket.Listen(2);
                byte[] recByte = new byte[1024];
                while (true)
                {
                    var pros = Process.GetProcesses().Select(x => x.ProcessName);
                    foreach (var item in pros)
                    {
                        bytes += "\n" + item;

                    }
                    Socket listener = socket.Accept();
                    int bytecount = listener.Receive(recByte);
                    var data = Encoding.Unicode.GetString(recByte, 0, bytecount);

                    if (data.Equals("Show"))
                    {
                        listener.Send(Encoding.Unicode.GetBytes(bytes));
                    }
                    else if (data.Split('-')[0].Equals("Start"))
                    {
                        foreach (var item in Process.GetProcesses())
                        {
                            if (item.ProcessName.Equals(data.Split('-')[1]))
                            {
                                Process.Start(item.ProcessName);
                            }
                        }
                    } else if (data.Split('-')[0].Equals("Kill"))
                    {
                        foreach (var item in Process.GetProcesses())
                        {
                            if (item.ProcessName.Equals(data.Split('-')[1]))
                            {
                                item.Kill();
                            }
                        }
                    }
                    else
                        return ;
                    

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Server Closing");
                        break;
                    }
                    listener.Shutdown(SocketShutdown.Both);
                    listener.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
