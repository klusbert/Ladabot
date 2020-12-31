using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;

namespace KonjoBot.Packets.OutGoing
{
    public class ShopBuy
    {
        public static void Send(Client client, int itemId, byte amount, bool IgnoreCapacity, bool WithBackpack)
        {
        
            NetworkMessage packet = new NetworkMessage();
            packet.Position = 0;
            packet.AddByte(0x7A);
            packet.AddUInt16((ushort)itemId);
            packet.AddByte(0);
            packet.AddByte(amount);
            packet.AddByte(Convert.ToByte(IgnoreCapacity));
            packet.AddByte(Convert.ToByte(WithBackpack));
            client.HookProxy.SendPacketToServer(packet.Data);

        }
    }
}
