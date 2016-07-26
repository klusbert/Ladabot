using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FastColoredTextBoxNS;
namespace KonjoBot.Forms
{
    public partial class ScriptForm : Form
    {
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
        public ScriptForm()
        {
            InitializeComponent();
            ListBox1.DataSource = Core.Global.ScriptList;
            ListBox1.DisplayMember = "ToString";
            InitStylesPriority();
        }
        private void InitStylesPriority()
        {
            FastColoredTextBox1.ClearStylesBuffer();
            FastColoredTextBox1.AddStyle(SameWordsStyle);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            var _with1 = dlg;
            _with1.Title = "Save Script";
            _with1.Filter = "Script File (*.vb)|*.vb";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                FastColoredTextBox1.Text = sr.ReadToEnd();
                TextBox1.Text = System.IO.Path.GetFileName(dlg.FileName);
                fs.Close();
            }

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            var _with1 = dlg;
            _with1.Title = "Save Script";
            _with1.Filter = "Script File (*.vb)|*.vb";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                sw.Write(FastColoredTextBox1.Text);
                sw.Close();
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
                 TextBox1.Focus();
                 StringBuilder strB = new StringBuilder();

                 strB.Append("  Public Sub Main()" + System.Environment.NewLine);
                 strB.Append("    'Your Code here" + System.Environment.NewLine);
                 strB.Append("  End Sub" + System.Environment.NewLine);
                 strB.Append(System.Environment.NewLine);
                 FastColoredTextBox1.Text = strB.ToString();
        }

        private void ExecuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(FastColoredTextBox1.Text != "")
            {
                if(TextBox1.Text != "")
                {
                    Objects.Bot.Script script = new Objects.Bot.Script();
                    script.Name = TextBox1.Text;
                    script.ScriptCode = FastColoredTextBox1.Text;
                    Core.Global.ScriptList.Add(script);
                    FastColoredTextBox1.Text = "";
                    TextBox1.Text = "";

                }
                else
                {
                    MessageBox.Show("You must enter a name for the script");
                }
            }
            else
            {
                MessageBox.Show("No Code to Compile");
            }

        }

        private void ExeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ScriptForm_Load(object sender, EventArgs e)
        {

        }

        private void ScriptForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = ListBox1.SelectedIndex;
            if(i > -1)
            {
                Objects.Bot.Script script = Core.Global.ScriptList[i];
                bool val;
                if(script.ShouldRun)
                {
                    val = false;
                }
                else
                {
                    val = true;
                }
                Core.Global.ScriptList[i].ShouldRun = val;          
                ChangeScriptState(i, val);
              
                /*if(val)
                {
                    Core.PreformScript(script.ScriptCode, false);

                }
                else
                {
                    Core.ScriptObjects.DisposeScript(script.ScriptCode);
                }
                */
            }
        }
        private void ChangeScriptState(int index, bool val)
        {
            List<Objects.Bot.Script> list = new List<Objects.Bot.Script>();
            Core.Global.ScriptList[index].ShouldRun = val;
            for (int i = 0; i <  Core.Global.ScriptList.Count; i++)
            {
                list.Add(Core.Global.ScriptList[i]);
                Core.Global.ScriptList.RemoveAt(i);
                Core.Global.ScriptList.Add(list[i]);
            }          

        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
                int i = ListBox1.SelectedIndex;
                if (i > -1)
                {
                    string code = Core.Global.ScriptList[i].ScriptCode;
                    Core.Global.ScriptList.RemoveAt(i);
                    Core.ScriptObjects.DisposeScript(code);
                }
        }

        private void ViewCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           int i = ListBox1.SelectedIndex;
           if(i > -1)
           {
               Objects.Bot.Script script = Core.Global.ScriptList[i];
               FastColoredTextBox1.Text = script.ScriptCode;
               TextBox1.Text = script.Name;
           }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.GlobalVarsForm frm = new Settings.GlobalVarsForm();
            frm.ShowDialog();
        }
    }
}
