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
               t.DoStance(c, GetAroundTargets());
               System.Threading.Thread.Sleep(50);
           }
           Core.client.MiniMap.Stop();
           if (Core.Global.LootWhenAllIsDead == false)
           {
               if(!NonLootAbleCreatures.Contains(c.Name))
               {
                   Core.WaitForLoot = System.DateTime.Now.AddSeconds(3);
               }
               
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

       public class PrioritezedLocations
       {
           public int Cost;
           public Location Location;
           public PrioritezedLocations() { }

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
