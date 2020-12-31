using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KonjoBot.Objects;
using System.Threading;
namespace KonjoBot.WorkClasses
{  
    public class Walker
    {
        private Util.Timer m_timer;

        public Walker()
        {
            m_timer = new Util.Timer(500, true);
            m_timer.Execute += m_timer_Execute;
        }

        void m_timer_Execute()
        {
           if(!Core.IsLoggedIn)
           {
               return;
           }
           if (Core.Global.SkipWalk && Core.Global.WalkerEnabled)
           {
               if (CheckSkip())
               {
                   if (Core.WaypointLine == Core.Global.Waypoints.Count - 1)
                   {
                       Core.WaypointLine = 0;
                   }
                   else
                   {
                       Core.WaypointLine += 1;
                   }
               }
           }
           if (Core.Global.WalkerEnabled && Core.Global.Waypoints.Count > 0)
            {
                if(Core.WaypointLine >= Core.Global.Waypoints.Count())
                {
                    //dissable walker
                    Core.Global.WalkerEnabled = false;
                    if (Core.CavebotForm.checkBox1.InvokeRequired == true)
                        Core.CavebotForm.checkBox1.Invoke((MethodInvoker)delegate { Core.CavebotForm.checkBox1.Checked = false; });

                    else
                        Core.CavebotForm.checkBox1.Checked = false;
                }
                if(PerFormWaypoint(Core.Global.Waypoints[Core.WaypointLine]))
                {
                    Core.PreformScript(Core.Global.Waypoints[Core.WaypointLine].Script,true,false);
                    Core.SleepRandom();
                }

            }
        }
        public bool CheckSkip()
        {
            if(!Core.Global.SkipWalk){return false;}
            Objects.Bot.Waypoint CurrentWaypoint = Core.Global.Waypoints[Core.WaypointLine];
            Objects.Bot.Waypoint NextWaypoint;
            int NextWaypointIndex = 0;
          
            if (Core.WaypointLine == Core.Global.Waypoints.Count - 1)
            {
                NextWaypointIndex = 0;
            }
            else
            {
                NextWaypointIndex = Core.WaypointLine + 1;
            }
            NextWaypoint = Core.Global.Waypoints[NextWaypointIndex];
            if (CurrentWaypoint.Location.DistanceTo(Core.client.PlayerLocation) <= Core.Global.SkipRange)
            {
                if (CurrentWaypoint.Script == "NextWaypoint()" && NextWaypoint.Location.Z == CurrentWaypoint.Location.Z && CurrentWaypoint.Type == Constants.WaypointType.Walk)
                {
                    return true;
                }
            }
         
            return false ;
        }
        private void PreformWalk(Location loc,int skipNodes = 0)
        {
            /*  IEnumerable<Objects.MiniMap.MyPathNode> path = Core.client.MiniMap.GetPath(loc);
              if(path !=null)
              {
                  Core.client.MiniMap.IsWalking = false;
                  Core.client.MiniMap.ProcessDirections(path, loc,skipNodes);
              }
              else
              {
                  NextWaypoint();
              }
            */
            Core.client.Player.GoTo = loc;
        }
        private bool PerFormWaypoint(Objects.Bot.Waypoint w)
        {
            if(!Core.IsLoggedIn)
            {
                return false;
            }
            bool WaypointReached = false;
            if (Core.WaitForLoot > DateTime.Now)
            {
                if(Core.Looter.CorpseList.Count() > 0 && Core.Global.LootWhenAllIsDead == false)
                {
                    return OpenCorpses();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Core.Looter.CorpseList.Count() > 0 && Core.Global.LootWhenAllIsDead == false)
                {
                    return OpenCorpses();
                }
            }
            if(Core.Attacker.TargetsAround > 0  || Core.client.Player.RedSquare > 0 || Core.client.MiniMap.IsWalking)
            {
                return false;
            }
            if(w.Location.Z != Core.client.PlayerLocation.Z)
            {
                NextWaypoint();
                Core.SleepRandom();
                return false;
            }
            if (Core.Global.LootWhenAllIsDead && Core.Looter.CorpseList.Count() > 0)
            {
                return OpenCorpses();
            }
            if(Core.Global.Waypoints.Count() == 0)
            {
                return false;
            }
            switch(w.Type)
            {
                case Constants.WaypointType.Walk:
                    if(Core.client.PlayerLocation == w.Location || Core.client.PlayerLocation.Z != w.Location.Z)
                    {
                        Thread.Sleep(800);
                        WaypointReached = true;
                        break;
                    }
                    else
                    {

                        // Core.client.Player.GoTo = w.Location;
                        PreformWalk(w.Location);
                        break;
                    }                
                case Constants.WaypointType.UpUse:
                    if(Core.client.PlayerLocation.Z <w.Location.Z)
                    {
                        WaypointReached = true;
                        break;
                    }
                    else
                    {
                        if(Core.client.PlayerLocation == w.Location)
                        {
                            Tile t = Core.client.Map.GetTile(w.Location);
                            foreach (Item i in t.Items)
                            {
                                if(Constants.TileList.UpUse.Contains((uint)i.Id))
                                {
                                    i.Use();
                                    Core.SleepRandom();
                                    WaypointReached = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // Core.client.Player.GoTo = w.Location;
                            PreformWalk(w.Location);
                            WaypointReached = false;
                            break;
                        }
                        break;
                    }
                    
                case Constants.WaypointType.DownUse:
                    if (Core.client.PlayerLocation.Z > w.Location.Z)
                    {
                        WaypointReached = true;
                        break;
                    }
                    else
                    {
                        if (Core.client.PlayerLocation == w.Location)
                        {
                            Tile t = Core.client.Map.GetTile(w.Location);
                            foreach (Item i in t.Items)
                            {
                                if (Constants.TileList.DownUse.Contains((uint)i.Id))
                                {
                                    i.Use();
                                    Core.SleepRandom();
                                    WaypointReached = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //  Core.client.Player.GoTo = w.Location;
                            PreformWalk(w.Location);
                            WaypointReached = false;
                            break;
                        }
                        break;
                    }
                  
                case Constants.WaypointType.Rope:
                    if (Core.client.PlayerLocation.Z < w.Location.Z)
                    {
                        WaypointReached = true;
                        break;
                    }
                    else
                    {
                        if(Core.client.PlayerLocation != w.Location && w.Location.IsAdjacentTo(Core.client.PlayerLocation) == false)
                        {
                            //   Core.client.Player.GoTo = w.Location;
                            PreformWalk(w.Location);
                            WaypointReached = false;
                            break;
                        }
                        else
                        {
                            // we must be on the spot
                            Core.client.Inventory.UseItemOnTile(3003, Core.client.Map.GetTile(w.Location));
                            Core.SleepRandom();
                            WaypointReached = false;
                            break;
                        }
                        
                    }
                    
                case Constants.WaypointType.Shovel:
         
                    if (Core.client.PlayerLocation.Z > w.Location.Z)
                    {
                        WaypointReached = true;
                        break;
                    }
                    else
                    {
                        if (Core.client.PlayerLocation != w.Location && w.Location.IsAdjacentTo(Core.client.PlayerLocation) == false)
                        {
                            //  Core.client.Player.GoTo = w.Location;
                            PreformWalk(w.Location);
                            WaypointReached = false;
                            break;
                        }
                        else
                        {
                          if(Core.client.PlayerLocation == w.Location)
                          {
                              List<Tile> tlist = Core.client.Map.GetTilesSameFloor().Where(t => t.Location.IsAdjacentTo(Core.client.PlayerLocation) && t.Location != Core.client.PlayerLocation && t.IsBlocking() ==false).ToList();
                                // Core.client.Player.GoTo = tlist[0].Location;
                                PreformWalk(tlist[0].Location);
                                WaypointReached = false;
                              break;
                          }
                          else if (Core.client.PlayerLocation.IsAdjacentTo(w.Location))
                          {
                              Tile t = Core.client.Map.GetTile(w.Location);
                              Core.client.Inventory.UseItemOnTile(3457, t);
                              Core.SleepRandom();
                              Core.client.Player.GoTo = w.Location;
                              WaypointReached = false;
                              break;
                          }
                         

                        }
                    }
                    break;
                case  Constants.WaypointType.Door:
                    if(Core.client.PlayerLocation == w.Location)
                    {
                        WaypointReached = true;
                        break;

                    }
                    else if(Core.client.PlayerLocation.IsAdjacentTo(w.Location) == false)
                    {
                        List<Tile> tiles = Core.client.Map.GetTilesSameFloor().Where(t => t.Location.IsAdjacentTo(w.Location)).ToList();
                        List<Tile> tList = tiles.Where(t => t.IsReachable()).ToList();
                        if(tList.Count() > 0)
                        {
                            //Core.client.Player.GoTo = tList[0].Location;
                            PreformWalk( tList[0].Location);
                            WaypointReached = false;
                            break;
                        }
                        else
                        {
                          //  Core.client.Player.GoTo = w.Location;
                            PreformWalk(w.Location);
                            WaypointReached = false;
                            break;
                        }
                    }
                    else if(Core.client.PlayerLocation.IsAdjacentTo(w.Location))
                    {
                        Tile t = Core.client.Map.GetTile(w.Location);
                        if(t.IsBlocking())
                        {
                            foreach (Item i in Core.client.Map.GetTile(w.Location).Items)
                            {
                                if (i.ItemData.LensHelp == 1104)// dooor
                                {
                                    i.Use();
                                    Core.SleepRandom();
                                    Core.client.Player.GoTo = w.Location;
                                    WaypointReached = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //Core.client.Player.GoTo = w.Location;
                            PreformWalk(w.Location);
                            WaypointReached = false;
                            break;
                        }
                       
                    }
                    break;
                case Constants.WaypointType.Machete:
                    if (Core.client.PlayerLocation == w.Location)
                    {
                        WaypointReached = true;
                        break;
                    }
                    else if (Core.client.PlayerLocation.IsAdjacentTo(w.Location) == false)
                    {
                        List<Tile> tiles = Core.client.Map.GetTilesSameFloor().Where(t => t.Location.IsAdjacentTo(w.Location)).ToList();
                        List<Tile> tList = tiles.Where(t => t.IsReachable()).ToList();
                        if (tList.Count() > 0)
                        {
                            //Core.client.Player.GoTo = tList[0].Location;
                            PreformWalk(tList[0].Location);
                            WaypointReached = false;
                            break;
                        }
                        else
                        {
                         //   Core.client.Player.GoTo = w.Location;
                            PreformWalk(w.Location);
                            WaypointReached = false;
                            break;
                        }
                    }
                    else if (Core.client.PlayerLocation.IsAdjacentTo(w.Location))
                    {
                        Tile t = Core.client.Map.GetTile(w.Location);
                        if (t.IsBlocking())
                        {
                            Item i = t.TopItem();
                            Core.client.Inventory.UseItemOnItem(3308, i);
                            Core.SleepRandom();
                        }
                        else
                        {
                            //Core.client.Player.GoTo = w.Location;
                            PreformWalk(w.Location);
                            WaypointReached = false;
                            break;
                        }

                    }

                    break;
            }

            return WaypointReached;
        }
        public static void NextWaypoint()
        {

            if (Core.WaypointLine == Core.Global.Waypoints.Count - 1)
            {
                Core.WaypointLine = 0;
            }
            else
            {
                Core.WaypointLine += 1;
            }
        }
        private bool  OpenCorpses()
        {

            List<Objects.Bot.Corpse> items = Core.Looter.CorpseList.OrderBy(t => t.Location.DistanceTo(Core.client.PlayerLocation)).ToList();
            if (items[0].Location.IsAdjacentTo(Core.client.PlayerLocation))
            {            
                    Core.Looter.IsLooting = true;
                    Core.Looter.LootCorpse(items[0]);
                    Core.SleepRandom();
                    return false;             
               
            }
            else if (items[0].Location.Z != Core.client.PlayerLocation.Z)
            {
                Core.Looter.CorpseList.Remove(items[0]);
                return false;
            }
            else
            {
                //  Core.client.MiniMap.Goto(items[0].Location, 1);
                PreformWalk(items[0].Location, 1);
                return false;
            }


            return false;

        }

        
    }
}
