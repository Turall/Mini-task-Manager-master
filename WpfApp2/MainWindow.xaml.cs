using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Diagnostics;
using System.Collections.ObjectModel;

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
            System.Windows.Threading.DispatcherTimer dispatcher = new System.Windows.Threading.DispatcherTimer();
            dispatcher.Tick += Dispatcher_Tick;
            dispatcher.Interval = new TimeSpan(0, 0, 3);
            dispatcher.Start();
        }

        private void Dispatcher_Tick(object sender, EventArgs e)
        {
            ProcessesList();
        }

        public  void ProcessesList()
        {
            var processes = Process.GetProcesses();
            if(processList.Items.Count != 0)
            {
            processList.Items.Clear();
            }
            foreach (var item in processes)
            {
                processList.Items.Add(item.ProcessName);
            }
           
        }
        public void CallBack(object obj)
        {
            ProcessesList();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txtbox.Text))
            {
                return;
            }
            else
            {
                Process.Start(Txtbox.Text);
                Txtbox.Clear();
            }
        }

        private void KillBtn_Click(object sender, RoutedEventArgs e)
        {
            var processes = Process.GetProcesses();
            if (!string.IsNullOrWhiteSpace(Txtbox.Text))
            {
                foreach (var item in processes)
                {
                    if (item.ProcessName.Equals(Txtbox.Text))
                    {
                        
                        item.Kill();
                        Txtbox.Clear();
                    }
                }
            } else
            {
                var selected = processList.SelectedItem as string;
                foreach (var item in processes)
                {
                    if (item.ProcessName.Equals(selected))
                    {
                        item.Kill();
                        Txtbox.Clear();
                    }
                }
               
            }
            
        }
    }
}
