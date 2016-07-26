using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KonjoBot.Objects;
using KonjoBot.Constants;
namespace KonjoBot.Packets.Pipe
{
    class AddTextPipe
    {
        public static void Send(Client client, ushort font, string text, ushort r, ushort b, ushort g, ushort posX, ushort posy, string textname)
        {
            NetworkMessage msg = new NetworkMessage();
            msg.Position = 0;

            msg.AddByte((byte)PipePacketType.DisplayTextPipe);
            msg.AddString(textname);
            msg.AddUInt16(posX);
            msg.AddUInt16(posy);
            msg.AddUInt16(r);
            msg.AddUInt16(g);
            msg.AddUInt16(b);
            msg.AddUInt16(font);
            msg.AddString(text);
            client.HookProxy.SendPipePacket(msg.Data);


        }
    }
}
