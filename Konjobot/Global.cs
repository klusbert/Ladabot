using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace KonjoBot
{
    public class Global
    {
        public BindingList<KonjoBot.Objects.Bot.Waypoint> Waypoints;
        public BindingList<KonjoBot.Objects.Bot.LootItem> LootList;
        public BindingList<KonjoBot.Objects.Bot.Target> TargetList;
        public BindingList<KonjoBot.Objects.Bot.Script> ScriptList;

        public string GlobalVariables = "'Your variables here";


        //alarms
        public bool PlayerOnScreen = false;
        public bool PrivateMessage = false;
        public bool Message = false;
        public bool lowHp = false;
        public int lowHpValue = 0;
        public bool LowMana = false;
        public int LowManavalue =0;
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
        public  bool LootFriendly = false;
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
        public bool ManatrainEnable
        {
            get
            {
                return Core.HealingForm.CheckBox1.Checked;
            }
            set
            {
                Core.HealingForm.CheckBox1.Checked = value;
                
            }
        }
        public string ManaTrainMana
        {
            get
            {
                return Core.HealingForm.TextBox1.Text;
            }
            set
            {
                Core.HealingForm.TextBox1.Text = value;
            }
        }
        public string ManatrainSpell
        {
            get
            {
                return Core.HealingForm.TextBox2.Text;
            }
            set
            {
                Core.HealingForm.TextBox2.Text = value;
            }
        }
        public string SpellHealHealth
        {
            get
            {
                return Core.HealingForm.TextBox4.Text;
            }
            set
            {
                Core.HealingForm.TextBox4.Text = value;
            }
        }
        public string SpellHealMana
        {
            get
            {
                return Core.HealingForm.TextBox3.Text;
            }
            set
            {
                Core.HealingForm.TextBox3.Text = value;
            }
        }
        public string SpellHealSpell
        {
            get
            {
                return Core.HealingForm.TextBox8.Text;
            }
            set
            {
                Core.HealingForm.TextBox8.Text = value;
            }
        }
        public bool SpellHealEnable
        {
            get
            {
                return Core.HealingForm.CheckBox2.Checked;
            }
            set
            {
                Core.HealingForm.CheckBox2.Checked = value;

            }
        }
        public string ItemHealHealth
        {
            get
            {
                return Core.HealingForm.TextBox7.Text;
            }
            set
            {
                Core.HealingForm.TextBox7.Text = value;
            }
        }
        public string ItemHealItem
        {
            get
            {
                return Core.HealingForm.TextBox6.Text;
            }
            set
            {
                Core.HealingForm.TextBox6.Text = value;
            }
        }
        public bool ItemHealEnable
        {
            get
            {
                return Core.HealingForm.CheckBox3.Checked;
            }
            set
            {
                Core.HealingForm.CheckBox3.Checked = value;

            }
        }
        public string ManaRestoreMana
        {
            get
            {
                return Core.HealingForm.TextBox5.Text;
            }
            set
            {
                Core.HealingForm.TextBox5.Text = value;
            }
        }
        public string ManaRestoreItem
        {
            get
            {
                return Core.HealingForm.TextBox9.Text;
            }
            set
            {
                Core.HealingForm.TextBox9.Text = value;
            }
        }
        public bool ManaRestoreEnable
        {
            get
            {
                return Core.HealingForm.CheckBox4.Checked;
            }
            set
            {
                Core.HealingForm.CheckBox4.Checked = value;

            }
        }


    }
}
