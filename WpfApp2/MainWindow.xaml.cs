using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Clients;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            TxtBoxCommand.Text = "Commands :\nCommand 'Show' - show all processes. \n" +
                "Command 'Start-App Name'- Run Application. \n" +
                "Command 'Kill- App Name' - Kill Aplication";
        }


        public void ProcessesList(string cmd)
        {

            var processes = Client.SendMessage(cmd);
            if (processList.Items.Count != 0)
            {
                processList.Items.Clear();
            }
            foreach (var item in processes.Split('\n'))
            {
                processList.Items.Add(item);
            }

        }


        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(Txtbox.Text))
            {
                return;
            }
            else
            {
                ProcessesList(Txtbox.Text);
            }
        }

        //private void KillBtn_Click(object sender, RoutedEventArgs e)
        //{

        //    if (!string.IsNullOrWhiteSpace(Txtbox.Text))
        //    {
        //        ProcessesList(Txtbox.Text);

        //    }
        //    else
        //    {
        //        var selected = processList.SelectedItem as string;
        //        ProcessesList(Txtbox.Text);
        //    }

        //}
    }
}
