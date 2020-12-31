using FastColoredTextBoxNS;
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
    public partial class AddWaypoint : Form
    {

        private bool IsEdit = false;
        private int Editindex = 0;
        private KonjoBot.Objects.Bot.Waypoint tempWaypoint = null;
        private Util.Timer m_timer;
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
        public AddWaypoint(KonjoBot.Objects.Bot.Waypoint w = null,int index = 0)
        {
            InitializeComponent();
            m_timer = new Util.Timer(300, true);
            ComboBox1.SelectedIndex = 0;
            ComboBox2.SelectedIndex = 0;
            m_timer.Execute += m_timer_Execute;
            if (w != null)
            {
                IsEdit = true;
                Editindex = index;
                TextBox1.Text = Convert.ToString(w.Location.X);
                TextBox2.Text = Convert.ToString(w.Location.Y);
                TextBox3.Text = Convert.ToString(w.Location.Z);
                TextBox4.Text = w.Comment;
                groupBox1.Enabled = false;
                FastColoredTextBox1.Text = w.Script;
                switch (w.Type)
                {
                    case  Constants.WaypointType.Walk:
                        ComboBox1.SelectedIndex = 0;
                        break;
                    case Constants.WaypointType.Rope:
                        ComboBox1.SelectedIndex = 2;
                        break;
                    case Constants.WaypointType.Shovel:
                        ComboBox1.SelectedIndex = 1;
                        break;
                    case Constants.WaypointType.UpUse:
                        ComboBox1.SelectedIndex = 3;
                        break;
                    case Constants.WaypointType.DownUse:
                        ComboBox1.SelectedIndex = 4;
                        break;
                    case Constants.WaypointType.Door:
                        ComboBox1.SelectedIndex = 5;
                        break;
                }

            }
            else
            {

                this.TopMost = true;
                Core.client.Window.Activate();
                Objects.Location loc = Core.client.PlayerLocation;
                TextBox1.Text = loc.X.ToString();
                TextBox2.Text = loc.Y.ToString();
                TextBox3.Text = loc.Z.ToString();
                if(index == -1)
                {
                    //no index selected cant insert
                    groupBox1.Enabled = false;
                }
                Editindex = index;
              
            }
           /* FastColoredTextBox1.ClearStylesBuffer();
            FastColoredTextBox1.AddStyle(SameWordsStyle);
            */
        }

        void m_timer_Execute()
        {
            UpdateMe();
        }

        private void AddWaypoint_Load(object sender, EventArgs e)
        {
    
        }
        private void UpdateMe()
        {
            Objects.Location loc;
            string str = "";

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { this.Text = Core.client.PlayerLocation.ToString(); });
            }
            else
            {
                this.Text = Core.client.PlayerLocation.ToString();
            }
                     
        }
        public Objects.Location GetLocation(string offset)
        {
            Objects.Location location = Core.client.PlayerLocation;
            if (offset.ToLower() == "playerlocation")
            {
                return location;
            }
            switch (offset.ToLower())
            {
                case "north":
                    return location.Offset(0, -1, 0);
                case "south":
                    return location.Offset(0, 1, 0);
                case "east":
                    return location.Offset(1, 0, 0);
                case "west":
                    return location.Offset(-1, 0, 0);
            }
            return location;
        }
        public void Add()
        {
            try
            {
                int x = Convert.ToInt32(TextBox1.Text);
                int y = Convert.ToInt32(TextBox2.Text);
                int z = Convert.ToInt32(TextBox3.Text);
                Objects.Location location = new Objects.Location(x, y, z);
                Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
                w.Location = location;
                w.Type = GetWayPointType();
                w.Script = FastColoredTextBox1.Text;
                w.Comment = TextBox4.Text;
                TextBox4.Text = "";
                FastColoredTextBox1.Text = "NextWaypoint";
                if(Editindex != -1)
                {
                    if(radioButton1.Checked) //add
                    {
                        Core.Global.Waypoints.Add(w);
                    }
                    if(radioButton3.Checked)//before    
                    {

                    }
                    if(radioButton2.Checked)//after
                    {

                    }
                }
                else
                {
                    Core.Global.Waypoints.Add(w);
                }
              
            }
            catch (Exception ex)
            {
               
            }
        }
        public void Edit()
        {
            int x = Convert.ToInt32(TextBox1.Text);
            int y = Convert.ToInt32(TextBox2.Text);
            int z = Convert.ToInt32(TextBox3.Text);
            Objects.Location location = new Objects.Location(x, y, z);
            Objects.Bot.Waypoint w = new Objects.Bot.Waypoint();
            w.Location = location;
            w.Type = GetWayPointType();
            w.Script = FastColoredTextBox1.Text;
            w.Comment = TextBox4.Text;
            Core.Global.Waypoints[Editindex] = w;
            this.Close();
        }
        private Constants.WaypointType GetWayPointType()
        {

            string text = ComboBox1.Text;
            switch (text)
            {
                case "Rope":
                    return Constants.WaypointType.Rope;
                case "Walk":
                    return Constants.WaypointType.Walk;
                case "Shovel":
                    return Constants.WaypointType.Shovel;
                case "UpUse":
                    return Constants.WaypointType.UpUse;
                case "DownUse":
                    return Constants.WaypointType.DownUse;
                case "Door":
                    return Constants.WaypointType.Door;
                case "Machete":
                    return Constants.WaypointType.Machete;
                default:
                    MessageBox.Show("Invalid waypointType");
                    break;
            }
            return Constants.WaypointType.Walk;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                Edit();
            }
            else
            {
                Add();
            }
        }

        private void AddWaypoint_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_timer.Dispose();
        }



    }
}
