using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace KonjoBot.Objects.Bot
{
    public class Target
    {
        public string Name;
        public Constants.FollowType FollowType;
        public string Script;
        public int Prio;
        public bool AvoidWave;
        public Target()
        {
            Name = "";
            FollowType = Constants.FollowType.Reach;
            Script = "";
            Prio = 0;
            AvoidWave = false;

        }
        public void DoStance(Creature targetCreature,List<Creature> targetsAround)
        {
            switch(FollowType)
            {
                case Constants.FollowType.Stand:
                    Thread.Sleep(100);
                    break;
                case Constants.FollowType.Reach:
                    DoReach(targetCreature,targetsAround);
                    break;
                case Constants.FollowType.Distance:
                    DoDistance(targetCreature);
                    break;
            }
        }
        private void DoDistance(Creature targetCreature)
        {
            List<Location> locs = GetDistanceLocaiton(targetCreature);
            if (locs.Count() > 0)
            {
                Core.client.MiniMap.Goto(locs[0]);
            }
        }
        private List<Location> GetDistanceLocaiton(Creature targetCreature)
        {
            List<Location> locs = new List<Location>();
            Location PlayerLoc = Core.client.PlayerLocation;
               foreach (Tile t in Core.client.Map.GetTilesSameFloor())
               {
                   if(!t.IsBlocking())
                   {
                       Location l = t.Location;              
                       double targetDist = targetCreature.Location.DistanceTo(t.Location);
                       l.WalkCost = (int)t.Location.DistanceTo(PlayerLoc);
                       if (targetDist > Core.Global.MaxDist)
                       {
                           l.WalkCost +=3;
                       }
                       if (targetDist < Core.Global.MinDist)
                       {
                           l.WalkCost += 3;
                       }
                       if(!l.IsShootable(Core.client,targetCreature.Location))
                       {
                           l.WalkCost +=3;
                       }
                       locs.Add(l);
                   }
               }
               return (from j in locs orderby j.WalkCost select j).ToList();     
        }
        private void DoReach(Creature targetCreature,List<Creature> targetsAround)
        {
            if(AvoidWave)
            {
                List<Location> locs = ReachAvoidWaveLocations(targetCreature, targetsAround);
                if(locs.Count() >0)
                {
                    Core.client.MiniMap.Goto(locs[0]);
                }
            }
            else
            {
                if (Core.client.PlayerLocation.IsAdjacentTo(targetCreature.Location))
                {
                    return;
                }
                Core.client.MiniMap.Goto(targetCreature.Location, 1);
            }
        }
        private List<Location> ReachAvoidWaveLocations(Creature targetCreature, List<Creature> targetsAround)
        {
            List<Location> locs = new List<Location>();
            Location PlayerLoc = Core.client.PlayerLocation;
            foreach (Tile t in Core.client.Map.GetTilesSameFloor())
            {
                if(t.IsBlocking() == false && t.Location.IsAdjacentTo(targetCreature.Location))
                {
                    Location l = t.Location;
                    double distance = t.Location.DistanceTo(PlayerLoc);
                    l.WalkCost = (int)distance;
                    if (l.IsDiagonal(targetCreature.Location  )== false)
                    {
                        l.WalkCost += 2;
                    }
                    foreach (Creature c in targetsAround)
                    {
                        if (l.IsDiagonal(c.Location) == false)
                        {
                            l.WalkCost += 2;
                        }
                    }
                    locs.Add(l);
                }
            }
            return (from j in locs orderby j.WalkCost select j).ToList();     
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
