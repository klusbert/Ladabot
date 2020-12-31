using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Adresses
{
   public class Client
    {

       //10.90
       public uint GuiAddress = 0x934970;
       public uint Status = 0x946168;
       public uint PeekMessage = 0x822884;
       public uint LastSeendId = 0xACE434;
       public uint StatusbarText = 0x987DD0;
       public uint StatusbarTime = 0x987DC0;
       public uint Ping = 0xAd2e18;
       public uint Hunger = 0x93B75C;
    
       public uint McAddress;
       private int baseAddress;
       public Client (Objects.Client client)     
       {
           baseAddress = client.Process.MainModule.BaseAddress.ToInt32();
       }
       public void RecalcAddresses()
       {
           GuiAddress = GuiAddress.RecalAddress(baseAddress);
           Status = Status.RecalAddress(baseAddress);
           PeekMessage = PeekMessage.RecalAddress(baseAddress);
           LastSeendId = LastSeendId.RecalAddress(baseAddress);
           StatusbarText = StatusbarText.RecalAddress(baseAddress);
           StatusbarTime = StatusbarTime.RecalAddress(baseAddress);
           Ping = Ping.RecalAddress(baseAddress);
           Hunger = Hunger.RecalAddress(baseAddress);
           Objects.Client.McAddress = McAddress.RecalAddress(baseAddress);
   
       }

    }
}
