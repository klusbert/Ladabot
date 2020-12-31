using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace KonjoBot.Forms
{
    public partial class HealingForm : Form
    {
        public HealingForm()
        {
            InitializeComponent();
            m_timer = new Util.Timer(400, true);
            m_timer.Execute += m_timer_Execute;
        }
        Util.Timer m_timer;
        private void HealingForm_Load(object sender, EventArgs e)
        {
        
        }

        void m_timer_Execute()
        {
            try
            {

                if (CheckBox1.Checked)
                {
                    //mana train
                    if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox2.Text))
                    {
                        if (Core.client.Player.Mana >= Convert.ToInt32(TextBox1.Text))
                        {
                           
                            Packets.OutGoing.Speech.SendConsole(Core.client, TextBox2.Text);
                            Core.SleepRandom();
                            return;
                        }
                    }
                    else
                    {
                        CheckBox1.Checked = false;
                    }
                }
                if (CheckBox2.Checked)
                {
                    if (!string.IsNullOrEmpty(TextBox4.Text) && !string.IsNullOrEmpty(TextBox8.Text) && !string.IsNullOrEmpty(TextBox3.Text))
                    {
                        //spell heal

                        int Hp = Convert.ToInt32(TextBox4.Text);
                        int mana = Convert.ToInt32(TextBox3.Text);
                        string spell = TextBox8.Text;
                        if (Core.client.Player.HitPoints <= Hp)
                        {
                            if (Core.client.Player.Mana >= mana)
                            {                                
                                Packets.OutGoing.Speech.SendConsole(Core.client, spell);
                                Core.SleepRandom();
                                return;
                            }
                        }
                    }
                    else
                    {
                        CheckBox2.Checked = false;
                    }
                }
                if (CheckBox3.Checked)
                {
                    //item heal

                    if (!string.IsNullOrEmpty(TextBox6.Text) && !string.IsNullOrEmpty(TextBox7.Text))
                    {
                        int hp = Convert.ToInt32(TextBox7.Text);
                        int itemId = Convert.ToInt32(TextBox6.Text);
                        if (Core.client.Player.HitPoints <= hp)
                        {
                            UseOnSelf(itemId);
                            Core.SleepRandom();
                            return;
                        }
                    }
                    else
                    {
                        CheckBox3.Checked = false;

                    }
                }
                if (CheckBox4.Checked)
                {
                    if (!string.IsNullOrEmpty(TextBox5.Text) && !string.IsNullOrEmpty(TextBox9.Text))
                    {
                        int mana = Convert.ToInt32(TextBox5.Text);
                        int itemId = Convert.ToInt32(TextBox9.Text);
                        if (Core.client.Player.Mana <= mana)
                        {
                            UseOnSelf(itemId);
                            Core.SleepRandom();
                            return;

                        }
                    }
                    else
                    {
                        CheckBox4.Checked = false;
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void UseOnSelf(int itemId)
        {
            Core.client.Inventory.UseItemOnSelf((uint)itemId);
        }

        private void HealingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
