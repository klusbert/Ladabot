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
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
namespace KonjoBot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }
        private bool closeMe = false;
        private Util.Timer MainTimer;
        private void MainForm_Load(object sender, EventArgs e)
        {
            Forms.Splash f = new Forms.Splash();
            if(f.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {

            }
            Client m_client;
            m_client = Forms.ClientChooser.GetClient();
            if (m_client != null)
            {

                Core.InitilizeCore(m_client,this);
                
                MainTimer = new Util.Timer(500, true);
                MainTimer.Execute += MainTimer_Execute;
            
            }
            else
            {
                MessageBox.Show("No client selected...");
                Environment.Exit(0);
            }
        }

        void MainTimer_Execute()
        {
           if(Core.IsLoggedIn)
           {                          
            
               if (label1.InvokeRequired == true)
               {
                   label1.Invoke((MethodInvoker)delegate { label1.Text = Core.client.Player.Name; });
               }
                   else{
                        label1.Text = Core.client.Player.Name;
               }

               Packets.Pipe.AddTextPipe.Send(Core.client, 2, Core.client.Ping.ToString() + "ms", 150, 150, 150, 25, 7, "ping");
           }
           else
           {
               if (label1.InvokeRequired == true)
               {
                   label1.Invoke((MethodInvoker)delegate { label1.Text = "LoggedOut"; });
               }
               else
               {
                   label1.Text = "LoggedOut";
               }             

           }
        }
      
     
   
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(closeMe)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.closeMe = true;
            Core.client.Abort();
            Environment.Exit(0);
        }

        private void caveBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(Core.CavebotForm);
        }
        private void OpenForm(Form form)
        {
            form.TopLevel = true;
            form.Show();
        }


        public Point ContainerWindowPosition()
        {
            int x, y;
            int num = Core.client.Memory.ReadInt32(Core.client.Addresses.Client.GuiAddress);
            x = Core.client.Memory.ReadInt32(num + 0x14);
            y = Core.client.Memory.ReadInt32(num + 0x18);
            num = Core.client.Memory.ReadInt32(num + 0x38);// points to containerwindow
            x += Core.client.Memory.ReadInt32(num + 0x14);
            y += Core.client.Memory.ReadInt32(num + 0x18);
            num = Core.client.Memory.ReadInt32(num + 0x24);// points to first child
            x += Core.client.Memory.ReadInt32(num + 0x14);
            y += Core.client.Memory.ReadInt32(num + 0x18);
            return new Point(x, y);

        }
        private int GetContainerGuiAdr(int number)
        {
            int num = Core.client.Memory.ReadInt32(Core.client.Addresses.Client.GuiAddress);
            num = Core.client.Memory.ReadInt32(num + 0x38);
            num = Core.client.Memory.ReadInt32(num + 0x24);
            for (int i = 0; i < 20; i++)
            {
                int num1 = Core.client.Memory.ReadInt32(num + 0x2c);
                int ContIndex = num1 - 0x40;
                if (ContIndex == number)
                {
                    return num;
                }
                num = Core.client.Memory.ReadInt32(num + 0x10);
            }
            return 0;
        }
        private Point ContainerPos(int number)
        {
            int x, y;
      
            Point ContainerWindow = ContainerWindowPosition();
            int ContainerAdr = GetContainerGuiAdr(number);
            x = Core.client.Memory.ReadInt32(ContainerAdr + 0x14);
            y = Core.client.Memory.ReadInt32(ContainerAdr + 0x18);
            return new Point(ContainerWindow.X + x, ContainerWindow.Y + y);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Packets.OutGoing.Speech.SendConsole(Core.client, "hi");
            Core.SleepRandom();
            Packets.OutGoing.Speech.SendToNpc(Core.client, "deposit all");

        }
        private void DrawTile(Point p,bool blocking,string name,int speed)
        {
          if(blocking)
          {
              Packets.Pipe.AddTextPipe.Send(Core.client, 2, "Blocking " + speed.ToString(), 255, 1, 1, (ushort)(p.X-24), (ushort)(p.Y -6), name);

          }
          else
          {
              Packets.Pipe.AddTextPipe.Send(Core.client, 2, "WalkAble "+speed.ToString(), 1, 1,255, (ushort)(p.X - 24), (ushort)(p.Y - 6),name);
          }
        }

      

        private void packetScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(Core.PacketScannerFrm);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(Core.Global.GetType());
            SaveFileDialog dlg = new SaveFileDialog();
            var _with1 = dlg;
            _with1.Title = "Save Settnings";
            _with1.Filter = "Xml Document (*.xml)|*.xml";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream tw = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write);
                xs.Serialize(tw, Core.Global);
                tw.Close();
            }
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KonjoBot.Global ret;
            XmlSerializer xs = new XmlSerializer(Core.Global.GetType());
            OpenFileDialog dlg = new OpenFileDialog();
            var _with1 = dlg;
            _with1.Title = "Load Settnings";
            _with1.Filter = "Xml Document (*.xml)|*.xml";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream tw = new FileStream(dlg.FileName, FileMode.Open, FileAccess.ReadWrite);
                ret = xs.Deserialize(tw) as KonjoBot.Global;
                if(ret != null)
                {
                    ParseGlobal(ret);
                }
                tw.Close();
            }

        }
        private void ParseGlobal(KonjoBot.Global ret)
        {
            Core.WaypointLine = 0;
            Core.Global.LootList.Clear();
            Core.Global.TargetList.Clear();
            Core.Global.Waypoints.Clear();
            Core.Global.FullLight = ret.FullLight;
            Core.Global.LootWhenAllIsDead = ret.LootWhenAllIsDead;
            Core.Global.LootFriendly = ret.LootFriendly;
            Core.Global.MinDist = ret.MinDist;
            Core.Global.MaxDist = ret.MaxDist;
            Core.Global.StickToTarget_Prio = ret.StickToTarget_Prio;
         
            Core.Global.OpenCorpses = ret.OpenCorpses;
            Core.Global.OpenNextBp = ret.OpenNextBp;
            Core.Global.SkipRange = ret.SkipRange;
            Core.Global.SkipWalk = ret.SkipWalk;
            Core.Global.GlobalVariables = ret.GlobalVariables;
            foreach (Objects.Bot.LootItem l in ret.LootList)
            {
                Core.Global.LootList.Add(l);
            }
            foreach (Objects.Bot.Target t in ret.TargetList)
            {
                Core.Global.TargetList.Add(t);
            }
            foreach (Objects.Bot.Waypoint w in ret.Waypoints)
            {
                Core.Global.Waypoints.Add(w);
            }
            foreach (Objects.Bot.Script  s in ret.ScriptList)
            {
                s.IsRunning = false;
                Core.Global.ScriptList.Add(s);
            }
        }

        private void healerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(Core.HealingForm);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fullLightrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core.Global.FullLight = fullLightrToolStripMenuItem.Checked;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void scripterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(Core.ScriptForm);
        }

        private void openNewInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
             Process NewbotProcess = new Process();

             NewbotProcess.StartInfo.WorkingDirectory =Application.StartupPath +"\\";
             NewbotProcess.StartInfo.FileName = "KonjoBot.exe";         
             NewbotProcess.Start();
        }

        private void alarmsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(Core.Alarms);
        }

     

       
    }
}
