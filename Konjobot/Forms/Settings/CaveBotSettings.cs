using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KonjoBot.Forms.Settings
{
    public partial class CaveBotSettings : Form
    {
        public CaveBotSettings()
        {
            InitializeComponent();
            CheckBox1.Checked = Core.Global.LootWhenAllIsDead;
            CheckBox2.Checked = Core.Global.LootFriendly;
            NumericUpDown1.Value = Core.Global.MinDist;
            NumericUpDown2.Value = Core.Global.MaxDist;
            TrackBar1.Value = Core.Global.StickToTarget_Prio;
            numericUpDown3.Value = Core.MinThreadWait;
            checkBox4.Checked = Core.Global.SkipWalk;
            numericUpDown4.Value = Core.Global.SkipRange;
            checkBox5.Checked = Core.Global.OpenNextBp;
        }

        private void CaveBotSettings_Load(object sender, EventArgs e)
        {

        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.LootFriendly = CheckBox2.Checked;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.LootWhenAllIsDead = CheckBox1.Checked;
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Core.Global.MinDist = (int)NumericUpDown1.Value;

        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Core.Global.MaxDist = (int)NumericUpDown2.Value;
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            Core.Global.StickToTarget_Prio = (int)TrackBar1.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            int value = (int)numericUpDown3.Value;
            Core.MinThreadWait = value;
            Core.MaxThreadWait = value + 400;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            int value = (int)numericUpDown4.Value;
            Core.Global.SkipRange = value;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.SkipWalk = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.OpenNextBp = checkBox5.Checked;
        }
    }
}
