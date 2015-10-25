using System;
using System.Net;
using System.Net.Sockets;

namespace MonopolyNetworking
{
    public class Client
    {
        public string Name { get; private set; }
        public Socket ClientSocket { get; private set; }
        public ushort Id { get; private set; }
        public EndPoint IPAddress { get; private set; }
    }
}
