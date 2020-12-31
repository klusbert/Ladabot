using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Packets.OutGoing
{
   public class ItemUse
    {
       public static  void Send(Objects.Client client, Objects.Location fromPosition, ushort itemId, byte fromStack, byte index)
       {
           NetworkMessage packet = new NetworkMessage();
           Objects.Item item = new Objects.Item(Core.client, itemId);
         
           packet.Position = 0;
           packet.AddByte(0x82);
           packet.AddBytes(fromPosition.ToBytes());
           packet.AddUInt16(itemId);
           packet.AddByte(fromStack);
           packet.AddByte(index);

           client.HookProxy.SendPacketToServer(packet.Data);
          // client.HookProxy.SendToInternal(packet.Data);

       }
    }

}
