using KonjoBot.Constants;
using KonjoBot.Objects;

namespace KonjoBot.Packets.Incoming
{
    public class TextMessagePacket : IncomingPacket
    {
        public string Text { get; set; }

        public byte Mode { get; set; }

        public Location Location { get; set; }

        public uint NumValue { get; set; }

        public byte NumColor { get; set; }

        public uint PhysicalDmgValue { get; set; }

        public byte PhysicalDmgColor { get; set; }

        public uint MagicDmgValue { get; set; }

        public byte MagicDmgColor { get; set; }

        public ushort ChannelId { get; set; }

        public TextMessagePacket(Client c)
          : base(c)
        {
            this.Type = IncomingPacketType.TextMessage;
            this.Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;
            this.Destination = destination;
            this.Type = IncomingPacketType.TextMessage;
            this.Mode = msg.GetByte();
            switch (this.Mode)
            {
                case 23:
                case 24:
                case 27:
                    this.Location = msg.GetLocation();
                    this.PhysicalDmgValue = msg.GetUInt32();
                    this.PhysicalDmgColor = msg.GetByte();
                    this.MagicDmgValue = msg.GetUInt32();
                    this.MagicDmgColor = msg.GetByte();
                    break;
                case 25:
                case 26:
                case 28:
                case 29:
                    this.Location = msg.GetLocation();
                    this.NumValue = msg.GetUInt32();
                    this.NumColor = msg.GetByte();
                    break;
            }
            this.Text = msg.GetString();
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)this.Type);
            msg.AddByte(this.Mode);
            switch (this.Mode)
            {
                case 23:
                case 24:
                case 27:
                    msg.AddLocation(this.Location);
                    msg.AddUInt32(this.PhysicalDmgValue);
                    msg.AddByte(this.PhysicalDmgColor);
                    msg.AddUInt32(this.MagicDmgValue);
                    msg.AddByte(this.MagicDmgColor);
                    break;
                case 25:
                case 26:
                case 28:
                case 29:
                    msg.AddLocation(this.Location);
                    msg.AddUInt32(this.NumValue);
                    msg.AddByte(this.NumColor);
                    break;
            }
            msg.AddString(this.Text);
        }
    }
}