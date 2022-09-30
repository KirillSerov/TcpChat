using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpChatLib
{
    public class Server
    {
        private TcpListener _server;
        private List<ClientObject> _clients;
        public event Action<string> Notify;
        public Server(string host, int port)
        {
            if (IPAddress.TryParse(host, out IPAddress ipAddress))
                _server = new TcpListener(ipAddress, port);
            else
                _server = new TcpListener(IPAddress.Any, port);
            _clients = new List<ClientObject>();
        }
        public void Connect()
        {
            _server.Start();
            Notify?.Invoke("Сервер запущен");
            try
            {
                while (true)
                {
                    var client = _server.AcceptTcpClient();
                    Notify?.Invoke("Кто-то подключился");
                    ClientObject newClient = new ClientObject(client);
                    _clients.Add(newClient);
                    StartChat(newClient);
                }
            }
            catch (Exception ex)
            {
                Notify?.Invoke("Сервер: " + ex.Message);
            }
            finally
            {
                _server.Stop();
                Notify?.Invoke("Сервер остановлен");
            }
        }
        private void StartChat(ClientObject client)
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    client.Username = Receive(client.Stream, client);
                    while (true)
                    {
                        var message = $"{client.Username}: {Receive(client.Stream, client)}";
                        SendAllClients(client, message);
                    }
                }
                catch (Exception ex)
                {
                    Notify?.Invoke("Сервер: " + ex.Message);
                }
                finally
                {
                    if (client.Stream != null)
                    {
                        client.Stream.Dispose();
                    }
                    client.Close();
                }
            });
            t.Start();
        }
        private string Receive(NetworkStream clientStream, ClientObject client)
        {
            StringBuilder message = new StringBuilder();
            byte[] buffer = new byte[255];
            do
            {
                int bytes = clientStream.Read(buffer, 0, buffer.Length);
                message.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
            } while (clientStream.DataAvailable);
            Notify?.Invoke(message.ToString());
            return message.ToString();
        }
        private void Send(NetworkStream clientStream, string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            clientStream.Write(buffer, 0, buffer.Length);
        }
        private void SendAllClients(ClientObject client, string message)
        {
            foreach (var cl in _clients)
            {
                if (cl != null && cl.Stream != null)
                    Send(cl.Stream, message);
            }
        }
    }
}
