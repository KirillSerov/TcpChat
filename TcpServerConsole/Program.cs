using System;
using TcpChatLib;

namespace TcpServerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new("127.0.0.1", 8888);
            server.Notify += MessageHandler;
            server.Connect();
        }
        static void MessageHandler(string message)
        {
            Console.WriteLine(message);
        }
    }
}
