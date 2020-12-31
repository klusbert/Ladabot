using KonjoBot.Constants;
using KonjoBot.Objects;

namespace KonjoBot.Packets.Incoming
{
    public class TileAddThing : IncomingPacket
    {
        public TileAddThing(Client c)
          : base(c)
        {
            this.Type = IncomingPacketType.TextMessage;
            this.Destination = PacketDestination.Client;
        }

        public Item Item { get; set; }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            Location location = msg.GetLocation();
            byte stack = (byte)((uint)msg.GetByte() - 1U);
            this.Item = new Item(this.Client, (int)msg.GetUInt16());
            this.Item.Location = ItemLocation.FromLocation(location, stack);
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
        }
    }
}
