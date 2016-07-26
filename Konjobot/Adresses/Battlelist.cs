using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Adresses
{
     public  class Battlelist
    {

         public uint Start = 0xB2F210;
         public uint Step = 0xDC;
         public uint MaxCreatures = 1300;
         public uint End;
         private int baseAddress;
         public Battlelist(Objects.Client client)
         {
             baseAddress = client.Process.MainModule.BaseAddress.ToInt32();
             End = Start + (Step * MaxCreatures);
             
         }
         public void RecalcAddresses()
         {
             Start = Start.RecalAddress(baseAddress);
             End = End.RecalAddress(baseAddress);
         }
        
    }
}
