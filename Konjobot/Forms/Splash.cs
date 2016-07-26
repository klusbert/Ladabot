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
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
    
        string Path;
        System.Threading.Thread t;
        private void Splash_Load(object sender, EventArgs e)
        {
      
            Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Tibia\\Automap\\";
            //LoadTibiaMap();




            // t = new System.Threading.Thread(new System.Threading.ThreadStart(LoadTibiaMap));
            // t = new System.Threading.Thread(new System.Threading.ThreadStart(Wait));
            //t.Start();
            //   System.Threading.Thread.Sleep(1000);
            // this.DialogResult = System.Windows.Forms.DialogResult.OK;
            timer1.Start();
        }
        private void Wait()
        {
            System.Threading.Thread.Sleep(1000);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        private void LoadTibiaMap()
        {
      
          
            int MaxHeight = 2048;
            int MaxWidth = 2048;
            Util.TibiaMap.Color  = new byte[16, MaxWidth, MaxHeight];
            Util.TibiaMap.Speed = new byte[16, MaxWidth, MaxHeight];
            DirectoryInfo di = new DirectoryInfo(Path);
            Dictionary<int, FileInfo[]> MapFiles = new Dictionary<int, FileInfo[]>();
            int count = 0;
            for (int z = 0; z < 16; z++)
            {
                FileInfo[] mapFiles = di.GetFiles("1??1??" +
                   z.ToString("00") +
                   ".map");
                MapFiles[z] = mapFiles;
                count += mapFiles.Count();
            }
            int ReadObjects = 0;
            for (int i = 0; i < 16; i++)
            {
                foreach (FileInfo inf in MapFiles[i])
                {
                    LoadMapFile(inf,i);
                    int prec = (ReadObjects * 100) / count;
                 
                    if (label1.InvokeRequired == true)
                    {
                        label1.Invoke((MethodInvoker)delegate { label1.Text = "Loading Mapfiles " + prec.ToString() + "%"; ; });
                    }
                    else
                    {
                        label1.Text = "Loading Mapfiles " + prec.ToString() + "%";
                    }
                    ReadObjects += 1;
                   // Application.DoEvents();
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
      //  public static byte[, ,] Color;
       // public static byte[, ,] Speed;
        private void LoadMapFile(FileInfo inf,int z)
        {
            int StartX = 31744;
            int StartY = 30976;
            Location RealLocation = MapFileNameToLocation(inf.Name);
            int ArrayX = RealLocation.X - StartX;
            int ArrayY = RealLocation.Y - StartY;
            byte[] ColorArray = new byte[65536 ];
            byte[] SpeedArray = new byte[65536 ];
            FileStream fs = new FileStream(inf.FullName, FileMode.Open);
        
            fs.Read(ColorArray, 0, 65536);
            fs.Read(SpeedArray, 0, 65536);
            int index = 0;
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 256; y++)
                {
                    Util.TibiaMap.Color[z, x + ArrayX, y + ArrayY] = ColorArray[index];
                    Util.TibiaMap.Speed[z, x + ArrayX, y + ArrayY] = SpeedArray[index];
                    index += 1;
                }
            }
            fs.Close();
        }
        private Location MapFileNameToLocation(string fileName)
        {
            Location l = new Location(0, 0, 0);
            if (fileName.Length == 12 || fileName.Length == 8)
            {
                l.X = Int32.Parse(fileName.Substring(0, 3)) * 256;
                l.Y = Int32.Parse(fileName.Substring(3, 3)) * 256;
                l.Z = Int32.Parse(fileName.Substring(6, 2));
            }
            return l;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        int wait = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            wait += 1;
            if(wait == 10)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
