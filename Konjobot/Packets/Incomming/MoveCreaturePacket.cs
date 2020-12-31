using KonjoBot.Constants;
using KonjoBot.Objects;

namespace KonjoBot.Packets.Incoming
{
    public class MoveCreaturePacket : IncomingPacket
    {
        public byte FromStackPosition { get; set; }

        public Location FromLocation { get; set; }

        public Location ToLocation { get; set; }

        public MoveCreaturePacket(Client c)
          : base(c)
        {
            this.Type = IncomingPacketType.MoveCreature;
            this.Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;
            this.Destination = destination;
            this.Type = IncomingPacketType.MoveCreature;
            uint uint16 = (uint)msg.GetUInt16();
            if (uint16 != (uint)ushort.MaxValue)
            {
                this.FromLocation = new Location((int)uint16, (int)msg.GetUInt16(), (int)msg.GetByte());
                this.FromStackPosition = msg.GetByte();
            }
            else
            {
                int uint32 = (int)msg.GetUInt32();
            }
            this.ToLocation = msg.GetLocation();
            return true;
        }
    }
}