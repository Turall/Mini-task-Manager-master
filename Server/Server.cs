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
using System.Threading;
using System.Runtime.InteropServices;

namespace Server
{
    public class Server
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 1100);
            Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            string bytes = "";
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            Console.WriteLine(bytes);
            try
            {

                socket.Bind(iPEndPoint);
                socket.Listen(2);
                byte[] recByte = new byte[5000];
                IEnumerable<string> pros = null;
                while (true)
                {
                    Socket listener = socket.Accept();
                    int bytecount = listener.Receive(recByte);
                    var data = Encoding.Unicode.GetString(recByte, 0, bytecount);

                    if (data.ToLower().Equals("show"))
                    {
                        pros = Process.GetProcesses().Select(x => x.ProcessName);
                        foreach (var item in pros)
                        {
                            bytes += "\n" + item;

                        }
                        listener.Send(Encoding.Unicode.GetBytes(bytes));
                        bytes = "";
                    }
                    else if (data.ToLower().Split('-')[0].Equals("start"))
                    {
                        Process.Start(data.Split('-')[1]);
                         pros = Process.GetProcesses().Select(x => x.ProcessName);
                        foreach (var item in pros)
                        {
                            bytes += "\n" + item;

                        }
                        listener.Send(Encoding.Unicode.GetBytes(bytes));
                        bytes = "";
                    }
                    else if (data.ToLower().Split('-')[0].Equals("Kill"))
                    {
                        foreach (var item in Process.GetProcesses())
                        {
                            if (item.ProcessName.Equals(data.Split('-')[1]))
                            {
                                item.Kill();
                            }
                        }
                        Thread.Sleep(1000);
                        pros = Process.GetProcesses().Select(x => x.ProcessName);
                        foreach (var item in pros)
                        {
                            bytes += "\n" + item;

                        }
                        listener.Send(Encoding.Unicode.GetBytes(bytes));
                        bytes = "";
                    }
                    else
                        return;


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
