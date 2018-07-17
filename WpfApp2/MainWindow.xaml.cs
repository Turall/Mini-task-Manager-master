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
        }



        public void ProcessesList()
        {

            var processes = Client.SendMessage(cmdTxt.Text);
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
            ProcessesList();
            if (string.IsNullOrWhiteSpace(Txtbox.Text))
            {
                return;
            }
            else
            {
                Client.SendMessage(Txtbox.Text);
                Txtbox.Clear();
            }
        }

        private void KillBtn_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(Txtbox.Text))
            {
                foreach (string item in processList.Items)
                {
                    if (item.Equals(Txtbox.Text))
                    {
                        Client.SendMessage(Txtbox.Text);
                        Txtbox.Clear();
                    }
                }
            }
            else
            {
                var selected = processList.SelectedItem as string;
                foreach (string item in processList.Items)
                {
                    if (item.Equals(selected))
                    {
                        Client.SendMessage(Txtbox.Text);
                        Txtbox.Clear();
                    }
                }

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cmdTxt.Visibility = Visibility.Visible;
            Txtbox.IsEnabled = true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Txtbox.IsEnabled = false;
        }
    }
}
