using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpChatLib
{
    public class Client
    {
        private TcpClient _server;
        public event Action<string> Notify;
        public StreamReader Reader { get; set; }
        public StreamWriter Writer { get; set; }
        public Client(string _host, int port)
        {
            _server = new(_host, port);
            Reader = new StreamReader(_server.GetStream());
            Writer = new StreamWriter(_server.GetStream());
            Writer.AutoFlush = true;
        }

        public void Send(string message)
        {
            Writer?.WriteLine(message);
        }
        public void Receive(string username = "")
        {
            try
            {
                Send(username);
                while (true)
                {
                    string message = Reader.ReadLine();
                    Notify?.Invoke(message);
                }
            }
            catch { }
            finally
            {
                Close();
            }
        }
        public void Close()
        {
            Reader?.Close();
            Writer?.Close();
            _server?.Dispose();
        }
    }
}
