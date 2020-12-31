using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;

namespace KonjoBot.Packets.OutGoing
{
    public class CloseContainer
    {
        public static void Send(Client client,int ContainerIndex)
        {

            NetworkMessage packet = new NetworkMessage();
            packet.Position = 0;
            packet.AddByte(0x87);
            packet.AddByte((byte)ContainerIndex);
            client.HookProxy.SendPacketToServer(packet.Data);

        }
    }
}
