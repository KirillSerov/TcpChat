using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TcpChatLib;

namespace TcpClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client _client;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Equals(""))
            {
                Username.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            try
            {
                _client = new("127.0.0.1", 8888);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _client.Notify += MessageHandler;
            string username = Username.Text;
            Thread t = new(() => _client.Receive(username));
            t.Start();
            Connect.IsEnabled = false;
            Disconnect.IsEnabled = true;
            Send.IsEnabled = true;
            BitmapImage newImage = new BitmapImage();
            newImage.BeginInit();
            newImage.UriSource = new Uri(@"pack://application:,,,/Resources/Connected.png");
            newImage.EndInit();
            ConnectionStatus.Source = newImage;
            Username.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        private void MessageHandler(string message)
        {
            Dispatcher.Invoke(() =>
            {
                Chat.Text += message + "\n";
                Chat.ScrollToEnd();
            });

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            _client.Send(Message.Text);
            Message.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _client?.Close();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            _client?.Close();
            Connect.IsEnabled = true;
            Disconnect.IsEnabled = false;
            Send.IsEnabled = false;
            BitmapImage newImage = new BitmapImage();
            newImage.BeginInit();
            newImage.UriSource = new Uri(@"pack://application:,,,/Resources/Disconnected.png");
            newImage.EndInit();
            ConnectionStatus.Source = newImage;
        }
    }
}
