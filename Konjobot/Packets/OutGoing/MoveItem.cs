using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KonjoBot.Objects;

namespace KonjoBot.Packets.OutGoing
{
   public class MoveItem
    {
       public static void Send(Client client, Objects.Location fromLocation, ushort spriteId, byte fromStackPostion, Objects.Location toLocation, byte count)
       {
           NetworkMessage packet = new NetworkMessage();
           packet.Position = 0;
           packet.AddByte(0x78);
           packet.AddBytes(fromLocation.ToBytes());
           packet.AddUInt16(spriteId);
           packet.AddByte(fromStackPostion);
           packet.AddBytes(toLocation.ToBytes());
           packet.AddByte(count);

         client.HookProxy.SendPacketToServer(packet.Data);
           //client.HookProxy.SendToInternal(packet.Data);

       }
    }
}
