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
    public partial class AddLoot : Form
    {
        private static AddLoot m_AddLoot;
        public static Objects.Bot.LootItem LootItem;
        public AddLoot()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddLoot_Load(object sender, EventArgs e)
        {

        }
        public static Objects.Bot.LootItem GetItem(Objects.Bot.LootItem Item = null)
        {
            m_AddLoot = new AddLoot();

            if(Item != null)
            {
                switch(Item.Destination)
                {
                    case Constants.LootDestination.Container:
                        m_AddLoot.comboBox1.SelectedIndex = 0;
                        break;
                    case Constants.LootDestination.LeftHand:
                        m_AddLoot.comboBox1.SelectedIndex = 1;
                        break;
                    case Constants.LootDestination.RightHand:
                        m_AddLoot.comboBox1.SelectedIndex = 2;
                        break;
                    case Constants.LootDestination.Arrow:
                        m_AddLoot.comboBox1.SelectedIndex = 3;
                        break;              
                    case Constants.LootDestination.Ground:
                        m_AddLoot.comboBox1.SelectedIndex = 4;
                        break;

                }
                m_AddLoot.TextBox1.Text = Item.Id.ToString();
                m_AddLoot.TextBox2.Text = Item.Cap.ToString();
                m_AddLoot.comboBox2.SelectedIndex = Item.ContainerDestionation;
                m_AddLoot.checkBox1.Checked = Item.DropInDepot;

            }
            else
            {
                m_AddLoot.comboBox1.SelectedIndex = 0;
                m_AddLoot.comboBox2.SelectedIndex = 16;

            }
            m_AddLoot.TopMost = true;
            m_AddLoot.TextBox1.Focus();
            m_AddLoot.ShowDialog();

            return LootItem;

        }
        private Constants.LootDestination GetDest()
        {

            switch (comboBox1.Text)
            {
                case "Container":
                    return (Constants.LootDestination)1;
                case "ArrowSlot":
                    return (Constants.LootDestination)2;
                case "LeftHand":
                    return (Constants.LootDestination)3;
                case "RightHand":
                    return (Constants.LootDestination)4;
                case "Ground":
                    return (Constants.LootDestination)5;
            }
            return Constants.LootDestination.Container;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Objects.Bot.LootItem l = new Objects.Bot.LootItem();
                int.TryParse(TextBox1.Text, out l.Id);
                int.TryParse(TextBox2.Text, out l.Cap);
                l.Destination = GetDest();
                LootItem = l;
                l.ContainerDestionation = (byte)comboBox2.SelectedIndex;
                l.DropInDepot = checkBox1.Checked;
                m_AddLoot.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Container")
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;

            }
        }
    }
}
