using KonjoBot.Constants;
using KonjoBot.Objects;

namespace KonjoBot.Packets.Incoming
{
    public class DeleteInContainerPacket : IncomingPacket
    {
        public byte ContainerId { get; set; }

        public byte Slot { get; set; }

        public DeleteInContainerPacket(Client c)
          : base(c)
        {
            this.Type = IncomingPacketType.DeleteInContainer;
            this.Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;
            this.Destination = destination;
            this.Type = IncomingPacketType.DeleteInContainer;
            this.ContainerId = msg.GetByte();
            this.Slot = msg.GetByte();
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)this.Type);
            msg.AddByte(this.ContainerId);
            msg.AddByte(this.Slot);
        }
    }
}
