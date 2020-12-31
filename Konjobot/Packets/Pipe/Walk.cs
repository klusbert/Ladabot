using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
using KonjoBot.Constants;

namespace KonjoBot.Packets.Pipe
{
    class Walk
    {
        public static void Send(Client client,int x, int y)
        {
            OverRide(client,x,y);
            return;
            NetworkMessage msg = new NetworkMessage();
            msg.Position = 0;
            int diag = 0;
            diag = Math.Abs(x) * Math.Abs(y);

            msg.AddByte((byte)PipePacketType.Walk);
            msg.AddByte((byte)diag);
            msg.AddInt16((short)y);
            msg.AddInt16((short)x);
            client.HookProxy.SendPipePacket(msg.Data);
        }
        public static void OverRide(Client client,int x,int y)
        {

         
           byte val = 0;
            if(x == 0 && y == -1)
            {
                val = 0x65;
            }
            if( x== 1 && y == -1)
            {
                val = 0x6A;
            }
            if(x== -1 && y == -1)
            {
                val = 0x6D;
            }
            if(x == 1 && y == 0)
            {
                val = 0x66;
            }
            if(x == 1 && y == 1)
            {
                val = 0x6b;
            }
            if(x == 0 && y == 1)
            {
                val = 0x67;
            }
            if(x == -1 && y == 1)
            {
                val = 0x6C;
            }
            if(x == -1 && y == 0)
            {
                val = 0x68;
            }
            if( val > 0)
            {
                NetworkMessage packet = new NetworkMessage();
                packet.Position = 0;
                packet.AddByte(val);
                client.HookProxy.SendPacketToServer(packet.Data);
            }

         
        }
    }
}
