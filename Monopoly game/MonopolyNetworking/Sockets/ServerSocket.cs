using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Net;
using MonopolyNetworking.Packet_handlers;

namespace MonopolyNetworking
{
    public class ServerSocket : AbstractSocket
    {
        private static ServerSocket serverInstance;
        private static Socket serverSocket;
        private byte[] buffer;
        private List<Client> clientList;
        private ServerSidePacketHandler handler;

        public static ushort IDCounter;

        private ServerSocket(int port)
        {
            base.Port = port;
            clientList = new List<Client>();
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IDCounter = 0;
            handler = new ServerSidePacketHandler(serverInstance);
        }

        public static ServerSocket GetServerSocket(int port)
        {
            if (serverInstance == null)
            {
                serverInstance = new ServerSocket(port);
            }

            return serverInstance;
        }

        public override void Close()
        {
            serverSocket.Close();
        }

        public override void Run()
        {
            bindAndListen();
            accept();
        }

        private void bindAndListen()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, base.Port));
            serverSocket.Listen(50);
        }

        private void accept()
        {
            serverSocket.BeginAccept(acceptCallback, null);
        }

        /// <summary>
        /// This method will be called when socket accepts a client.
        /// </summary>
        /// <param name="ar"></param>
        private void acceptCallback(IAsyncResult result)
        {
            Socket tempClientSocket = null;
            try
            {
                acceptClientAndAddToList(result, ref tempClientSocket);
                beginReceivingFromClient(tempClientSocket);
            } catch (SocketException)
            {
                handleSocketException(tempClientSocket);
            }
        }

        private void acceptClientAndAddToList(IAsyncResult result, ref Socket tempClientSocket)
        {
            tempClientSocket = serverSocket.EndAccept(result);
            Client tempClient = new Client(tempClientSocket, ServerSocket.IDCounter++);
            clientList.Add(tempClient);
        }

        private void beginReceivingFromClient(Socket clientSocket)
        {
            buffer = new byte[1024];
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, receiveCallback, clientSocket);
        }

        private void receiveCallback(IAsyncResult result)
        {
            Socket clientSocket = result.AsyncState as Socket;
            try
            {
                receivePacketAndHandle(clientSocket, result);
                beginReceivingFromClient(clientSocket);
            } catch (SocketException)
            {
                handleSocketException(clientSocket);
            }
        }

        private void receivePacketAndHandle(Socket clientSocket, IAsyncResult result)
        {
            int packetLen = clientSocket.EndReceive(result);
            byte[] packet = new byte[packetLen];
            Buffer.BlockCopy(buffer, 0, packet, 0, packetLen);

            handler.Handle(packet);
        }

        private void handleSocketException(Socket tempClientSocket)
        {
            if (!tempClientSocket.Connected)
            {
                removeClient(tempClientSocket.RemoteEndPoint);
            }
        }

        private void removeClient(EndPoint remoteEndPoint)
        {
            var index = getClientsPosition(remoteEndPoint);
            if (index != -1)
            {
                clientList.RemoveAt(index);
            }
        }

        private int getClientsPosition(EndPoint remoteEndPoint)
        {
            for (int i = 0; i < clientList.Count; i++)
            {
                if (remoteEndPoint.Equals(clientList[i].IPAddress))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
