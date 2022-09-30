using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChatLib
{
    public class ClientObject
    {
        public string Username { get; set; }
        public TcpClient Client { get; init; }
        public NetworkStream Stream { get; set; }
        public ClientObject(TcpClient client)
        {
            Client = client;
            Stream = client.GetStream();
        }
        public void Close()
        {
            if(Client != null)
                Client.Close();
        }
    }
}
