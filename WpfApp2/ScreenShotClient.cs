using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    public  class ScreenShotClient
    {
        public static byte[] ScreenShot(string command, string Ip = "127.0.0.1")
        {
            string ip = Ip;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip),2200);
            try
            {
                Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(iPEndPoint);
                byte[] recbyte = new byte[150_00_00];
               
                 socket.Receive(recbyte);
                
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return recbyte;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Server not Running!! " + ex.Message);
                return null;
            }
        }
    }
}
