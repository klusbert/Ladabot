
using KonjoBot.Constants;
using KonjoBot.Objects;
using System;

namespace KonjoBot.Packets
{
    public class IncomingPacket : Packet
    {
        public IncomingPacketType Type { get; set; }

        public IncomingPacket(Client c)
          : base(c)
        {
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)this.Type);
            throw new NotImplementedException();
        }
    }
}
