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

        public Client(Socket clientSocket, ushort id)
        {
            this.ClientSocket = clientSocket;
            this.Id = id;
            this.IPAddress = clientSocket.RemoteEndPoint;
        }

        public Client(string name, Socket clientSocket, EndPoint ipAddress)
        {
            this.Name = name;
            this.ClientSocket = clientSocket;
            this.IPAddress = ipAddress;
        }

        public void SetNameAndId(string name, ushort id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}
