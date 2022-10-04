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
            try
            {
                _server.Start();
                Notify?.Invoke("Сервер запущен");
                while (true)
                {
                    var client = _server.AcceptTcpClient();
                    ClientObject newClient = new ClientObject(client);
                    
                    _clients.Add(newClient);
                    Thread t = new Thread(() => StartChat(newClient));
                    t.Start();
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                Notify?.Invoke("Сервер: " + ex.Message);
            }
            finally
            {
                Notify?.Invoke("Сервер остановлен");
                _server.Stop();
            }
        }
        private void StartChat(ClientObject client)
        {
            try
            {
                client.Username = Receive(client.Reader, client);
                var helloMessage = $"{client.Username} подключился";
                Notify?.Invoke(helloMessage);
                SendAllClients(helloMessage);
                while (true)
                {
                    var message = $"{client.Username}: {Receive(client.Reader, client)}";
                    Notify?.Invoke(message);
                    SendAllClients(message);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                var byeMessage = $"{client.Username} отключился";
                Notify?.Invoke(byeMessage);
                SendAllClients(byeMessage);
                client.Close();
                _clients.Remove(client);
            }
        }
        private string Receive(StreamReader clientStream, ClientObject client)
        {
            string message = clientStream?.ReadLine();
            return message.ToString();
        }
        private void Send(StreamWriter clientStream, string message)
        {
            clientStream?.WriteLine(message);
        }
        private void SendAllClients(string message)
        {
            foreach (var cl in _clients)
            {
                Send(cl.Writer, message);
            }
        }
    }
}
