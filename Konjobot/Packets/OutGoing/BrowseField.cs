using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;

namespace KonjoBot.Packets.OutGoing
{
    public class BrowseField
    {
        public static void Send(Client client,Location location)
        {

            // client.SendPacket.SendPacketToServer((byte)0xA0, (byte)fightMode, (byte)chaseMode, (byte)1);
            NetworkMessage packet = new NetworkMessage();
            packet.Position = 0;
            packet.AddByte(0xCB);
            packet.AddLocation(location);
            client.HookProxy.SendPacketToServer(packet.Data);

        }
    }
}
