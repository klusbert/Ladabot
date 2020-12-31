using KonjoBot.Constants;
using KonjoBot.Objects;
using KonjoBot.Objects.Bot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KonjoBot.Forms
{
    public partial class Mapviewer : Form
    {
        public Mapviewer()
        {
            InitializeComponent();
            string folderPath = Application.StartupPath + "\\mapfiles\\";
            if (Core.client.LoggedIn)
            {
                this.mapViewer1.GlobalLocation = Core.client.PlayerLocation;
            }
            this.mapViewer1.LoadMapfiles(folderPath);

        }

        private void Mapviewer_Load(object sender, EventArgs e)
        {
            this.mapViewer1.MouseClicked += this.MapViewer1_MouseClicked;
        }
        private void MapViewer1_MouseClicked()
        {
            Thread thread = new Thread(delegate ()
            {
             //Core.client.MiniMap.Goto(this.mapViewer1.GlobalLocation);
               Core.client.Player.GoTo = this.mapViewer1.GlobalLocation;
            });
            thread.Start();
        }

        private void Mapviewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.mapViewer1.LoadMap(Core.client.PlayerLocation);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.mapViewer1.LevelDown();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.mapViewer1.LevelUp();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void addWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Location globalLocation = this.mapViewer1.GlobalLocation;
            if (!mapViewer1.IsBlocking(globalLocation))
            {
                Waypoint waypoint = new Waypoint();
                waypoint.Location = globalLocation;
                waypoint.Type = WaypointType.Walk;
                Core.Global.Waypoints.Add(waypoint);
            }
        }
    }
}
