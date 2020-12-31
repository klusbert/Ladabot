using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;

namespace KonjoBot.Packets.OutGoing
{
   public  class AttackMode
    {
       public static void Send(Client client, byte fightMode, byte chaseMode,byte safemode)
       {
 
          // client.SendPacket.SendPacketToServer((byte)0xA0, (byte)fightMode, (byte)chaseMode, (byte)1);
           NetworkMessage packet = new NetworkMessage();
           packet.Position = 0;
           packet.AddByte(0xA0);
           packet.AddByte(fightMode);
           packet.AddByte(chaseMode);
           packet.AddByte(safemode);
           client.HookProxy.SendPacketToServer(packet.Data);

       }
    }
}
