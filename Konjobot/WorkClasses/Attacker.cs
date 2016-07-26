using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
namespace KonjoBot.WorkClasses
{
   public class Attacker
    {
       public int TargetsAround;
      
    
       private Util.Timer m_timer;
       private List<string> NonLootAbleCreatures;
  
       public Attacker()
       {
           m_timer = new Util.Timer(500, true);
           m_timer.Execute += m_timer_Execute;
           NonLootAbleCreatures = new List<string>();
           NonLootAbleCreatures.Add("Snake");
           NonLootAbleCreatures.Add("Slime");    
       }

       void m_timer_Execute()
       {
           if (Core.IsLoggedIn && Core.Global.AttackerEnabled)
          {
              AttackerExecute();
          }
       }
       private void AttackerExecute()
       {
           if (Core.Global.LootWhenAllIsDead == false)
           {
               if(Core.Looter.IsLooting)
               {
                   return;
               }
           }
           Creature NewMonster = GetNextTarget();
           if(NewMonster != null)
           {
               Attack(NewMonster);
           }
       }
       private Creature MonstertoAtack;
       private void Attack(Creature newMonster)
       {
           Objects.Bot.Target target = Core.Global.TargetList.FirstOrDefault(t => t.Name.ToLower() == newMonster.Name.ToLower());
           Core.client.MiniMap.Stop();
           Core.SleepRandom();
           newMonster.Attack();
           MonstertoAtack = newMonster;
           while(Core.client.Player.RedSquare == 0)
           {
               System.Threading.Thread.Sleep(10);

           }
           KeepStance(newMonster, target);
       }
       private void Stance(Creature c,Objects.Bot.Target t)
       {
           switch (t.FollowType)
           {
               case Constants.FollowType.Reach:
                   ReachStance(c, t);
                   break;
               case Constants.FollowType.Distance:
                   DistanceStance(c, t);
                   break;
           }
       }
       private void KeepStance(Creature c,Objects.Bot.Target t)
       {         
         
           while (Core.client.Player.RedSquare > 0)
           {
               Creature newMonster = GetNextTarget();
               if (newMonster != null)
               {
                   if (newMonster.Id != c.Id)
                   {
                       if (isNewTargetBetter(c, newMonster))
                       {
                           Attack(newMonster);
                           return;
                       }
                   }
               }
              // Stance(c, t);
               t.DoStance(c, GetAroundTargets());
               System.Threading.Thread.Sleep(50);
           }
           Core.client.MiniMap.Stop();
           //creature died
           if (Core.Global.LootWhenAllIsDead == false)
           {
               if(!NonLootAbleCreatures.Contains(c.Name))
               {
                   Core.WaitForLoot = System.DateTime.Now.AddSeconds(3);
               }
               
           }
       }
       public void DistanceStance(Creature creature,Objects.Bot.Target target)
       {
            /* Location l = Core.client.PlayerLocation;
                  
               List<Location> locs = GetDistanceLocations(creature); 
               if(locs.Count() >0)
               {
                  Core.client.MiniMap.Goto(locs[0]);
               }
               */
          
           List<PrioritezedLocations> Locations = OrderDistanceLocations(creature);
           if(Locations.Count() > 0)
           {
               System.Drawing.Point p = Core.client.Window.LocationToPoint(Locations[0].Location);

               Core.client.MiniMap.Stop();
               Core.client.MiniMap.Goto(Locations[0].Location);
              
           }
         

       }
       public void ReachStance(Creature creature, Objects.Bot.Target target)
       {
          
            if (target.AvoidWave)
            {
                List<Location> AvoidLocs = new List<Location>();
                List<Creature> targetsAround = GetAroundTargets();
                foreach (Creature c in targetsAround)
                {
                    Objects.Bot.Target aroundTarget = c.Target();
                    if(aroundTarget != null && aroundTarget.AvoidWave)
                    {
                       
                        AvoidLocs.Add(c.Location.Offset(-5, 0, 0));
                        AvoidLocs.Add(c.Location.Offset(-4, 0, 0));
                        AvoidLocs.Add(c.Location.Offset(-3, 0, 0));
                        AvoidLocs.Add(c.Location.Offset(-2, 0, 0));
                        AvoidLocs.Add(c.Location.Offset(-1, 0, 0));
                        AvoidLocs.Add(c.Location.Offset(1, 0, 0));
                        AvoidLocs.Add(c.Location.Offset(2, 0, 0));
                        AvoidLocs.Add(c.Location.Offset(3, 0, 0));
                        AvoidLocs.Add(c.Location.Offset(4, 0, 0));
                        AvoidLocs.Add(c.Location.Offset(5, 0, 0));

                        AvoidLocs.Add(c.Location.Offset(0, -5, 0));
                        AvoidLocs.Add(c.Location.Offset(0, -4, 0));
                        AvoidLocs.Add(c.Location.Offset(0, -3, 0));
                        AvoidLocs.Add(c.Location.Offset(0, -2, 0));
                        AvoidLocs.Add(c.Location.Offset(0, -1, 0));
                        AvoidLocs.Add(c.Location.Offset(0, 1, 0));
                        AvoidLocs.Add(c.Location.Offset(0, 2, 0));
                        AvoidLocs.Add(c.Location.Offset(0, 3, 0));
                        AvoidLocs.Add(c.Location.Offset(0, 4, 0));
                        AvoidLocs.Add(c.Location.Offset(0, 5, 0));
                    }
                  
                }
          

                if(Core.client.PlayerLocation.IsAdjacentTo(creature.Location))
                {
                    avoidWave(creature);
                }
                else
                {
                    IEnumerable<Objects.MiniMap.MyPathNode> path = Core.client.MiniMap.GetPath(creature.Location, AvoidLocs);
                    if (path != null)
                    {
                        Core.client.MiniMap.ProcessDirections(path, creature.Location, 1);
                    }
                }
              


            }
            if (Core.client.PlayerLocation.IsAdjacentTo(creature.Location))
           {
               return;
           }
            Core.client.MiniMap.Goto(creature.Location, 1);
            List<Location> locs;

        
       Script:

           if (Core.client.PlayerLocation.IsAdjacentTo(creature.Location))
           {
               //execute script

           }
       }
       private List<Creature> GetAroundTargets()
       {
           List<Creature> Targets = new List<Creature>();
           foreach (Creature c in Core.client.Battlelist.GetScreenCreatures())
           {
               foreach (Objects.Bot.Target target in Core.Global.TargetList)
               {
                   if(c.Name == target.Name)
                   {
                       Targets.Add(c);
                   }
               }
           }
           return Targets;
       }
       public List<Location> GetDistanceLocations(Creature c)
       {

	    List<Location> locations = new List<Location>();
	    foreach (Tile t in Core.client.Map.GetTilesSameFloor()) {

            if (t.Location.DistanceTo(c.Location) <= Core.Global.MaxDist && t.Location.DistanceTo(c.Location) >= Core.Global.MinDist && t.IsBlocking() == false && t.Location.IsShootable(Core.client, c.Location))
          {
            locations.Add(t.Location);
           }   	}
        return (from j in locations
                orderby j.DistanceTo(Core.client.PlayerLocation)
                select j).ToList();

}
       public List<PrioritezedLocations>OrderDistanceLocations(Creature c)
       {
           List<PrioritezedLocations> m_locs = new List<PrioritezedLocations>();
           Location PlayerLoc = Core.client.PlayerLocation;

           foreach (Tile t in Core.client.Map.GetTilesSameFloor())
           {
               if(t.IsBlocking() == false)
               {
                   PrioritezedLocations loc = new PrioritezedLocations();
                   double distance = t.Location.DistanceTo(PlayerLoc);
                   loc.Location = t.Location;
                   loc.Cost = (int)distance;
                   if (distance < Core.Global.MinDist)
                   {
                       loc.Cost += (Core.Global.MinDist + 3 - loc.Cost); // higher is better

                   }
                   if (distance > Core.Global.MaxDist)
                   {
                       loc.Cost += 8; // we would like to avoid this locations any time.
                   }
                   if (loc.Location.IsShootable(Core.client, c.Location))
                   {
                       loc.Cost -= 1;  //if this location can shoot the target loc, I prefere this
                   }
                   else
                   {
                       loc.Cost += 2; // if not I dont pref this
                   }
                  
                 
                
                   m_locs.Add(loc);
               }
           }
           return (from j in m_locs orderby j.Cost select j).ToList();          
       }
       public class PrioritezedLocations
       {
           public int Cost;
           public Location Location;
           public PrioritezedLocations() { }

       }

