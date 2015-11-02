using System;
using MonopolyNetworking.Packets;

namespace MonopolyNetworking.Packet_handlers
{
    public abstract class AbstractPacketHandler
    {
        public void Handle(byte[] buffer)
        {
            Packet packet = new Packet(buffer);
            Handle(packet);
        }

        public void Handle(Packet packet)
        {
            int packetType = packet.GetPacketType();
            switch (packetType)
            {
                case PacketTypes.NEW_CLIENT_PACKET:
                    NewClientConnectedPacket newClientPacket = (NewClientConnectedPacket)packet;
                    handle(newClientPacket);
                    return;
            }

        }

        protected abstract void handle(NewClientConnectedPacket packet);
    }
}
