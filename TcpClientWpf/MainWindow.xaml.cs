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
        Client client;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            client = new("127.0.0.1", 8888);
            client.Notify += MessageHandler;
            string username = Username.Text;
            Thread t = new(() => client.Receive(username));
            t.Start();
        }

        private void MessageHandler(string message)
        {
            Dispatcher.Invoke(()=> Chat.Text += message + "\n");
           // Chat.Text += message + "\n";
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            client.Send(Message.Text);
        }
    }
}
