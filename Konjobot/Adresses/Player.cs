using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Adresses
{
   public class Player
    {  
        //properties 1
        // for flags search for 16384 in pz 0 if nothing

       public uint Flags = 0x9346C4;
       public uint Xor = 0x934658;   
       public uint Experince = 0x934660;
       public uint Level = 0x934670;
       public uint Soul;
       public uint Mana = 0x934688;
       public uint ManaMax = 0x93465C;

       public uint HP = 0xAD2030;
       public uint Cap = 0xAD201C;
       public uint Id = 0xAD202C;
       public uint X = 0xAD2038;
       public uint Y = 0xAD203C;
       public uint Z = 0xAD2040;

       public uint AttackCount = 0xAD2DD8;
       public uint RedSquare = 0x934684;

        // search "MOV ECX,0A1h" attack
       private int baseAddress;
       public Player(Objects.Client client)
       {
           baseAddress = client.Process.MainModule.BaseAddress.ToInt32();
         
      
       }
       public void RecalcAddresses()
       {
           Flags = Flags.RecalAddress(baseAddress);
           Xor = Xor.RecalAddress(baseAddress);
           ManaMax = ManaMax.RecalAddress(baseAddress);
           Experince = Experince.RecalAddress(baseAddress);
           Level = Level.RecalAddress(baseAddress);
           Soul = Flags + 0x84;
           Mana = Mana.RecalAddress(baseAddress);
           HP = HP.RecalAddress(baseAddress);
           Cap = Cap.RecalAddress(baseAddress);
           Id = Id.RecalAddress(baseAddress);
           X = X.RecalAddress(baseAddress);
           Y = Y.RecalAddress(baseAddress);
           Z = Z.RecalAddress(baseAddress);
           AttackCount = AttackCount.RecalAddress(baseAddress);
           RedSquare = RedSquare.RecalAddress(baseAddress);

       }



    }
}
