using KonjoBot.Constants;
using KonjoBot.Objects;

namespace KonjoBot.Packets.Incoming
{
    public class MissileEffect : IncomingPacket
    {
        public MissileEffect(Client c)
          : base(c)
        {
            this.Type = IncomingPacketType.TextMessage;
            this.Destination = PacketDestination.Client;
        }

        public Location FromLocation { get; set; }

        public Location Tolocation { get; set; }

        public ProjectileType Effect { get; set; }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            this.FromLocation = msg.GetLocation();
            this.Tolocation = msg.GetLocation();
            this.Effect = (ProjectileType)msg.GetByte();
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)this.Type);
            msg.AddLocation(this.FromLocation);
            msg.AddLocation(this.Tolocation);
            msg.AddByte((byte)this.Effect);
        }
    }
}
