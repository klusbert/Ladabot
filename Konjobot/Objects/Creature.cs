using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Objects
{
  public  class Creature
    {
       public Client client;
        uint m_adress;
        public int Cost;
        public Creature(Client cl, uint adress)
        {
            client = cl;
            m_adress = adress;

        }
        public void Attack()
        {
            client.Player.RedSquare = this.Id;
            client.Player.AttackCount += 1;
            Packets.OutGoing.Attack.Send(client, (uint)this.Id, (uint)client.Player.AttackCount);        
        }
          public byte Type
        {
            get
            {
                return client.Memory.ReadByte(m_adress + 3);
            }
        }
          public bool isPlayer1()
          {
              if(Type != 64)
              {
                  return true;
              }
              return false;
          }
          public bool isPlayer()
          {
              if (this.Id < 0x40000000)
              {
                  return true;

              }
              return false;

          }
        public bool IsReachable()
        {
            var tileList = client.Map.GetTilesSameFloor();
            var playerTile = client.Map.GetPlayerTile();
            var creatureTile = tileList.GetTileWithCreature((uint)Id);

            if (playerTile == null || creatureTile == null)
                return false;

            int xDiff, yDiff;
            uint playerZ = (uint)client.PlayerLocation.Z;
            var creatures = client.Battlelist.GetCreatures().Where(c => c.Z == playerZ);
            uint playerId = client.Memory.ReadUInt32(client.Addresses.Player.Id);

            xDiff = (int)playerTile.MemoryLocation.X - 8;
            yDiff = (int)playerTile.MemoryLocation.Y - 6;

            playerTile.MemoryLocation = AdjustLocation(playerTile.MemoryLocation, xDiff, yDiff);
            creatureTile.MemoryLocation = AdjustLocation(creatureTile.MemoryLocation, xDiff, yDiff);

            foreach (Tile tile in tileList)
            {
                tile.MemoryLocation = AdjustLocation(tile.MemoryLocation, xDiff, yDiff);

                if (tile.Ground.ItemData.Blocking || tile.Ground.ItemData.BlocksPath ||
                    tile.Items.Any(i => i.ItemData.Blocking || i.ItemData.BlocksPath || client.PathFinder.ModifiedItems.ContainsKey((uint)i.Id)) ||
                    creatures.Any(c => tile.Objects.Any(o => o.Data == c.Id && o.Data != playerId && o.Data != this.Id)))
                {
                    client.PathFinder.Grid[tile.MemoryLocation.X, tile.MemoryLocation.Y] = 0;
                }
                else
                {
                    client.PathFinder.Grid[tile.MemoryLocation.X, tile.MemoryLocation.Y] = 1;
                }
            }
            client.PathFinder.Grid[creatureTile.MemoryLocation.X, creatureTile.MemoryLocation.Y] = 1;
            return client.PathFinder.FindPath(playerTile.MemoryLocation, creatureTile.MemoryLocation);
        }
        private static Location AdjustLocation(Location loc, int xDiff, int yDiff)
        {
            int xNew = loc.X - xDiff;
            int yNew = loc.Y - yDiff;

            if (xNew > 17)
                xNew -= 18;
            else if (xNew < 0)
                xNew += 18;

            if (yNew > 13)
                yNew -= 14;
            else if (yNew < 0)
                yNew += 14;

            return new Location(xNew, yNew, loc.Z);
        }

        public uint Address
        {
            get
            {
                return m_adress;
            }
        }
        public int Id
        {
            get
            {
                return client.Memory.ReadInt32(Address + Adresses.Creature.DistanceId);
            }
        }
        public string Name
        {
            get
            {
                return client.Memory.ReadString(Address + Adresses.Creature.DistanceName);
            }
        }
        public int X
        {
            get { return client.Memory.ReadInt32(Address + Adresses.Creature.DistanceX); }
        }
        public int Y
        {
            get { return client.Memory.ReadInt32(Address + Adresses.Creature.DistanceY); }
        }
        public int Z
        {
            get { return client.Memory.ReadInt32(Address + Adresses.Creature.DistanceZ); }
        }
        public Constants.Direction Direction
        {
            get { return (Constants.Direction)client.Memory.ReadInt32(Address + Adresses.Creature.DistanceFaceDirection); }
            set { client.Memory.WriteInt32(Address + Adresses.Creature.DistanceFaceDirection, (int)value); }
        }
        public Location Location
        {
            get
            {
                return new Location(X, Y, Z);
            }
        }
        public bool IsOnScreen
        {
            get
            {
                int xdiff, ydiff;
                if (Z != client.PlayerLocation.Z)
                {
                    return false;
                }
                xdiff = this.X - client.PlayerLocation.X;
                ydiff = this.Y - client.PlayerLocation.Y;
                if (xdiff <= 7 && xdiff >= -7 && ydiff<= 5 && ydiff >= -5)
                {
                    return true;
                }

                return false;

            }
        }
        public bool IsWalking
        {
            get
            {
             return Convert.ToBoolean(client.Memory.ReadByte(Address + Adresses.Creature.DistanceIsWalking));
             }
            set
            {
                client.Memory.WriteByte(Address + Adresses.Creature.DistanceIsWalking, Convert.ToByte(value));
            }
        }
        public bool IsVisible
        {
            get
            {
                return Convert.ToBoolean(client.Memory.ReadByte(Address + Adresses.Creature.DistanceIsVisible));
            }
        }
        public int HP
        {
            get { return client.Memory.ReadInt32(Address + Adresses.Creature.DistanceHPBar); }
        }
        public int MoveMentSpeed
        {
            get { return client.Memory.ReadInt32(Address + Adresses.Creature.DistanceWalkSpeed); }
        }
        public Bot.Target Target()
        {
            return this.GetTarget();
        }
 
                 
    }
}
