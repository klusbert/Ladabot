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
    public partial class GlobalVarsForm : Form
    {
        public GlobalVarsForm()
        {
            InitializeComponent();
        }

        private void GlobalVarsForm_Load(object sender, EventArgs e)
        {
            FastColoredTextBox1.Text = Core.Global.GlobalVariables;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Core.Global.GlobalVariables = FastColoredTextBox1.Text;
            Core.ScriptObjects.ClearComplipedScripts();
            this.Close();
        }
    }
}
