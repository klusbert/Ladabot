using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
namespace KonjoBot.Packets.OutGoing
{
    public static class Stop
    {
        public static void Send(Client client)
        {
            NetworkMessage packet = new NetworkMessage();
            packet.Position = 0;
            packet.AddByte((byte)105);
            client.HookProxy.SendPacketToServer(packet.Data);

        }
    }
}