       public List<Location> GetFollowLoc(Location loc, bool diagonal = false)
       {
           List<Tile> tiles = Core.client.Map.GetTilesSameFloor().Where(t => t.Location.IsAdjacentTo(loc) && t.IsBlocking() == false && t.Location.Equals(loc) == false).ToList();
           tiles = tiles.OrderBy(t => t.Location.DistanceTo(Core.client.PlayerLocation)).ToList();

           List<Location> list = new List<Location>();
           foreach (Tile t in tiles)
           {
               if (diagonal)
               {
                   if (IsDiagonal(t.Location))
                   {
                       list.Add(t.Location);
                   }
               }
               else
               {
                   list.Add(t.Location);
               }

           }
           if (list.Count == 0)
           {
               if (tiles.Count > 0)
               {
                   list.Add(tiles[0].Location);
               }
               else
               {
                   list.Add(loc);
               }
           }
           return list;
       }
       private void avoidWave(Creature cr)
       {
           bool IsDiag = false;
           int xDiff = cr.Location.X - Core.client.PlayerLocation.X;
           int ydiff = cr.Location.Y - Core.client.PlayerLocation.Y;
           if (xDiff == 1 && ydiff == 1)
           {
               IsDiag = true;
           }
           if (xDiff == 1 && ydiff == -1)
           {
               IsDiag = true;
           }
           if (xDiff == -1 && ydiff == 1)
           {
               IsDiag = true;
           }
           if (xDiff == -1 && ydiff == -1)
           {
               IsDiag = true;
           }
           if (IsDiag == false)
           {
               if (xDiff == -1 && ydiff == 0)
               {
                   UpOrDown();
               }
               if (xDiff == 1 && ydiff == 0)
               {
                   UpOrDown();
               }
               if (xDiff == 0 && ydiff == 1)
               {
                   LeftOrRight();
               }
               if (xDiff == 0 && ydiff == -1)
               {
                   LeftOrRight();
               }
           }
       }
       private void UpOrDown()
       {
           Tile t1 = Core.client.Map.GetTile(new Location(Core.client.PlayerLocation.X, Core.client.PlayerLocation.Y + 1, Core.client.PlayerLocation.Z));
           Tile t2 = Core.client.Map.GetTile(new Location(Core.client.PlayerLocation.X, Core.client.PlayerLocation.Y - 1, Core.client.PlayerLocation.Z));
           if (t1.IsBlocking() == false)
           {
              // Packets.Outgoing.MovePacket.Send(Client, Tibia.Constants.Direction.Down);
               KonjoBot.Packets.Pipe.Walk.Send(Core.client, 0, 1);
               return;
           }
           if (t2.IsBlocking() == false)
           {
               //Packets.Outgoing.MovePacket.Send(Client, Tibia.Constants.Direction.Up);
               KonjoBot.Packets.Pipe.Walk.Send(Core.client, 0, -1);
               return;
           }
       }
       private void LeftOrRight()
       {
           Tile t1 = Core.client.Map.GetTile(new Location(Core.client.PlayerLocation.X + 1, Core.client.PlayerLocation.Y, Core.client.PlayerLocation.Z));
           Tile t2 = Core.client.Map.GetTile(new Location(Core.client.PlayerLocation.X - 1, Core.client.PlayerLocation.Y, Core.client.PlayerLocation.Z));
           if (t1.IsBlocking() == false)
           {
               //Packets.Outgoing.MovePacket.Send(Client, Tibia.Constants.Direction.Right);
               KonjoBot.Packets.Pipe.Walk.Send(Core.client, 1, 0);
               return;
           }
           if (t2.IsBlocking() == false)
           {
               //Packets.Outgoing.MovePacket.Send(Client, Tibia.Constants.Direction.Left);
               KonjoBot.Packets.Pipe.Walk.Send(Core.client, -1, 0);
               return;
           }
       }

