using Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApp2
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

       

        public void GetScreen(byte[] bytes)
        {
            using(MemoryStream memStream = new MemoryStream())
            {
               
                memStream.Write(bytes, 0, Convert.ToInt32(bytes.Length));
                Bitmap bitmap = new Bitmap(memStream, false);
                pictureBox1.Image = bitmap;
            }
        }
    }
}
