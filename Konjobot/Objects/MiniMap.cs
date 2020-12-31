using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Threading;
namespace KonjoBot.Objects
{
    public class MiniMap1
    {
        Client client;
      //  public byte[,,] TibiaMap;
        private string Path;
        public MyPathNode[,] grid;
        private int GridSize;
        private bool m_isWalking;
        private Util.Timer m_walkTimer;
        private IEnumerable<MyPathNode> m_path;
        private Location m_location;
        public MiniMap1(Client cl)
        {
            client = cl;
            Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Tibia\\Automap\\";
            GridSize = 256;// 256* 256
            grid = new MyPathNode[256, 256];
           // LoadTibiaMap();
            m_walkTimer = new Util.Timer(800,true);
            m_walkTimer.Execute += m_walkTimer_Execute;
        }

      
        #region MapStuff

        private void LoadTibiaMap()
        {        
           /* int StartX = 31744;
            int StartY = 30976;
            int MaxHeight = 2048;
            int MaxWidth = 2048;
            TibiaMap = new byte[16, MaxWidth, MaxHeight];
            DirectoryInfo di = new DirectoryInfo(Path);
            for (int z = 0; z < 16; z++)
            {
                FileInfo[] mapFiles = di.GetFiles("1??1??" +
                   z.ToString("00") +
                   ".map");
                foreach (FileInfo inf in mapFiles)
                {
                    Location RealLocation = MapFileNameToLocation(inf.Name);
                    int ArrayX = RealLocation.X - StartX;
                    int ArrayY = RealLocation.Y - StartY;
                    byte[] ColorArray = new byte[65536];
                    byte[] SpeedArray = new byte[65536];
                    byte[] NumbersOfMarks = new byte[4];
                    FileStream fs = new FileStream(inf.FullName, FileMode.Open);
                    BufferedStream bs = new BufferedStream(fs);
                    bs.Read(ColorArray, 0, 65536);
                    bs.Read(SpeedArray, 0, 65536);
                    long offset = bs.Position;
                    bs.Read(NumbersOfMarks, 0, 4);
                    int count = BitConverter.ToInt32(NumbersOfMarks, 0);
                    if(count > 0)
                    {
                        //System.Windows.Forms.MessageBox.Show(count.ToString());
                    }
                    int index = 0;
                    for (int x = 0; x < 256; x++)
                    {
                        for (int y = 0; y < 256; y++)
                        {
                            TibiaMap[z, x + ArrayX, y + ArrayY] = ColorArray[index];
                            index += 1;
                        }
                    }
                    fs.Close();
                }
            }
            */
        }
        public void LoadGrid()
        {
            int playerX, playerY, playerZ;
            playerX = client.PlayerLocation.X;
            playerY = client.PlayerLocation.Y;
            playerZ = client.PlayerLocation.Z;
            for (int x = -GridSize / 2; x < GridSize / 2; x++)
            {
                for (int y = -GridSize / 2; y < GridSize / 2; y++)
                {
                    int ArryX = playerX + x;
                    int ArryY = playerY + y;
                    int Speed = 0;
                    bool isBlocking = IsBlocking(ArryX, ArryY, playerZ);
                    grid[x + GridSize / 2, y + GridSize / 2] = new MyPathNode()
                    {
                        IsWall = isBlocking,
                        X = x + GridSize / 2,
                        Y = y + GridSize / 2,
                        Cost = Speed,
                    };
                }
            }
            foreach (Tile t in client.Map.GetTilesSameFloor())
            {
                bool isWall = false;
                int cost = 0;
                int cx, cy;
                cx = t.Location.X - playerX + GridSize / 2;
                cy = t.Location.Y - playerY + GridSize / 2;
                if (t.IsBlocking())
                {
                    isWall = true;
                    cost = 500;

                }
              if(t.Ground.ItemData.Blocking || t.Ground.ItemData.BlocksPath || t.Items.Any(i => i.ItemData.Blocking || i.ItemData.BlocksPath))
              {
                  isWall = true;
                  cost = 500;
              }
              foreach (TileObject o in t.Objects)
              {
                  if(o.Id ==99)
                  {
                      //has creature
                      if (o.Data == Core.client.Memory.ReadInt32(client.Addresses.Player.Id)) { break; }
                      Creature cr = Core.client.Battlelist.GetCreatures().First(c => c.Id == o.Data);
                      if(cr !=null)
                      {
                          Objects.Bot.Target target = Core.Global.TargetList.FirstOrDefault(j => j.Name.ToLower() == cr.Name.ToLower());
                          if(target ==null)
                          {
                              //it means it is a monster that we dont want.
                              isWall = true;
                              cost = 500;
                          }
                      }
                  }
              }
                grid[cx, cy] = new MyPathNode()
                {
                    IsWall = isWall,
                   // X = t.Location.X - playerX,
                   // Y = t.Location.Y - playerY,
                   X = cx,
                   Y= cy,
                   Cost = 1,
                };
                
            }
           

        }
        public IEnumerable<MyPathNode> GetPath(Location loc)
        {
            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
            s.Start();
            LoadGrid();
            int startX, startY, EndX, EndY;
            startX = GridSize / 2;// the center of the grid is player location
            startY = GridSize / 2;
            EndX = loc.X - client.PlayerLocation.X + GridSize / 2;
            EndY = loc.Y - client.PlayerLocation.Y + GridSize / 2;
            grid[EndX, EndY] = new MyPathNode()
            {
                IsWall = false,
                X = EndX,
                Y =EndY,
                Cost = 1,
            };
            MySolver<MyPathNode, Object> aStar = new MySolver<MyPathNode, Object>(grid);
            IEnumerable<MyPathNode> path = aStar.Search(new Point(startX, startY), new Point(EndX, EndY), null);
            s.Stop();
            client.StatusBar ="total MS = " + s.ElapsedMilliseconds.ToString();
            return path ;
            
        }
        public bool IsWalking
        {
            get { return m_isWalking; }
            set { m_isWalking = value; }
        }
        public void Stop()
        {
            /*client.Memory.WriteInt32(Adresses.Player.GotoX, 0);
            Packets.OutGoing.Stop.Send(client);
             */

            IsWalking = false;
        }
        public void Goto(Location loc, int skipNodes = 0)
        {
            IEnumerable<MyPathNode> path = GetPath(loc);
            if(path != null)
            {
               //  m_path = path;
              //   m_location = loc;
              //   m_walkTimer.Start();
                IsWalking = false;
                ProcessDirections(path,loc,skipNodes);
            }
          
        }
        public void Goto(Location loc, IEnumerable<MyPathNode> path, int skipNodes = 0)
        {
           // m_path = path;
           // m_location = loc;
            //m_walkTimer.Start();
            IsWalking = false;
            ProcessDirections(path,loc,skipNodes);
        }
        private bool ProcessDirections(IEnumerable<MyPathNode> path, Location destLocation,int skipNodes = 0)
        {      
            List<MyPathNode> myPath = path.ToList();
            myPath.RemoveAt(0); // this is no directions let's skip this
            if(skipNodes > 0)
            {
                if (skipNodes - 1 >= myPath.Count()) {
                    return true; 
                }
                for (int i = 1; i <= skipNodes; i++)
                {
                    myPath.RemoveAt(myPath.Count() -i);
                }
                // we can remove last loc, becouse that is the targetLoc    
               
            }
            int lastX, lastY;
            lastX = GridSize / 2;
            lastY = GridSize / 2;
            IsWalking = true;
            foreach (MyPathNode node in myPath)
            {
                if(!IsWalking)
                {
                    return false;
                }
                int NextX, NextY;
                NextX = node.X - lastX;
                NextY = node.Y - lastY;
                
                Constants.WalkDirection d = GetDirection(NextX, NextY);
                Location nextLocation = new Location(client.PlayerLocation.X + NextX, client.PlayerLocation.Y + NextY, client.PlayerLocation.Z);
                DateTime date = DateTime.Now.AddMilliseconds(1000 - client.Player.MoveMentSpeed);
                Tile t = client.Map.GetTile(nextLocation);
                if(t.IsBlocking() && myPath.Count() >1)
                {
                    IsWalking = false;
                    return false;
                }
                KonjoBot.Packets.Pipe.Walk.Send(client, NextX, NextY);
                while (client.PlayerLocation != nextLocation && client.PlayerLocation.Z == nextLocation.Z)
                {
                    if (!IsWalking)
                    {
                        return false;
                    }
                    if (date <= DateTime.Now)
                    {
                        IsWalking = false;                     
                        break;
                    }
                    Thread.Sleep(5);
                }
                lastX = node.X;
                lastY = node.Y;

            }
            IsWalking = false;

            return true;
        }
        void m_walkTimer_Execute()
        {
          
            //LoadGrid();

        }
        public byte GetAutoMapColorAtLocation(int x, int y, int z)
        {
            int StartX = 31744;
            int StartY = 30976;
            int arrayX = x - StartX;
            int arrayY = y - StartY;
          //  return TibiaMap[z, arrayX, arrayY];
            return Util.TibiaMap.Color[z, arrayX, arrayY];
        }
        public byte GetWalkSpeed(int x, int y, int z)
        {
            int StartX = 31744;
            int StartY = 30976;
            int arrayX = x - StartX;
            int arrayY = y - StartY;
            //  return TibiaMap[z, arrayX, arrayY];
            return Util.TibiaMap.Speed[z, arrayX, arrayY];
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
        public bool IsBlocking14(int x, int y, int z,ref int _speed)
        {
            byte Speed = (byte)this.GetWalkSpeed(x, y, z);
            _speed = Speed;
            switch(Speed)
            {
                case 0xFF:
                    return true;
                case 0xFA:
                    return true;
                default:
                    return false;
            }
        }
        public bool IsBlocking(int x, int y, int z)
        {

            byte color = (byte)this.GetAutoMapColorAtLocation(x, y, z);
            switch (color)
            {
                case 40:
                    return false;
                case 0:
                    return true;
                case 24:
                    return false;
                case 186:
                    return true;
                case 121:
                    return false;
                case 86:
                    return false;
                case 207:
                    return false;
                case 210:
                    return false;
                case 12:
                    return true;
                case 129:
                    return false;
                case 140:
                    return false;
                case 51:
                    return true;
                case 114:
                    return true;
                case 192:
                    return true;
            }
            return false;
        }
        #endregion


        #region Process Directions

        public Constants.WalkDirection GetDirection(int x, int y)
        {

            if (x == -1 && y == -1)
            {
                return (Constants.WalkDirection)109;
            }
            else if (x == -1 && y == 0)
            {
                return (Constants.WalkDirection)104;
            }
            else if (x == -1 && y == 1)
            {
                return (Constants.WalkDirection)108;
            }
            else if (x == 0 && y == -1)
            {
                return (Constants.WalkDirection)101;
            }
            else if (x == 0 && y == 0)
            {
                //nothing
            }
            else if (x == 0 && y == 1)
            {
                return (Constants.WalkDirection)103;
            }
            else if (x == 1 && y == -1)
            {
                return (Constants.WalkDirection)106;
            }
            else if (x == 1 && y == 0)
            {
                return (Constants.WalkDirection)102;
            }
            else if (x == 1 && y == 1)
            {
                return (Constants.WalkDirection)107;
            }
            return (Constants.WalkDirection)105;
            //' Stop packet
        }
        #endregion
        
        #region Astar
        public class MyPathNode : SettlersEngine.IPathNode<Object>
        {
            public Int32 X { get; set; }
            public Int32 Y { get; set; }
            public Boolean IsWall { get; set; }
            public int Cost { get; set; }

            public bool IsWalkable(Object unused)
            {
                return !IsWall;
            }
        }
        public class MySolver<TPathNode, TUserContext> : SettlersEngine.SpatialAStar<TPathNode, TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
        {
            protected override Double Heuristic(PathNode inStart, PathNode inEnd)
            {
                return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
            }

            protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
            {
                return Heuristic(inStart, inEnd);
            }

            public MySolver(TPathNode[,] inGrid)
                : base(inGrid)
            {
            }
        }

        #endregion

    }
}
