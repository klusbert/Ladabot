using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
using KonjoBot.Util; 
namespace KonjoBot.WorkClasses
{
    public  class Healer
    {
        Timer m_timer;
        public Healer()
        {
            m_timer = new Util.Timer(500, true);
            m_timer.Execute += m_timer_Execute;
        }

        void m_timer_Execute()
        {
            if(Core.Global.ManatrainEnable)
            {

            }
        }
    }
}
