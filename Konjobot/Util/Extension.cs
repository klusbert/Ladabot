using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KonjoBot.Objects;
using System.Drawing;
namespace KonjoBot
{
   public static class Extension
    {
       public static uint RecalAddress(this uint address, int baseAddress)
       {
           int MyBase = 0x400000;
           int off = (int)address - MyBase;
           return (uint)(off + baseAddress);
       }
       public static bool HasCreature(this Tile t)
       {
           bool ret = false;
           foreach (Item i in t.Items)
           {
               if(i.Id == 99)
               {
                   ret = true;
               }
           }
           return ret;
       }
   
       public static bool IsShootable(this Location location, Client client,Location fromLocation )
       {
           int XSign = (location.X > fromLocation.X) ? 1 : -1;
           int YSign = (location.Y > fromLocation.Y) ? 1 : -1;
           double XDistance = Math.Abs(location.X - fromLocation.X);
           double YDistance = Math.Abs(location.Y - fromLocation.Y);
           double max = location.DistanceTo(fromLocation);
           Location check;

           // This checks if location is on viewable screen, someone might to remove that for some reason
           if (Math.Abs(XDistance) > 8 || Math.Abs(YDistance) > 5)
           {
               return false;
           }

           for (int i = 1; i <= max; i++)
           {
               check = fromLocation.Offset((int)Math.Ceiling(i * XDistance / max) * XSign, (int)Math.Ceiling(i * YDistance / max) * YSign, 0);
               Tile tile = client.Map.GetTile(check);

               if (tile != null)
               {
                   if (tile.Ground.ItemData.BlocksMissiles)
                   {
                       return false;
                   }

                   Item item = tile.Items.FirstOrDefault(tileItem => tileItem.ItemData.BlocksMissiles);

                   if (item != null)
                   {
                       return false;
                   }
               }
           }

           return true;
       }
       public static Tile Tile(this Location loc )
       {
           if (loc.Z == Core.client.PlayerLocation.Z)
           {
               return Core.client.Map.GetTile(loc);
           }
           return null;
       }
        public static byte Low(this int value)
        {
            return BitConverter.GetBytes(value)[0];
        }
              
        public static byte High(this int value)
        {
            return BitConverter.GetBytes(value)[1];
        }
     public static string AutoMapPath = "C:\\Users\\Daniel\\AppData\\Roaming\\Tibia\\Automap\\";
           public static int RoundOff(this int i)
           {
               return ((int)Math.Round(i / 10.0)) * 10;
           }

