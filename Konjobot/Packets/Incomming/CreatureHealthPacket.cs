using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KonjoBot.Constants;
using KonjoBot.Objects;

namespace KonjoBot.Packets.Incoming
{
    public class CreatureHealthPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }

        public byte Health { get; set; }

        public CreatureHealthPacket(Client c)
          : base(c)
        {
            this.Type = IncomingPacketType.CreatureHealth;
            this.Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;
            this.Destination = destination;
            this.Type = IncomingPacketType.CreatureHealth;
            this.CreatureId = msg.GetUInt32();
            this.Health = msg.GetByte();
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)this.Type);
            msg.AddUInt32(this.CreatureId);
            msg.AddByte(this.Health);
        }
    }
}
