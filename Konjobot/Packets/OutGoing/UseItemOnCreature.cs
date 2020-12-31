using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Packets.OutGoing
{
    public class ItemUseOnCreature
    {
        public static void Send(Objects.Client client, Objects.Location fromLocation, ushort itemId, byte fromStack, uint creatureId)
        {

            NetworkMessage packet = new NetworkMessage();
            packet.Position = 0;
            packet.AddByte(0x84);
            packet.AddLocation(fromLocation);
            packet.AddUInt16(itemId);
            packet.AddByte(fromStack);
            packet.AddUInt32(creatureId);

            client.HookProxy.SendPacketToServer(packet.Data);


        }
    }
}
