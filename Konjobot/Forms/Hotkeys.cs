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
    public partial class Hotkeys : Form
    {
        public Hotkeys()
        {
            InitializeComponent();
            KeyboardHook.Enable();
        }

        private void Hotkeys_Load(object sender, EventArgs e)
        {
         
            dataGridView1.Rows.Add("NumPad1", "dashsouthwest()");
            dataGridView1.Rows.Add("NumPad2", "dashsouth()");
            dataGridView1.Rows.Add("NumPad3", "dashsoutheast()");
            dataGridView1.Rows.Add("NumPad4", "dashwest()");
            dataGridView1.Rows.Add("NumPad6", "dasheast()");
            dataGridView1.Rows.Add("NumPad7", "dashnorthwest()");
            dataGridView1.Rows.Add("NumPad8", "dashnorth()");
            dataGridView1.Rows.Add("NumPad9", "dashnortheast()");
            dataGridView1.RowCount = 20;
        }

        private void Hotkeys_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                Keys k = Forms.Settings.GetHotkey.GetKey();
                dataGridView1[0, e.RowIndex].Value = k.ToString();
            }

            if(e.ColumnIndex == 2)
            {
                var ch1 = dataGridView1[e.ColumnIndex, e.RowIndex];      
              if (ch1.Value == null)
                  ch1.Value=false;
              switch (ch1.Value.ToString())
              {
                  case "True":
                      ch1.Value = false;
                      break;
                  case "False":
                      ch1.Value = true;
                      break;   
              }
            
                    KeysConverter kc = new KeysConverter();
                    string keyString = (string)dataGridView1[0, e.RowIndex].Value;
                    if (keyString != null)
                    {
                        object o = kc.ConvertFromString(keyString);
                        Keys keyCode = (Keys)o;
                        if (Convert.ToBoolean(ch1.Value) == true)
                        {
                            KeyboardHook.AddKeyDown(keyCode, () => PreformScript(e.RowIndex));
                        }
                        else
                        {
                            KeyboardHook.RemoveDown(keyCode);
                        }
                     
                    }
              
            
               

            }
        }
        private bool PreformScript(int rowIndex)
        {
            if(Core.client.Window.IsActive)
            {
                string action = (string)dataGridView1[1, rowIndex].Value;            
                Core.PreformScript(action, true, false);
                return false;
            }        
           
            return true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
           
        }
    }
}
