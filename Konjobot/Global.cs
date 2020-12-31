using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using KonjoBot.Objects.Bot;

namespace KonjoBot
{
    public class Global
    {
        public BindingList<KonjoBot.Objects.Bot.Waypoint> Waypoints;
        public BindingList<KonjoBot.Objects.Bot.LootItem> LootList;
        public BindingList<KonjoBot.Objects.Bot.Target> TargetList;
        public BindingList<KonjoBot.Objects.Bot.Script> ScriptList;
        public BindingList<HealingRule> HealingRules;


        public string GlobalVariables = "'Your variables here";


        //alarms
        public bool PlayerOnScreen = false;
        public bool PrivateMessage = false;
        public bool Message = false;
        public bool lowHp = false;
        public int lowHpValue = 0;
        public bool LowMana = false;
        public int LowManavalue = 0;
        public List<string> FriendList = new List<string>();

        //walker settings
        public bool WalkerEnabled = false;
        public int SkipRange = 3;
        public bool SkipWalk = true;



        //Attacker settings
        public bool AttackerEnabled = false;
        public int MinDist = 2;
        public int MaxDist = 4;
        public int StickToTarget_Prio = 2;

        //LooterSettings
        public bool LooterEnabled = false;
        public bool OpenCorpses = false;
        public bool LootWhenAllIsDead = false;
        public bool LootFriendly = false;
        public bool OpenNextBp = false;


        public Global()
        {

        }

        public bool FullLight
        {
            get { return Core.MainForm.fullLightrToolStripMenuItem.Checked; }
            set
            {
                if (value == true)
                {
                    Core.client.Map.FullLightOn();
                }
                else
                {
                    Core.client.Map.FullLightOff();
                }
                Core.MainForm.fullLightrToolStripMenuItem.Checked = value;
            }
        }
        public bool RefillAmmo
        {
            get
            {
                return Core.HealingForm.checkBox11.Checked;
            }
            set
            {
                Core.HealingForm.checkBox11.Checked = value;
            }
        }
        public bool AutoEat
        {
            get
            {
                return Core.HealingForm.checkBox7.Checked;
            }
            set
            {
                Core.HealingForm.checkBox7.Checked = value;
            }
        }

        public bool AutChangeGold
        {
            get
            {
                return Core.HealingForm.checkBox8.Checked;
            }
            set
            {
                Core.HealingForm.checkBox8.Checked = value;
            }
        }

        public bool AutoFish
        {
            get
            {
                return Core.HealingForm.checkBox9.Checked;
            }
            set
            {
                Core.HealingForm.checkBox9.Checked = value;
            }
        }

        public bool AntiParalyzeEnable
        {
            get
            {
                return Core.HealingForm.checkBox10.Checked;
            }
            set
            {
                Core.HealingForm.checkBox10.Checked = value;
            }
        }

        public string AntiParalyzeSpell
        {
            get
            {
                return Core.HealingForm.textBox15.Text;
            }
            set
            {
                Core.HealingForm.textBox15.Text = value;
            }
        }


    }
}
