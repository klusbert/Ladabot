using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace KonjoBot.Objects
{
  public class FloorChanger
    {
      private Client client;
      public FloorChanger(Client _client)
      {
          client = _client;

      }
      public List<Tile> SortedMapTiles()
      {
          return client.Map.GetTilesSameFloor().OrderBy(t => t.Location.DistanceTo(client.PlayerLocation)).ToList();
      }
      public bool GoDown()
      {
          foreach (Tile  t in SortedMapTiles())
          {
              if(IsDownUse(t)) return true;
              if (IsHole(t)) return true;
              if (IsShovel(t)) return true;
          }
          return false;
      }
      private bool IsHole(Tile tile)
      {
          foreach (Item i in tile.Items)
          {
              if (Constants.TileList.Down.Contains((uint)i.Id))
              {
                  client.MiniMap.Goto(tile.Location);
                  return true;
              }
          }
          return false;
      }
      private bool IsShovel(Tile tile)
      {
          foreach (Item i in tile.Items)
          {
              if (Constants.TileList.Shovel.Contains((uint)i.Id))
              {
                  var tlist = client.Map.GetTilesSameFloor().Where(t => t.Location.IsAdjacentTo(tile.Location) && !t.IsBlocking() && t.Location != tile.Location).OrderBy(t => t.Location.DistanceTo(client.PlayerLocation)).ToList();
                  tlist = tlist.Where(t => t.IsReachable()).ToList();
                  if (tlist.Count != 0)
                  {
                      client.MiniMap.Goto(tlist[0].Location);
                      while (client.PlayerLocation != tlist[0].Location)
                      {

                          Thread.Sleep(10);
                          System.Windows.Forms.Application.DoEvents();
                      }
                      client.Inventory.UseItemOnTile(3457, tile);
                      System.Threading.Thread.Sleep(500);
                      client.MiniMap.Goto(tile.Location);
                      return true;
                  }
                  else
                  {
                      return false;
                  }

              }
          }
          return false;
      }
      private bool IsDownUse(Tile tile)
      {
          foreach (Item i in tile.Items)
          {
              if (Constants.TileList.DownUse.Contains((uint)i.Id))
              {
                    var tlist = client.Map.GetTilesSameFloor().Where(t => t.Location.IsAdjacentTo(tile.Location) && !t.IsBlocking() && t.Location != tile.Location).OrderBy(t => t.Location.DistanceTo(client.PlayerLocation)).ToList();
                    tlist = tlist.Where(t => t.IsReachable()).ToList(); 
                  if (tlist.Count != 0)
                    {
                        client.MiniMap.Goto(tlist[0].Location);
                        while (client.PlayerLocation != tlist[0].Location)
                        {

                            Thread.Sleep(10);
                            System.Windows.Forms.Application.DoEvents();
                        }
                        System.Threading.Thread.Sleep(500);
                        i.Use();
                        return true;
                    }
              }
          }
          return false;
      }
      public bool GoUp()
      {
          foreach (Tile t in SortedMapTiles())
          {
              if (IsUpUse(t)) return true;
              if (IsRope(t)) return true ;
              if (IsStairs(t)) return true;
          }

          return false;
      }
      private bool IsUpUse(Tile tile)
      {
          foreach (Item i in tile.Items)
          {
              if (Constants.TileList.UpUse.Contains((uint)i.Id))
              {
                  var tlist = client.Map.GetTilesSameFloor().Where(t => t.Location.IsAdjacentTo(tile.Location) && !t.IsBlocking() && t.Location != tile.Location).OrderBy(t => t.Location.DistanceTo(client.PlayerLocation)).ToList();
                  tlist = tlist.Where(t => t.IsReachable()).ToList();
                  if (tlist.Count != 0)
                  {
                      client.MiniMap.Goto(tlist[0].Location);
                      while (client.PlayerLocation != tlist[0].Location)
                      {

                          Thread.Sleep(10);
                          System.Windows.Forms.Application.DoEvents();
                      }
                      System.Threading.Thread.Sleep(500);
                      i.Use();
                      return true;
                  }
              }
          }
          return false;
      }
      private bool IsRope(Tile tile)
      {
          foreach (Item i in tile.Items)
          {
              if (Constants.TileList.Rope.Contains((uint)i.Id))
              {
                  var tlist = client.Map.GetTilesSameFloor().Where(t => t.Location.IsAdjacentTo(tile.Location) && !t.IsBlocking() && t.Location != tile.Location).OrderBy(t => t.Location.DistanceTo(client.PlayerLocation)).ToList();
                  tlist = tlist.Where(t => t.IsReachable()).ToList();
                  if (tlist.Count != 0)
                  {
                      client.MiniMap.Goto(tlist[0].Location);
                      while (client.PlayerLocation != tlist[0].Location)
                      {

                          Thread.Sleep(10);
                          System.Windows.Forms.Application.DoEvents();
                      }
                      client.Inventory.UseItemOnTile(3003, tile);
                     return true;
                  }
                  else
                  {
                      return false;
                  }

              }
          }
          return false;
      }
      private bool IsStairs(Tile tile)
      {
          foreach (Item i in tile.Items)
          {
              if (Constants.TileList.Up.Contains((uint)i.Id))
              {
                  client.MiniMap.Goto(tile.Location);
                  return true;
              }
          }
          return false;
      }
    }
}
