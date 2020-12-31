using KonjoBot.Constants;
using KonjoBot.Objects;
using System.Collections.Generic;

namespace KonjoBot.Packets.Incoming
{
    public class OpenContainerPacket : IncomingPacket
    {
        public ushort ItemId { get; set; }

        public string Name { get; set; }

        public byte Index { get; set; }

        public byte Capacity { get; set; }

        public byte HasParent { get; set; }

        public byte ItemCount { get; set; }

        public List<Item> Items { get; set; }

        public OpenContainerPacket(Client c)
          : base(c)
        {
            this.Type = IncomingPacketType.OpenContainer;
            this.Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            this.Index = msg.GetByte();
            this.ItemId = msg.GetUInt16();
            this.Name = msg.GetString();
            this.Capacity = msg.GetByte();
            this.HasParent = msg.GetByte();
            int num1 = (int)msg.GetByte();
            int num2 = (int)msg.GetByte();
            this.ItemCount = msg.GetByte();
            this.Items = new List<Item>();
            for (int index = 0; index < (int)this.ItemCount; ++index)
            {
                Item obj = new Item(this.Client, (int)msg.GetUInt16());
                if (obj.HasExtraByte)
                    obj.Count = (int)msg.GetByte();
                obj.Location = ItemLocation.FromContainer(this.Index, (byte)index);
                this.Items.Add(obj);
            }
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)this.Type);
            msg.AddByte(this.Index);
            msg.AddUInt16(this.ItemId);
            msg.AddString(this.Name);
            msg.AddByte(this.Capacity);
            msg.AddByte(this.HasParent);
            msg.AddByte((byte)this.Items.Count);
            foreach (Item obj in this.Items)
                msg.AddItem(obj);
        }
    }
}
