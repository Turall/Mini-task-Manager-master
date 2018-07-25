using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows;

namespace Clients
{
    public class Client
    {
        public static string SendMessage(string command,string Ip = "127.0.0.1")
        {
            string ip = Ip;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 1100);
            try
            {
                Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                 socket.Connect(iPEndPoint);
                byte[] recbyte = new byte[150_00_00];
                byte[] cmdBytes = Encoding.Unicode.GetBytes(command);
                socket.Send(cmdBytes);
                int recCount = socket.Receive(recbyte);
                var listOfProc = Encoding.Unicode.GetString(recbyte, 0, recCount);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return listOfProc;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Server not Running!! " + ex.Message);
                return null;
            }
        }
    }
}
