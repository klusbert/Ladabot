using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Objects.Bot
{
  public  class Waypoint
    {
      public Location Location { get; set; }
      public Constants.WaypointType Type { get; set; }
      public string Script { get; set; }
      public String Comment { get; set; }
      public Waypoint(Location loc, Constants.WaypointType type)
      {
          Location = loc;
          Type = type;
          Script = "NextWaypoint()";
          Comment = "";
      }
      public Waypoint()
          : this(Location.Invalid, 0) { }
      public override string ToString()
      {
          return Type.ToString() + " " + Location.X + " " + Location.Y + " " + Location.Z + " "+Comment;
      }
    }
}
