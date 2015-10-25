using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Net;

namespace MonopolyNetworking
{
    public class ServerSocket : AbstractSocket
    {
        private Socket serverSocket;
        private List<Client> clientList;

        public ServerSocket(int port)
        {
            base.Port = port;
            clientList = new List<Client>();
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }


        public override void Run()
        {
            throw new NotImplementedException();
        }

        private void bind()
        {
            
        }
    }
}
