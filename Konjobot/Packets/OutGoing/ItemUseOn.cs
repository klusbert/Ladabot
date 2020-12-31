using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Packets.OutGoing
{
  public class ItemUseOn
    {
      public static void Send(Objects.Client client, Objects.Location fromLocation, ushort fromItemId, byte fromStackPostion, Objects.Location toLocation, ushort toItemId, byte toStackPosition)
      {

          NetworkMessage packet = new NetworkMessage();
          packet.Position = 0;
          packet.AddByte(0x83);
          packet.AddBytes(fromLocation.ToBytes());
          packet.AddUInt16(fromItemId);
          packet.AddByte(fromStackPostion);
          packet.AddBytes(toLocation.ToBytes());
          packet.AddUInt16(toItemId);
          packet.AddByte(toStackPosition);

          client.HookProxy.SendPacketToServer(packet.Data);


      }
    }
}
