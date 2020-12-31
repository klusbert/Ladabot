using KonjoBot.Constants;
using KonjoBot.Objects;
using System;

namespace KonjoBot.Packets.Incoming
{
    public class PlayerDataPacket : IncomingPacket
    {
        public ushort Health { get; set; }

        public int HealthPercent { get; set; }

        public ushort MaxHealth { get; set; }

        public uint Capacity { get; set; }

        public uint TotalCapacity { get; set; }

        public ulong Experience { get; set; }

        public ushort Level { get; set; }

        public byte LevelPercent { get; set; }

        public ushort Mana { get; set; }

        public int ManaPercent { get; set; }

        public ushort MaxMana { get; set; }

        public byte MagicLevel { get; set; }

        public byte BaseMagicLevel { get; set; }

        public byte MagicLevelPercent { get; set; }

        public byte Soul { get; set; }

        public ushort Stamina { get; set; }

        public ushort BaseSpeed { get; set; }

        public ushort Regeneration { get; set; }

        public ushort OfflineTrainingTime { get; set; }

        public PlayerDataPacket(Client c)
          : base(c)
        {
            this.Type = IncomingPacketType.PlayerData;
            this.Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;
            this.Destination = destination;
            this.Type = IncomingPacketType.PlayerData;
            this.Health = msg.GetUInt16();
           
             this.MaxHealth = msg.GetUInt16();
            this.Capacity = msg.GetUInt32();
            this.TotalCapacity = msg.GetUInt32();
            this.Experience = msg.GetUInt64();
            this.Level = msg.GetUInt16();
            this.LevelPercent = msg.GetByte();
            this.Mana = msg.GetUInt16();
            this.MaxMana = msg.GetUInt16();
            this.MagicLevel = msg.GetByte();
            this.BaseMagicLevel = msg.GetByte();
            this.MagicLevelPercent = msg.GetByte();
            this.Soul = msg.GetByte();
            this.Stamina = msg.GetUInt16();
            this.BaseSpeed = msg.GetUInt16();
            this.Regeneration = msg.GetUInt16();
            this.OfflineTrainingTime = msg.GetUInt16();
            this.HealthPercent = (int)Math.Round((double)(100 * (int)this.Health) / (double)this.MaxHealth);
            this.ManaPercent = (int)Math.Round((double)(100 * (int)this.Mana) / (double)this.MaxMana);
           
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)this.Type);
            msg.AddUInt16(this.Health);
            msg.AddUInt16(this.MaxHealth);
            msg.AddUInt32(this.Capacity);
            msg.AddUInt32(this.TotalCapacity);
            msg.AddUInt64(this.Experience);
            msg.AddUInt16(this.Level);
            msg.AddByte(this.LevelPercent);
            msg.AddUInt16(this.Mana);
            msg.AddUInt16(this.MaxMana);
            msg.AddByte(this.MagicLevel);
            msg.AddByte(this.MagicLevelPercent);
            msg.AddByte(this.Soul);
            msg.AddUInt16(this.Stamina);
            msg.AddUInt16(this.BaseSpeed);
            msg.AddUInt16(this.Regeneration);
            msg.AddUInt16(this.OfflineTrainingTime);
        }
    }
}