           public static Objects.Bot.Target GetTarget(this Creature creature)
           {
               Objects.Bot.Target tr = Core.Global.TargetList.FirstOrDefault(t => t.Name.ToLower() == creature.Name.ToLower());
               return tr;
           }
        public static Location ToMemoryLocation1(this Location worldLocation, Tile playerTile, Client client)
        {
            Location globalPlayerLoc = client.PlayerLocation;
            Location localPlayerLoc = playerTile.MemoryLocation;
            int xAdjustment = globalPlayerLoc.X - localPlayerLoc.X;
            int yAdjustment = globalPlayerLoc.Y - localPlayerLoc.Y;
            int zAdjustment = globalPlayerLoc.Z - localPlayerLoc.Z;

            return new Location(worldLocation.X - xAdjustment, worldLocation.Y - yAdjustment, worldLocation.Z - zAdjustment);
        }
        public static Location ToMemoryLocation(this Location worldLocation, Tile playerTile, Client client)
        {


            Objects.Location playerMemLoc = playerTile.MemoryLocation ;



            // get and apply diffs

            int diffX = worldLocation.X - playerTile.Location.X;

            int diffY = worldLocation.Y - playerTile.Location.Y;

            int diffZ = worldLocation.Z - playerTile.Location.Z;

            Objects.Location memLoc = playerMemLoc.Offset(diffX, diffY, diffZ);



            // re-align values if they're out of range

            if (memLoc.X < 0) memLoc.X += (int)client.Addresses.Map.MaxX;

            else if (memLoc.X >= client.Addresses.Map.MaxX) memLoc.X -= (int)client.Addresses.Map.MaxX;

            if (memLoc.Y < 0) memLoc.Y += (int)client.Addresses.Map.MaxY;

            else if (memLoc.Y >= (int)client.Addresses.Map.MaxY) memLoc.Y -= (int)client.Addresses.Map.MaxY;

            return memLoc;
        }
        public static uint[] ToUInt32Array(this byte[] bytes)
        {
            if (bytes.Length % 4 > 0)
                throw new Exception();

            uint[] temp = new uint[bytes.Length / 4];

            for (int i = 0; i < temp.Length; i++)
                temp[i] = BitConverter.ToUInt32(bytes, i * 4);

            return temp;
        }
        public static Objects.Tile GetTileWithCreature(this IEnumerable<Objects.Tile> tiles, uint creatureId)
        {
            return tiles.FirstOrDefault(t => t.Objects.Any(
                    o => o.Id == 0x63 && o.Data == creatureId));
        }
        public static bool IsOnScreen(this Location loc, Client client)
        {
            int xdiff, ydiff;
            if (loc.Z != client.PlayerLocation.Z)
            {
                return false;
            }
            xdiff = loc.X - client.PlayerLocation.X;
            ydiff = loc.Y - client.PlayerLocation.Y;
            if (xdiff <= 7 && xdiff >= -7 && ydiff <= 5 && ydiff >= -5)
            {
                return true;
            }

            return false;
        }
       public static bool IsDiagonal(this Location fromLocation,Location toLocation)
        {
            int xDiff = fromLocation.X - toLocation.X;
            int ydiff = fromLocation.Y - toLocation.Y;
            if (xDiff == 1 && ydiff == 1)
            {
                return true;
            }
            if (xDiff == 1 && ydiff == -1)
            {
                return true;
            }
            if (xDiff == -1 && ydiff == 1)
            {
                return true;
            }
            if (xDiff == -1 && ydiff == -1)
            {
                return true;
            }
            return false;
        }
       public static bool IsInRange(this Location loc,int x,int y)
       {
           int xdiff = Core.client.Player.X - loc.X;
           int ydiff = Core.client.Player.Y - loc.Y;
           if( xdiff <= x && xdiff >= (x * -1))
           {
               if (ydiff <= y && ydiff >= (y * -1))
               {
                   return true;
               }
           }
           return false;
       }
       public static bool IsAvoidingWave(this Location loc,Creature c)
       {
           List<Location> locs = new List<Location>();

           switch(c.Direction)
           {
               case Constants.Direction.Up:
                   locs.Add(c.Location.Offset(0, -1, 0));

                   locs.Add(c.Location.Offset(-1, -2, 0));
                   locs.Add(c.Location.Offset(0, -2, 0));
                   locs.Add(c.Location.Offset(1, -2, 0));

                 
                   locs.Add(c.Location.Offset(-1, -3, 0));
                   locs.Add(c.Location.Offset(0, -3, 0));
                   locs.Add(c.Location.Offset(1, -3, 0));

                   locs.Add(c.Location.Offset(-2, -4, 0));
                   locs.Add(c.Location.Offset(-1, -4, 0));
                   locs.Add(c.Location.Offset(0, -4, 0));
                   locs.Add(c.Location.Offset(1, -4, 0));
                   locs.Add(c.Location.Offset(2, -4, 0));
               break;
               case Constants.Direction.Right:
               locs.Add(c.Location.Offset(1, 0, 0));

               locs.Add(c.Location.Offset(2, -1, 0));
               locs.Add(c.Location.Offset(2, 0, 0));
               locs.Add(c.Location.Offset(2, 1, 0));

               locs.Add(c.Location.Offset(3, -1, 0));
               locs.Add(c.Location.Offset(3, 0, 0));
               locs.Add(c.Location.Offset(3, 1, 0));

               locs.Add(c.Location.Offset(4, -2, 0));
               locs.Add(c.Location.Offset(4, -1, 0));
               locs.Add(c.Location.Offset(4, 0, 0));
               locs.Add(c.Location.Offset(4, 1, 0));
               locs.Add(c.Location.Offset(4, 2, 0));
               break;
               case Constants.Direction.Down:
                   locs.Add(c.Location.Offset(0, 1, 0));

                   locs.Add(c.Location.Offset(-1, 2, 0));
                   locs.Add(c.Location.Offset(0, 2, 0));
                   locs.Add(c.Location.Offset(1, 2, 0));

                 
                   locs.Add(c.Location.Offset(-1, 3, 0));
                   locs.Add(c.Location.Offset(0, 3, 0));
                   locs.Add(c.Location.Offset(1, 3, 0));

                   locs.Add(c.Location.Offset(-2, 4, 0));
                   locs.Add(c.Location.Offset(-1, 4, 0));
                   locs.Add(c.Location.Offset(0, 4, 0));
                   locs.Add(c.Location.Offset(1, 4, 0));
                   locs.Add(c.Location.Offset(2, 4, 0));
               break;
               case Constants.Direction.Left:
                locs.Add(c.Location.Offset(-1, 0, 0));

               locs.Add(c.Location.Offset(-2, -1, 0));
               locs.Add(c.Location.Offset(-2, 0, 0));
               locs.Add(c.Location.Offset(-2, 1, 0));

               locs.Add(c.Location.Offset(-3, -1, 0));
               locs.Add(c.Location.Offset(-3, 0, 0));
               locs.Add(c.Location.Offset(-3, 1, 0));

               locs.Add(c.Location.Offset(-4, -2, 0));
               locs.Add(c.Location.Offset(-4, -1, 0));
               locs.Add(c.Location.Offset(-4, 0, 0));
               locs.Add(c.Location.Offset(-4, 1, 0));
               locs.Add(c.Location.Offset(-4, 2, 0));
               break;
           }
           if(locs.Contains(loc))
           {
               return false;
           }
           return true;
       }
        public static byte[] ToByteArray(this string s)
        {
            List<byte> value = new List<byte>();
            foreach (char c in s.ToCharArray())
                value.Add(c.ToByte());
            return value.ToArray();
        }
        public static byte ToByte(this char value)
        {
            return (byte)value;
        }

       
        public static Location ToMemoryLocation(this uint tileNumber)
        {
            Location l = new Location();

            l.Z = Convert.ToInt32(tileNumber / (14 * 18));
            l.Y = Convert.ToInt32((tileNumber - l.Z * 14 * 18) / 18);
            l.X = Convert.ToInt32((tileNumber - l.Z * 14 * 18) - l.Y * 18);

            return l;
        }
        public static uint ToTileNumber(this Location memoryLocation)
        {
            return Convert.ToUInt32(memoryLocation.X + memoryLocation.Y * 18 + memoryLocation.Z * 14 * 18);
        }
        public static uint ToMapTileAddress(this uint tileNumber, Client client)
        {
            return client.Memory.ReadUInt32(client.Addresses.Map.MapPointer) + (client.Addresses.Map.StepTile * tileNumber);
        }
        public static Location ToWorldLocation(this Location memoryLocation, Tile playerTile, Client client)
        {
       
            Location loc = new Location();
            if (playerTile != null)
            {
                loc = new Location(memoryLocation.X, memoryLocation.Y, memoryLocation.Z);
                Location playerMemLoc = playerTile.MemoryLocation;
                int diffX = 8 - playerMemLoc.X;
                int diffY = 6 - playerMemLoc.Y;
                loc.X += diffX;
                loc.Y += diffY;

                int maxY = (int)(client.Addresses.Map.MaxY - 1);
                int maxX = (int)(client.Addresses.Map.MaxX);

                if (loc.X > maxX)
                {
                    loc.X -= (int)(client.Addresses.Map.MaxX); 
                //    loc.Y++;
                }
                else if (loc.X < 0)
                {
                    loc.X += (int)(client.Addresses.Map.MaxX);
                  //  loc.Y--;
                }
                if (loc.Y > maxY) 
                {
                    loc.Y -= (int)(client.Addresses.Map.MaxY);
                }
                else if (loc.Y < 0) 
                {
                    loc.Y += (int)(client.Addresses.Map.MaxY);
                }
                Location playerLoc = playerTile.Location;
                return new Location(playerLoc.X + (loc.X - 8), playerLoc.Y + (loc.Y - 6),
                                    playerLoc.Z + (loc.Z - playerMemLoc.Z));
            }
            return loc;
        }

    }
}
