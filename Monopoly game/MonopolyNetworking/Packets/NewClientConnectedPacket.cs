using System;

namespace MonopolyNetworking.Packets
{
    /// <summary>
    /// CODE OF THIS PACKET = 1000
    /// This packet will have the following structure:
    /// First 4 bytes: standart.
    /// All other bytes will contain the clients name.
    /// </summary>
    public class NewClientConnectedPacket : Packet
    {
        public string ClientsName
        {
            get { return ReadString(4, packet.Length - 4); }
            set 
            {
                WriteString(value, 4);
            }
        }

        public NewClientConnectedPacket(byte[] packet)
            : base(packet) { }
        public NewClientConnectedPacket(string clientsName) 
            : base((ushort)(clientsName.Length * 2 + 4), 1000)
        {
            ClientsName = clientsName;
        }
    }
}
