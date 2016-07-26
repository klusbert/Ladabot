using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonjoBot.Objects.Bot
{
    
    public class Script
    {
        public string Name;
        public string ScriptCode;
        public bool ErrorSet = false;
        public bool IsRunning = false;
        public bool ShouldRun = false;
        public Script()
        {

        }
        public override string ToString()
        {
            return Name + " " + ShouldRun.ToString();
        }

    }
}
