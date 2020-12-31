using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Objects
{
  public  class Player: Creature
    {
        public Player(Client cl, uint adr)
        : base(cl, adr) { }

      public int AttackCount
        {
            get
            {
                return client.Memory.ReadInt32(client.Addresses.Player.AttackCount);

            }
            set
            {
                client.Memory.WriteInt32(client.Addresses.Player.AttackCount, value);
            }
        }
        public int RedSquare
        {
            get
            {
                return client.Memory.ReadInt32(client.Addresses.Player.RedSquare);
            }
            set
            {
                client.Memory.WriteInt32(client.Addresses.Player.RedSquare, value);
            }
        }
        public int Mana
        {
            get
            {
                return client.Memory.ReadInt32(client.Addresses.Player.Mana) ^ XorKey;
            }
        }
        public int HitPoints
        {
            get
            {
                return client.Memory.ReadInt32(client.Addresses.Player.HP) ^ XorKey;
            }
        }
      public int ManaMax
        {
            get
            {
                return client.Memory.ReadInt32(client.Addresses.Player.ManaMax) ^ XorKey;
            }
        }
      public int ManaPercent
      {
          get
          {
              double db = (double)Mana / (double)ManaMax;
              double res = db * 100;

              return (int)res;

          }
      }
        public int XorKey
        {
            get
            {
                return client.Memory.ReadInt32(client.Addresses.Player.Xor);
            }
        }     
      public int Capacity
        {
            get
            {
                return ( client.Memory.ReadInt32(client.Addresses.Player.Cap) ^ XorKey) /100; 

            }
        }
      public int Level
      {
          get { return client.Memory.ReadInt32(client.Addresses.Player.Level); }
      }
      public int Experience
      {
          get { return client.Memory.ReadInt32(client.Addresses.Player.Experince); }
      }
      public int ExpLeft
      {
          get
          {
              int levelNeeded = Convert.ToInt32(Level + 1);
              double expNeeded = ((50.0 / 3.0) * Math.Pow(levelNeeded, 3)) - (100.0 * Math.Pow(levelNeeded, 2)) + ((850.0 / 3.0) * levelNeeded) - 200;
              long expToGo = Convert.ToInt64(Convert.ToInt64(Math.Truncate(expNeeded)) - Experience);
              return Convert.ToInt32(expToGo);
          }
      }
      public Location GoTo
      {
          set { 
              
             client.MiniMap.Goto(value); 
              
           /*   client.Memory.WriteInt32(Adresses.Player.GotoX, value.X);
              client.Memory.WriteInt32(Adresses.Player.GotoY, value.Y);
              client.Memory.WriteInt32(Adresses.Player.GotoZ, value.Z);
              this.IsWalking = true;
              */

          }
      }
    }
}
    