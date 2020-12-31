using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KonjoBot.Forms
{
    public partial class PacketScanner : Form
    {
        public PacketScanner()
        {
            InitializeComponent();
        }

        private void PacketScanner_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void PacketScanner_Load(object sender, EventArgs e)
        {
            Core.client.HookProxy.OutgoingPacket += HookProxy_OutgoingPacket;
            Core.client.HookProxy.IncommingPacket +=HookProxy_IncommingPacket;
        }
        void HookProxy_IncommingPacket(byte[]data)
        {
            if(!CheckBox1.Checked) {return;}
            string str = "Server ";
            for (int i = 0; i < data.Length; i++)
            {
                byte v = data[i];
                str += v.ToString("X") + " ";
            }
          
            if (listBox1.InvokeRequired == true)
            {
                listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add(str); });
            }
            else
            {
                listBox1.Items.Add(str);
            }
        }
        void HookProxy_OutgoingPacket(byte[] data)
        {
            if (!CheckBox2.Checked) { return; }
            string str = "Client ";
            for (int i = 0; i < data.Length; i++)
            {
                byte v = data[i];
                str += v.ToString("X") + " ";
            }
            if(listBox1.InvokeRequired == true)
            {
                listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add(str); });
            }
            else
            {
                listBox1.Items.Add(str);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if(index >-1)
            {
                System.Windows.Forms.Clipboard.SetText(listBox1.Items[index].ToString());
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to clear?","Clear?",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                listBox1.Items.Clear();
            }
        }
    }
}
