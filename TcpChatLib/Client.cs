using System;
using System.Collections.Generic;
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
        private NetworkStream _stream;
        public Client(string _host, int port)
        {
            _server = new(_host, port);
            _stream = _server.GetStream();
        }

        public void Send(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            _stream.Write(buffer, 0, buffer.Length);
        }
        public void Receive(string username = "")
        {
            Send(username);
            while (true)
            {
                StringBuilder message = new StringBuilder();
                do
                {
                    byte[] buffer = new byte[255];
                    int bytes = _stream.Read(buffer, 0, buffer.Length);
                    message.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                } while (_stream.DataAvailable);
                Notify?.Invoke(message.ToString());
            }
        }
    }
}
