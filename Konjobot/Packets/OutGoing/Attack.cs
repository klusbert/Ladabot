using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;

namespace KonjoBot.Packets.OutGoing
{
    public class Attack
    {
        public static void Send(Client client,uint creatureId,uint AttackCount)
        {
                    
            NetworkMessage packet = new NetworkMessage();
            packet.Position = 0;
            packet.AddByte(0xA1);
            packet.AddUInt32(creatureId);
            packet.AddUInt32(AttackCount);
          // client.HookProxy.SendPacketToServer(packet.Data);
            client.HookProxy.SendToInternal(packet.Data);
        }
    }
}
