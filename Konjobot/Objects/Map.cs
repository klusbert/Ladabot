using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace KonjoBot.Objects
{
   public  class Map
    {
       private Client client;
       public uint[, ,] TileNumberArray;
       private uint[] CachedTileNums;
       private Thread CacheThread;
       public bool PlayerHavedMoved = true;
       public Util.Timer Chacher;
       public Map(Client _client)
       {
           client = _client;
           TileNumberArray = new uint[client.Addresses.Map.MaxZ, client.Addresses.Map.MaxY, client.Addresses.Map.MaxX];
           CachedTileNums = new uint[client.Addresses.Map.MaxTiles];
           Chacher = new Util.Timer(600, true);
           Chacher.Execute += Chacher_Execute;
       }

       private IEnumerable<Tile> m_GetTilesSameFloor;
       void Chacher_Execute()
       {
        if(Core.Global.WalkerEnabled && Core.Global.AttackerEnabled)
        {

            m_GetTilesSameFloor = StoreMapTiles();
        }
       }

   
       public void UpdateTileNums()
       {
       // if(PlayerHavedMoved)
        //{   

           int index = 0;
           int Adr = client.Memory.ReadInt32(client.Addresses.Map.TileNumberArray);
          
           for (byte z = 0; z < 8; z++)
           {
               for (byte y = 0; y < 14; y++)
               {
                   for (byte x = 0; x < 18; x++)
                   {
                       TileNumberArray[z, y, x] = client.Memory.ReadUInt32(Adr + index);
                       index += 4;

                   }
               }
           }
           PlayerHavedMoved = false;
      //  }

       }
       private int GetFloorIndex()
       {
           Location Selfloc = client.PlayerLocation;
           int Index = 7 - Selfloc.Z;
           if (Index >= 0)
           {
               //nothing 
           }
           else
           {
               Index = 2;
           }
           return Index;
       }

       public IEnumerable<Tile> GetTilesSameFloor()
       {
           if (Core.Global.WalkerEnabled && Core.Global.AttackerEnabled && m_GetTilesSameFloor != null)
           {
               return m_GetTilesSameFloor;

           }
           else
           {
               return StoreMapTiles();
           }

       }
       private IEnumerable<Tile> StoreMapTiles()
       {
           UpdateTileNums();

           Location Selfloc = client.PlayerLocation;
           int Index = GetFloorIndex();
           uint MapStart = client.Memory.ReadUInt32(client.Addresses.Map.MapPointer);
           for (byte y = 0; y < 14; y++)
           {
               for (byte x = 0; x < 18; x++)
               {
                   uint tilenum = TileNumberArray[Index, y, x];
                   uint adr = MapStart + tilenum * client.Addresses.Map.StepTile;
                   Location loc = new Location(Selfloc.X + x - 8, Selfloc.Y + y - 6, Selfloc.Z);
                   Tile t = new Tile(client, (uint)adr, (uint)tilenum, loc);
                   yield return t;

               }
           }
       }

       public int GetFloorIndex(int floor)
       {

           int Index = 7 - floor;
           if (Index >= 0)
           {
               //nothing 
           }
           else
           {
               Index = 2;
           }
           return Index;
       }
       public Tile GetPlayerTile()
       {
           Location playerLoc = client.PlayerLocation;
           int floorIndex = GetFloorIndex(playerLoc.Z);
           client.Map.UpdateTileNums();
           uint tileNum = (uint)client.Map.TileNumberArray[floorIndex, 6, 8];
           uint adr = tileNum.ToMapTileAddress(client);
           return new Tile(client, adr, tileNum, playerLoc);
       }
       public Tile GetTile(Location loc)
       {
           int xdiff, ydiff;
           xdiff = loc.X - client.PlayerLocation.X + 8;
           ydiff = loc.Y - client.PlayerLocation.Y + 6;
           if (xdiff <= 17 && xdiff >= 0 && ydiff <= 13 && ydiff >= 0)
           {


               int floorIndex = GetFloorIndex(client.PlayerLocation.Z);
               client.Map.UpdateTileNums();

               uint tileNum = (uint)client.Map.TileNumberArray[floorIndex, ydiff, xdiff];
               uint adr = tileNum.ToMapTileAddress(client);
               return new Tile(client, adr, tileNum, loc);

           }
           else
           {
               return null;
           }


       }
       private uint TileNumToMapAdress(uint tilenum)
       {

           return client.Memory.ReadUInt32(client.Addresses.Map.MapPointer) + client.Addresses.Map.StepTile * tilenum;

       }
      public void FullLightOn()
       {
           client.Memory.WriteBytes(client.Addresses.Map.FullLightTrick, client.Addresses.Map.FullLightTrickBytes, (uint)client.Addresses.Map.FullLightTrickBytes.Length);

       }
      public void FullLightOff()
      {
          client.Memory.WriteBytes(client.Addresses.Map.FullLightTrick, client.Addresses.Map.OrginalLight, (uint)client.Addresses.Map.OrginalLight.Length);
      }
        public bool Fish(bool CountFish)
        {
            List<Tile> list = Core.client.Map.GetFishTiles().ToList<Tile>();
            Tile tile = null;
            int num = 0;
            foreach (Tile tile2 in list)
            {
                if (tile2.Location.DistanceTo(Core.client.PlayerLocation) <= 8.0 && tile2.Location.IsShootable(Core.client, Core.client.PlayerLocation))
                {
                    if (!CountFish)
                    {
                        Core.client.Inventory.UseItemOnTile(3483U, tile2);
                        return true;
                    }
                    if (tile == null)
                    {
                        tile = tile2;
                    }
                    num++;
                }
            }
            bool result;
            if (num == 0)
            {
                result = false;
            }
            else if (CountFish)
            {
                Core.Print(num.ToString() + " fish left.");
                Core.client.Inventory.UseItemOnTile(3483U, tile);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public IEnumerable<Tile> GetFishTiles()
       {
           foreach (Tile t in GetTilesSameFloor())
           {
               Item item = t.Items.FirstOrDefault(tileItem => Constants.Tiles.Water.GetFishIds().Contains((uint)tileItem.Id));
               if (item != null)
               {                  
                   if(t.Items.Count == 1)
                   {
                       yield return t;             

                   }
                      
               }
           }
       }
       public IEnumerable<Tile> GetNoFishTiles()
       {
           foreach (Tile t in GetTilesSameFloor())
           {
               Item item = t.Items.FirstOrDefault(tileItem => Constants.Tiles.Water.GetNoFishIds().Contains((uint)tileItem.Id));
               if (item != null)
               {
                   if (t.Location.IsShootable(Core.client, Core.client.PlayerLocation))
                   {
                       yield return t;
                   }

               }
           }
       }


    }
}
