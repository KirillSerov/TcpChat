using System;
using System.Collections.Generic;
using System.IO;
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
        public StreamReader Reader { get; set; }
        public StreamWriter Writer { get; set; }
        public ClientObject(TcpClient client)
        {
            Client = client;
            Reader = new StreamReader(client.GetStream());
            Writer = new StreamWriter(client.GetStream());
            Writer.AutoFlush = true;
        }
        public void Close()
        {
            if(Client != null)
                Client.Close();
        }
    }
}
