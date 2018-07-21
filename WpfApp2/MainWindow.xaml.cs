using System.Windows;
using Clients;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ip = "";
        public MainWindow()
        {
            
            InitializeComponent();
            TxtBoxCommand.Text = "Commands :\nCommand 'Show' - show all processes. \n" +
                "Command 'Start-App Name'- Run Application. \n" +
                "Command 'Kill- App Name' - Kill Aplication,or select process for killing \n" +
                "DEFAULT IP ADDRESS is 127.0.0.1";
        }


        public void ProcessesList(string cmd)
        {
            try
            {
                string processes;
                if (string.IsNullOrEmpty(ip))
                {
                    processes = Client.SendMessage(cmd);
                }
                else
                    processes = Client.SendMessage(cmd, ip);
                
                if (processList.Items.Count != 0)
                {
                    processList.Items.Clear();
                }
                foreach (var item in processes.Split('\n'))
                {
                    processList.Items.Add(item);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }


        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (!string.IsNullOrWhiteSpace(Txtbox.Text) && (processList.SelectedItem == null || processList.Items == null))
            {
                ProcessesList(Txtbox.Text);
            }
            else if( processList.SelectedItem != null)
            {
                var a = "kill-" + processList.SelectedItem as string;
                ProcessesList(a);
                processList.SelectedItem = null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IpTextBlock.Visibility = Visibility.Hidden;
            IpTextbox.Visibility = Visibility.Hidden;
            ApplyBtn.Visibility = Visibility.Hidden;
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            ip = IpTextbox.Text;
            IpTextBlock.Visibility = Visibility.Hidden;
            IpTextbox.Visibility = Visibility.Hidden;
            ApplyBtn.Visibility = Visibility.Hidden;
        }

        private void InputBtn_Click(object sender, RoutedEventArgs e)
        {
            IpTextBlock.Visibility = Visibility.Visible;
            IpTextbox.Visibility = Visibility.Visible;
            ApplyBtn.Visibility = Visibility.Visible;
        }
    }
}
