using MonopolyNetworking.Packets;
using System;

namespace MonopolyNetworking.Packet_handlers
{
    class ServerSidePacketHandler : AbstractPacketHandler
    {
        private ServerSocket serverSocket;

        public ServerSidePacketHandler(ServerSocket serverSocket)
        {
            this.serverSocket = serverSocket;
        }

        protected override void handle(NewClientConnectedPacket packet)
        {
            // TODO : Write packet handling.
        }
    }
}
