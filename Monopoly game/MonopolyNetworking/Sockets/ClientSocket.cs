using System;
using System.Net;
using System.Net.Sockets;

namespace MonopolyNetworking
{
    public class ClientSocket : AbstractSocket
    {
        private Socket socket;
        byte[] buffer;
        Packet_handlers.ClientSidePacketHandler handler;

        public ClientSocket(string ipAddress, int port)
        {
            base.IP = ipAddress;
            base.Port = port;
            setFieldsUp();
        }

        private void setFieldsUp()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            handler = new Packet_handlers.ClientSidePacketHandler();
        }

        public override void Close()
        {
            socket.Close();
        }

        public override void Run()
        {
            connect();
        }

        private void connect()
        {
            socket.BeginConnect(new IPEndPoint(IPAddress.Parse(IP), Port), connectCallback, null);
        }

        private void connectCallback(IAsyncResult result)
        {
            if (socket.Connected)
            {
                beginReceiving();
            } else
            {
                throw new SocketException(SocketError.NotConnected);
            }
        }

        private void beginReceiving()
        {
            buffer = new byte[1024];
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, receiveCallback, null);
        }

        private void receiveCallback(IAsyncResult result)
        {
            if (socket.Connected)
            {
                receiveAndHandleNewPacket(result);
                beginReceiving();
            } else
            {
                throw new SocketException(SocketError.NotConnected);
            }
        }

        private void receiveAndHandleNewPacket(IAsyncResult result)
        {
            int packetLength = socket.EndReceive(result);
            byte[] packet = new byte[packetLength];
            Buffer.BlockCopy(buffer, 0, packet, 0, packetLength);

            handler.Handle(packet);
        }
    }
}
