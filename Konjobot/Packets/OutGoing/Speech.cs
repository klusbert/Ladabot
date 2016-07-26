using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KonjoBot.Objects;
namespace KonjoBot.Packets.OutGoing
{
   public class Speech
    {
       public static void SendConsole(Client client,string message)
       {

           NetworkMessage msg = new NetworkMessage();
           msg.Position = 0;
           msg.AddByte(0x96);
           msg.AddByte(0x1);
           msg.AddString(message);
            client.HookProxy.SendPacketToServer(msg.Data);
           // client.HookProxy.SendToInternal(msg.Data);

       }
       public static void SendToNpc(Client client, string message)
       {
        
           NetworkMessage msg = new NetworkMessage();
           msg.Position = 0;
           msg.AddByte(0x96);
           msg.AddByte(0xC);//packet
          // msg.AddByte(0x4);//packet
           msg.AddString(message);
            client.HookProxy.SendPacketToServer(msg.Data);
            //client.HookProxy.SendToInternal(msg.Data);
       }
    }
}
