using System;
using System.Net.Sockets;
using System.Text;

namespace MonopolyNetworking.Packets
{
    /// <summary>
    /// First 4 bytes of all packets will be the same:
    /// 0, 1 - packet length; 2, 3 - packet type.
    /// </summary>
    public class Packet
    {
        protected byte[] packet;

        public Packet(byte[] packet)
        {
            this.packet = packet;
        }

        public Packet(ushort length, ushort type)
        {
            packet = new byte[length];
            WriteUshort(length, 0);
            WriteUshort(type, 2);
        }


        public void SendPacketTo(Socket receiver)
        {
            receiver.Send(packet);
        }

        public ushort GetPacketType()
        {
            return ReadUshort(2);
        }

        public ushort GetPacketLength()
        {
            return ReadUshort(0);
        }

        protected void WriteUshort(ushort value, int offset)
        {
            var tempBuffer = BitConverter.GetBytes(value);
            Buffer.BlockCopy(tempBuffer, 0, packet, offset, sizeof(ushort));
        }

        protected ushort ReadUshort(int offset)
        {
            return BitConverter.ToUInt16(packet, offset);
        }

        protected void WriteString(string value, int offset)
        {
            byte[] tempBuffer = Encoding.Unicode.GetBytes(value);
            Buffer.BlockCopy(tempBuffer, 0, packet, offset, tempBuffer.Length);
        }

        protected string ReadString(int offset, int count)
        {
            if(count % 2 > 0)
            {
                throw new ArgumentException("Bytes that represent Unicode string can't be of odd length");
            }
            return Encoding.Unicode.GetString(packet, offset, count);
        }
    }
}
