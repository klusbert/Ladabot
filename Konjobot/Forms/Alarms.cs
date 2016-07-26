using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KonjoBot.Objects;
namespace KonjoBot.Forms
{
    public partial class Alarms : Form
    {
        Util.Timer m_timer;
        public Alarms()
        {
            InitializeComponent();
          
            checkBox1.Checked = Core.Global.PlayerOnScreen;
            checkBox2.Checked = Core.Global.Message;
            checkBox3.Checked = Core.Global.PrivateMessage;
            checkBox4.Checked = Core.Global.lowHp;
            checkBox5.Checked = Core.Global.LowMana;
            numericUpDown1.Value = Core.Global.lowHpValue;
            numericUpDown2.Value = Core.Global.LowManavalue;
            m_timer = new Util.Timer(600, true);
            m_timer.Execute += m_timer_Execute;

        }

        void m_timer_Execute()
        {
          if(Core.Global.PlayerOnScreen)
          {
              foreach (Creature c in Core.client.Battlelist.GetScreenCreatures())
              {
                  if (c.isPlayer() && c.Id != Core.client.Player.Id)
                  {
                      Core.StartAlarm("Player On Screen");
                      break;
                  }
              }
          }
            
        }

        private void Alarms_Load(object sender, EventArgs e)
        {

        }

        private void Alarms_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.PlayerOnScreen = checkBox1.Checked;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.Message = checkBox2.Checked;

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.PrivateMessage = checkBox3.Checked;

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.lowHp = checkBox4.Checked;

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Core.Global.lowHpValue = (int)numericUpDown1.Value;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.LowMana = checkBox5.Checked;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Core.Global.LowManavalue = (int)numericUpDown2.Value;
        }
    }
}
