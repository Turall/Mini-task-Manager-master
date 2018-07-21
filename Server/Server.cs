using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows;

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
            IPHostEntry myhost = Dns.GetHostEntry(Dns.GetHostName());
            var ip = myhost.AddressList[1];
            Console.WriteLine("Server IP : {0} ", ip) ;
            IPEndPoint iPEndPoint = new IPEndPoint(ip, 1100);
            Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            string bytes = "";
            var handle = GetConsoleWindow();
                  /// if you want to hide the server window, then uncomment this method
                  /// Console.WriteLine("App after 3 seconds will be hide");
                  /// Thread.Sleep(3000);
                  /// ShowWindow(handle, SW_HIDE); 
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
                    try
                    {
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
                            Thread.Sleep(1000);
                            pros = Process.GetProcesses().Select(x => x.ProcessName);
                            foreach (var item in pros)
                            {
                                bytes += "\n" + item;

                            }
                            listener.Send(Encoding.Unicode.GetBytes(bytes));
                            bytes = "";
                        }
                        else if (data.ToLower().Split('-')[0].Equals("kill"))
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
                            MessageBox.Show("Wrong Command!!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                   

                    listener.Shutdown(SocketShutdown.Both);
                    listener.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
