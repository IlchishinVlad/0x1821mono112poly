using MonopolyNetworking.Packets;
using System;

namespace MonopolyNetworking.Packet_handlers
{
    class ClientSidePacketHandler : AbstractPacketHandler
    {
        protected override void handle(NewClientConnectedPacket packet)
        {
            throw new NotImplementedException();
        }
    }
}
