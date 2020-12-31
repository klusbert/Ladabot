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
    public partial class AddTarget : Form
    {
        public static AddTarget m_addtarget;
        public static Objects.Bot.Target m_target;
        public AddTarget()
        {
            InitializeComponent();
        }

        private void AddTarget_Load(object sender, EventArgs e)
        {

        }
        public static Objects.Bot.Target GetTarget(Objects.Bot.Target t = null  )
        {
            m_addtarget = new AddTarget();
            if(t != null)
            {
              
                m_addtarget.TextBox1.Text = t.Name;
                m_addtarget.CheckBox1.Checked = t.AvoidWave;
                switch(t.FollowType)
                {
                    case Constants.FollowType.Reach:
                        m_addtarget.ComboBox1.SelectedIndex = 0;
                        break;
                    case Constants.FollowType.Distance:
                        m_addtarget.ComboBox1.SelectedIndex = 2;
                        break;
                    case Constants.FollowType.Stand:
                        m_addtarget.ComboBox1.SelectedIndex = 1;
                        break;
                }
                m_addtarget.NumericUpDown1.Value = (decimal)t.Prio;
                m_addtarget.fastColoredTextBox1.Text = t.Script;

            }
            else
            {
                m_addtarget.ComboBox1.SelectedIndex = 0;
            }
            m_addtarget.TopMost = true;
            m_addtarget.TextBox1.Focus();
            m_addtarget.ShowDialog();

            return m_target;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Objects.Bot.Target t = new Objects.Bot.Target();
            t.Name = m_addtarget.TextBox1.Text;
            t.AvoidWave = m_addtarget.CheckBox1.Checked;
            t.Prio = (int)m_addtarget.NumericUpDown1.Value;
            t.Script = m_addtarget.fastColoredTextBox1.Text;
            switch(m_addtarget.ComboBox1.SelectedIndex)
            {
                case 0:
                  t.FollowType = Constants.FollowType.Reach;
                    break;
                case 1:
                    t.FollowType = Constants.FollowType.Stand;
                    break;
                case 2:
                    t.FollowType = Constants.FollowType.Distance;
                    break;            

            }
            m_target = t;
            m_addtarget.Dispose();
        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void AddTarget_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
