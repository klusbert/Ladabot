using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
namespace KonjoBot.Objects.Bot
{
    
  public  class Settnings
    {

      public BindingList<Waypoint> Waypoints;

      public int WayPointLine { get; set; }
      public Settnings()
      {
    
          Waypoints = new BindingList<Waypoint>();
         // WayPointLine = new int0
      }
      public void NextIndex()
      {
          if (Waypoints.Count - 1 == WayPointLine)
          {
              WayPointLine = 0;

          }
          else
          {
              WayPointLine += 1;

          }
      }

      #region Save and load

     
      public int RandomInt()
      {
          return RandomInt(400, 1200);
      }
      public  int RandomInt(int min, int max)
      {
          Random rnd = new Random();
          return rnd.Next(min, max);
      }
     
    }
      #endregion
}
                                                                                                                                                                                                                                                                                                        