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
using System.Diagnostics;

namespace KonjoBot.Forms
{
    public partial class ClientChooser : Form
    {
        public ClientChooser()
        {
            InitializeComponent();
        }
        public static ClientChooser m_clientchooser;
        public static Client m_client;

        private void ClientChooser_Load(object sender, EventArgs e)
        {

        }
        public static Client GetClient()
        {
            m_clientchooser = new ClientChooser();
       
            foreach (Client c in Client.GetClients())
            {
            
                m_clientchooser.listBox1.Items.Add(c);
            }
            m_clientchooser.TopMost = true;
            m_clientchooser.ShowDialog();
            

            return m_client;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (listBox1.SelectedIndex != -1)
            {
                          
                    m_client = (Client)listBox1.SelectedItem;
                
           
            }
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_clientchooser.listBox1.Items.Clear();
            foreach (Client c in Client.GetClients())
            {

                m_clientchooser.listBox1.Items.Add(c);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
                 System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                          dialog.Filter =
                         "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                          dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                           dialog.Title = "Select a Tibia client executable";
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_client = Client.OpenMc(dialog.FileName);
                    this.Dispose();
                }
                                                        
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
          m_client =  Client.OpenMc();
          this.Dispose();
        }
    }
}
