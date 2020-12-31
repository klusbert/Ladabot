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
using KonjoBot.Objects;
namespace KonjoBot.Forms
{
    public partial class Cavebot : Form
    {
        public Cavebot()
        {
            InitializeComponent();
            listBox1.DataSource = Core.Global.Waypoints;
            listBox1.DisplayMember = "ToString";
            listBox2.DataSource = Core.Global.LootList;
            listBox2.DisplayMember = "ToString";
            listBox3.DataSource = Core.Global.TargetList;
            listBox3.DisplayMember = "ToString";
            timer1.Start();
        }

        private void Cavebot_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Cavebot_Load(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.LooterEnabled = checkBox2.Checked;
            Core.Global.OpenCorpses = checkBox2.Checked;

        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Objects.Bot.LootItem lot = Forms.Settings.AddLoot.GetItem();
            if(lot !=null)
            {
                Core.Global.LootList.Add(lot);
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            int Index = listBox2.SelectedIndex;
            if(Index >  -1)
            {
                Objects.Bot.LootItem i = Core.Global.LootList[Index];
                Objects.Bot.LootItem Edited = Forms.Settings.AddLoot.GetItem(i);
                if(Edited != null)
                {
                    Core.Global.LootList[Index] = Edited;
                }

            }
        }

        private void walkToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Objects.Location l = Core.client.PlayerLocation;
           Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
           w.Location = l;
           w.Type = Constants.WaypointType.Walk;
           Core.Global.Waypoints.Add(w);
        }

        private void ropeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            w.Location = l;
            w.Type = Constants.WaypointType.Rope;
            Core.Global.Waypoints.Add(w);
        }

