using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
using KonjoBot.Constants;
namespace KonjoBot.Packets.OutGoing
{
   public static class Walk
    {
       public static void Send(Client client, WalkDirection direction)
       {
           NetworkMessage packet = new NetworkMessage();
           packet.Position = 0;
           packet.AddByte((byte)direction);
           client.HookProxy.SendPacketToServer(packet.Data);

       }

    }
}
