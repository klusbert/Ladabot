using KonjoBot.Objects.Bot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KonjoBot.Forms
{
    public partial class Healingform1 : Form
    {
        public Healingform1()
        {
            InitializeComponent();
        }

        private void Healingform1_Load(object sender, EventArgs e)
        {
            this.dataGridHealer.DataSource = Core.Global.HealingRules;
            this.comboBox1.Items.Add("Exura");
            this.comboBox1.Items.Add("Exura Gran");
            this.comboBox1.Items.Add("Exura Vita");
            this.comboBox1.Items.Add("Exura San");
            this.comboBox1.Items.Add("Exura Gran San");
            this.comboBox1.Items.Add("Exura Ico");
            this.comboBox1.Items.Add("Exura Gran Ico");
            this.comboBox1.Items.Add("Exura Infir");
            this.comboBox1.Items.Add("Exura Infir Ico");
            this.comboBox2.Items.Add("Ultimate Healing Rune");
            this.comboBox2.Items.Add("Intense Healing Rune");
            this.comboBox2.Items.Add("Small Health Potion");
            this.comboBox2.Items.Add("Health Potion");
            this.comboBox2.Items.Add("Strong Health Potion");
            this.comboBox2.Items.Add("Great Health Potion");
            this.comboBox2.Items.Add("Ultimate Health Potion");
            this.comboBox2.Items.Add("Supreme Health Potion");
            this.comboBox2.Items.Add("Great Spirit Potion");
            this.comboBox2.Items.Add("Ultimate Spirit Potion");
            this.comboBox2.Items.Add("Mana Potion");
            this.comboBox2.Items.Add("Strong Mana Potion");
            this.comboBox2.Items.Add("Great Mana Potion");
            this.comboBox2.Items.Add("Ultimate Mana Potion");
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.comboBox3.SelectedIndex = 0;
            this.comboBox4.SelectedIndex = 0;
        }

        private void Healingform1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HealingRule healingRule = new HealingRule();
            if (this.radioButton1.Checked)
            {
                healingRule.Action = this.comboBox1.Text;
                healingRule.Type = HealingRule.ActionType.Spell;
            }
            else
            {
                healingRule.Action = this.comboBox2.Text;
                healingRule.Type = HealingRule.ActionType.Item;
            }
            healingRule.Condition = this.comboBox3.Text;
            healingRule.Is = this.comboBox4.Text;
            healingRule.Value = this.textBox2.Text;
            Core.Global.HealingRules.Add(healingRule);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void dataGridHealer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 5)
                return;
            this.dataGridHealer.EndEdit();
        }

        private void dataGridHealer_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }   

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this item?", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            this.dataGridHealer.Rows.RemoveAt(this.dataGridHealer.SelectedRows[0].Index);
        }
    }
}