        private void upUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            w.Location = l;
            w.Type = Constants.WaypointType.UpUse;
            Core.Global.Waypoints.Add(w);
        }

        private void downUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            w.Location = l;
            w.Type = Constants.WaypointType.DownUse;
            Core.Global.Waypoints.Add(w);
        }

        private void shovelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            w.Location = l;
            w.Type = Constants.WaypointType.Shovel;
            Core.Global.Waypoints.Add(w);
        }

        private void doorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            w.Location = l;
            w.Type = Constants.WaypointType.Door;
            Core.Global.Waypoints.Add(w);
        }

        private void macheteToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void northToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            l.Y -= 1;
            w.Location = l;

            w.Type = Constants.WaypointType.Machete;
            Core.Global.Waypoints.Add(w);
        }

        private void southToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            l.Y += 1;
            w.Location = l;

            w.Type = Constants.WaypointType.Machete;
            Core.Global.Waypoints.Add(w);
        }

        private void eastToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            l.X += 1;
            w.Location = l;

            w.Type = Constants.WaypointType.Machete;
            Core.Global.Waypoints.Add(w);
        }

        private void westToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            l.X -= 1;
            w.Location = l;
            w.Type = Constants.WaypointType.Machete;
            Core.Global.Waypoints.Add(w);
        }
        private void SaveWaypoints()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            var _with1 = dlg;
            _with1.Title = "Save Waypoints Config";
            _with1.Filter = "WaypointList (*.lwt)|*.lwt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Create);
                BinaryWriter binaryWriter = new BinaryWriter(fs);
                int Lenght = Convert.ToInt32(Core.Global.Waypoints.Count);
                binaryWriter.Write(Lenght);
                foreach (Objects.Bot.Waypoint w in Core.Global.Waypoints)
                {
                    binaryWriter.Write(Convert.ToByte(w.Type));
                    binaryWriter.Write(Convert.ToInt32(w.Location.X));
                    binaryWriter.Write(Convert.ToInt32(w.Location.Y));
                    binaryWriter.Write(Convert.ToByte(w.Location.Z));
                    binaryWriter.Write(w.Script);
                    binaryWriter.Write(w.Comment);
                }
                fs.Close();
            }
        }
        public void LoadWaypoints()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            var _with1 = dlg;
            _with1.Title = "Load Waypoints Config";
            _with1.Filter = "WaypointList (*.lwt)|*.lwt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open);
                BinaryReader BinaryReader = new BinaryReader(fs);
                int Lenght = BinaryReader.ReadInt32();
                for (int i = 0; i <= Lenght - 1; i++)
                {
                    Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
                    Constants.WaypointType type = (Constants.WaypointType)BinaryReader.ReadByte();
                    int x = BinaryReader.ReadInt32();
                    int y = BinaryReader.ReadInt32();
                    byte z = BinaryReader.ReadByte();
                    string script = BinaryReader.ReadString();
                    string comment = BinaryReader.ReadString();

                    w.Location = new Location(x, y, z);
                    w.Type = type;
                    w.Script = script;
                    if (w.Script.ToLower() == "nextwaypoint")
                    {
                        w.Script = "NextWaypoint()";

                    }
                    w.Comment = comment;
                   Core.Global.Waypoints.Add(w);
                }
                fs.Close();
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveWaypoints();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadWaypoints();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.WalkerEnabled = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Forms.Settings.CaveBotSettings frm = new Settings.CaveBotSettings();
            frm.Show();
        }

        private void northToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            l.Y -= 1;
            w.Location = l;
            w.Type = Constants.WaypointType.Walk;
            Core.Global.Waypoints.Add(w);
        }

        private void southToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            l.Y += 1;
            w.Location = l;
            w.Type = Constants.WaypointType.Walk;
            Core.Global.Waypoints.Add(w);
        }

        private void eastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            l.X += 1;
            w.Location = l;
            w.Type = Constants.WaypointType.Walk;
            Core.Global.Waypoints.Add(w);
        }

        private void westToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects.Location l = Core.client.PlayerLocation;
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            l.X -= 1;
            w.Location = l;
            w.Type = Constants.WaypointType.Walk;
            Core.Global.Waypoints.Add(w);
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to clear?", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Core.Global.LootList.Clear();
            }
           
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int index = listBox2.SelectedIndex;
            if(index > -1)
            {
                Core.Global.LootList.RemoveAt(index);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you really want to clear?","",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes )
            {
                Core.Global.WalkerEnabled = false;
                checkBox1.Checked = false;
                Core.Global.Waypoints.Clear();
                Core.WaypointLine = 0;

            }
           
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index > -1)
            {
                Core.Global.Waypoints.RemoveAt(index);
            }
        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Objects.Bot.Target t =  Forms.Settings.AddTarget.GetTarget();
            if(t != null)
            {
                Core.Global.TargetList.Add(t);
            }
        }

        private void clearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to clear?", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Core.Global.TargetList.Clear();
            }         
           
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int index = listBox3.SelectedIndex;
            if(index >-1)
            {
                Core.Global.TargetList.RemoveAt(index);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Core.Global.AttackerEnabled = checkBox3.Checked;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            int index = listBox3.SelectedIndex;
            if (index > -1)
            {

                Objects.Bot.Target t = Forms.Settings.AddTarget.GetTarget(Core.Global.TargetList[index]);
                if (t != null)
                {
                    Core.Global.TargetList[index] = t;
                }
            }
        }

        private void costumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KonjoBot.Forms.Settings.AddWaypoint frm = new Settings.AddWaypoint();
            frm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Core.Global.WalkerEnabled)
            {
                if (Core.Global.Waypoints.Count > 0)
                {
                    listBox1.SelectedIndex = Core.WaypointLine;

                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if(index >-1)
            {
                Core.WaypointLine = listBox1.SelectedIndex;

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                Objects.Bot.Waypoint w = Core.Global.Waypoints[listBox1.SelectedIndex];
                Forms.Settings.AddWaypoint add = new Forms.Settings.AddWaypoint(w, listBox1.SelectedIndex);
                add.Show();
            }
        }

     
    }
}
