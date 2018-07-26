using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ScreenShot
    {
        public static void GetScreen()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2200);
            Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(iPEndPoint);
            socket.Listen(2);
            byte[] recByte = new byte[150_00_00];
            Socket listener = socket.Accept();
            int bytecount = listener.Receive(recByte);

            Rectangle bounds = new Rectangle(0, 0, 1920, 1080);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    var bytes =  stream.ToArray();
                    listener.Send(bytes);
                }
            }
            listener.Shutdown(SocketShutdown.Both);
            listener.Close();
        }


    }
}