       public bool IsDiagonal(Location targetLocation)
       {

           int xDiff = targetLocation.X - Core.client.PlayerLocation.X;
           int ydiff = targetLocation.Y - Core.client.PlayerLocation.Y;
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

       private bool isNewTargetBetter(Creature currentTarget, Creature newtarget)
       {
           try
           {
               if ((currentTarget.Target().Prio - newtarget.Target().Prio) * 2 > Core.Global.StickToTarget_Prio)
               {
                   return true;
               }
               double oldDist = Math.Round(currentTarget.Location.DistanceTo(Core.client.PlayerLocation));
               double newDist = Math.Round(newtarget.Location.DistanceTo(Core.client.PlayerLocation));
               double diff = Math.Round(oldDist - newDist);
               if (diff * 2 > Core.Global.StickToTarget_Prio)
               {
                   return true;
               }
               return false;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
       private Creature GetNextTarget()
       {
             List<Creature> CreatureList = (
                 from c in Core.client.Battlelist.GetCreatures() 
                 where c.IsOnScreen
                 orderby Math.Round(c.Location.DistanceTo(Core.client.PlayerLocation))
                 select c).ToList();
              List<Objects.Bot.Target> Tlist = (
                  from t in Core.Global.TargetList 
                  orderby t.Prio 
                  select t).ToList();

            
              foreach (Objects.Bot.Target t in Core.Global.TargetList.OrderBy(k=>k.Prio).Reverse())
              {
                  foreach (Creature c in CreatureList)
                  {
                      if(t.Name.ToLower() ==c.Name.ToLower())
                      {
                          if(c.IsReachable())
                          {
                              TargetsAround = 1;
                              return c;
                          }
                      }
                  }
              }
              TargetsAround = 0;
              return null;

       }

    }
}
