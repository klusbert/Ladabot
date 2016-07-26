using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KonjoBot.Constants;

namespace KonjoBot.Packets.Pipe
{
    class SetAdressPipe
    {
        public static byte[] CreatePacket(byte type, uint value)
        {
            NetworkMessage msg = new NetworkMessage();
            msg.Position = 0;

            msg.AddByte((byte)PipePacketType.SetAddress);
            msg.AddByte(type);
            msg.AddUInt32(value);
            return msg.Data;

        }
    }
}
