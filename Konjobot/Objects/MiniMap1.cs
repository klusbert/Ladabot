using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Windows.Forms;
namespace KonjoBot.Objects
{
   public class MiniMap2
    {
        Client client;
        int maxX = 150;
        int maxY = 150;

        private string Path;
        private MapFile[] m_mapfile;
        private System.Object lockThis = new System.Object();
        List<int> WalkAbleIds = new List<int> { 3504};
        Util.Timer timer_updater;
        List<Location> BlockingLocs = new List<Location>();

        MyPathNode[,] grid = new MyPathNode[150 * 2, 150 * 2];

        public MiniMap2(Client cl)
        {
            client = cl;
            Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Tibia\\Automap\\";
            m_mapfile = new MapFile[9];
            timer_updater = new Util.Timer(1000, true);
            timer_updater.Execute += timer_updater_Execute;
        }

        void timer_updater_Execute()
        {
            int playerX, playerY, playerZ;

            playerX = client.PlayerLocation.X;
            playerY = client.PlayerLocation.Y;
            playerZ = client.PlayerLocation.Z;
            this.LoadMapfiles();



            for (int x = 0; x < maxX * 2; x++)
            {
                for (int y = 0; y < maxX * 2; y++)
                {
                    int xdiff = playerX + x - maxX;
                    int ydiff = playerY + y - maxY;
                    int MovmentSpeed = GetTileCost(xdiff, ydiff);
                    bool isWall = IsBlocking(xdiff, ydiff);


                    grid[x, y] = new MyPathNode()
                    {
                        IsWall = isWall,
                        X = x,
                        Y = y,
                        Cost = 1,
                    };

                }
            }


            foreach (Tile t in client.Map.GetTilesSameFloor())
            {
                bool isWall = false;
                int cost = 0;
                int cx, cy;
                cx = t.Location.X - playerX + 300 / 2;
                cy = t.Location.Y - playerY + 300 / 2;

                /*if (t.IsBlocking())
                 {
                     isWall = true;
                     cost = 500;

                 }
                 */
                if (t.Ground.ItemData.Blocking || t.Ground.ItemData.BlocksPath || t.Items.Any(i => i.ItemData.Blocking || i.ItemData.BlocksPath && !WalkAbleIds.Contains(i.Id)))
                {
                    isWall = true;
                    cost = 500;
                }

                foreach (TileObject o in t.Objects)
                {
                    if (o.Id == 99)
                    {
                        //has creature
                        if (o.Data == Core.client.Memory.ReadInt32(client.Addresses.Player.Id)) { break; }
                        Creature cr = Core.client.Battlelist.GetCreatures().FirstOrDefault(c => c.Id == o.Data);
                        if (cr != null)
                        {
                            Objects.Bot.Target target = Core.Global.TargetList.FirstOrDefault(j => j.Name.ToLower() == cr.Name.ToLower());
                            if (target == null)
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
                    Y = cy,
                    Cost = 1,
                };

            }
            if (BlockingLocs != null)
            {
                foreach (Location l in BlockingLocs)
                {
                    int cx, cy;
                    cx = l.X - playerX + 300 / 2;
                    cy = l.Y - playerY + 300 / 2;
                    grid[cx, cy] = new MyPathNode()
                    {
                        IsWall = false,
                        X = cx,
                        Y = cy,
                        Cost = 2000,
                    };
                }
            }
        }        
             
      
        public MapFile[] Mapfiles
       {
           get
           {
               return m_mapfile;
           }
       }
      

        public void LoadMapfiles()
        {
            int mapx = client.PlayerLocation.X / 256;
            int mapy = client.PlayerLocation.Y / 256;
            int mapz = client.PlayerLocation.Z;
            int i = 0;

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    string fileName =  (mapx + x).ToString("000") + (mapy + y).ToString("000") + client.PlayerLocation.Z.ToString("00") + ".map";
                   
                    byte[] ColorArray = new byte[65536];
                    byte[] SpeedArray = new byte[65536];
                    int realX, realY;
                    if (System.IO.File.Exists(Path + fileName))
                    {

                
                    FileStream fs = new FileStream(Path + fileName, FileMode.Open);

                    BufferedStream bs = new BufferedStream(fs);
                    realX = (mapx + x) * 256;
                    realY = (mapy + y) * 256;
              
                   
                    bs.Read(ColorArray, 0, 65536);
                    bs.Read(SpeedArray, 0, 65536);


                    MapFile m = new MapFile();
                    m.MapColor = ColorArray;
                    m.MapMovment = SpeedArray;
                    m.X = realX;
                    m.Y = realY;
                    m.Z = mapz;
                    m_mapfile[i] = m;
                    fs.Close();
                    i++;
                    }
                    else
                    {
                        MapFile m = new MapFile();
                        m.MapColor = m.NoMapColor;
                        m.MapMovment = m.NoMapColor;
                        m.X = -1;
                        m.Y = -1;
                        m.Z = -1;
                        m_mapfile[i] = m;
                    }

                }
            }
        }
        private string LastLoadedMaps;
        private MyPathNode[,] myGrid;
        private MyPathNode[,] LoadGrid()
        {
            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch s2 = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch s3 = new System.Diagnostics.Stopwatch();

            int mapx = client.PlayerLocation.X / 256;
            int mapy = client.PlayerLocation.Y / 256;
            int mapz = client.PlayerLocation.Z;
            int playerX, playerY;
            playerX = client.PlayerLocation.X;
            playerY = client.PlayerLocation.Y;

            int i = 0;
            string CurrentFilename = (mapx).ToString("000") + (mapy).ToString("000") + client.PlayerLocation.Z.ToString("00") + ".map";
            if (LastLoadedMaps != CurrentFilename)
            {

                //then we load again
                LastLoadedMaps = CurrentFilename;
                myGrid = new MyPathNode[150 * 2, 150 * 2];
                s.Start();
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        string fileName = (mapx + x).ToString("000") + (mapy + y).ToString("000") + client.PlayerLocation.Z.ToString("00") + ".map";

                        byte[] ColorArray = new byte[65536];
                        byte[] SpeedArray = new byte[65536];
                        int realX, realY;
                        if (System.IO.File.Exists(Path + fileName))
                        {
                            FileStream fs = new FileStream(Path + fileName, FileMode.Open);
                            BufferedStream bs = new BufferedStream(fs);
                            realX = (mapx + x) * 256;
                            realY = (mapy + y) * 256;
                            bs.Read(ColorArray, 0, 65536);
                            bs.Read(SpeedArray, 0, 65536);
                            MapFile m = new MapFile();
                            m.MapColor = ColorArray;
                            m.MapMovment = SpeedArray;
                            m.X = realX;
                            m.Y = realY;
                            m.Z = mapz;
                            m_mapfile[i] = m;
                            fs.Close();
                            i++;
                        }
                        else
                        {
                            MapFile m = new MapFile();
                            m.MapColor = m.NoMapColor;
                            m.MapMovment = m.NoMapColor;
                            m.X = -1;
                            m.Y = -1;
                            m.Z = -1;
                            m_mapfile[i] = m;
                        }

                    }
                }
            }
            for (int x = 0; x < 150 * 2; x++)
            {
                for (int y = 0; y < 150 * 2; y++)
                {
                    int xdiff = playerX + x - 150;
                    int ydiff = playerY + y - 150;
                    int MovmentSpeed = GetTileCost(xdiff, ydiff);
                    bool isWall = IsBlocking(xdiff, ydiff);


                    myGrid[x, y] = new MyPathNode()
                    {
                        IsWall = isWall,
                        X = x,
                        Y = y,
                        Cost = 1,
                    };

                }
            }
            foreach (Tile t in client.Map.GetTilesSameFloor())
            {
                bool isWall = false;
                int cx, cy;
                cx = t.Location.X - playerX + 150;
                cy = t.Location.Y - playerY + 150;
                if (t.IsBlocking())
                {
                    isWall = true;
                }

                myGrid[cx, cy] = new MyPathNode()
                {
                    IsWall = isWall,
                    X = cx,
                    Y = cy,
                    Cost = 0,
                };

            }
            return myGrid;

        }
   
        public string  PlayerMapfile()
        {
            return Path +
               (client.PlayerLocation.X  / 256).ToString("000") +
               (client.PlayerLocation.Y / 256).ToString("000") +
               client.PlayerLocation.Z.ToString("00") + ".map";
                                    
        }
        public byte GetTileCost(int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                MapFile m = Mapfiles[i];
                if (m == null) continue;
                if (m.X <= x && m.X + 256 > x && m.Y <= y && m.Y + 256 > y)
                {
                    int xdiff, ydiff;
                    xdiff = x - m.X;
                    ydiff = y - m.Y;
                    return m.MapMovment[(xdiff * 256) + ydiff];
                
                }
            }
            return 0;

        }
        public byte GetTileColor(int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                MapFile m = Mapfiles[i];
                if (m == null) continue;
                if (m.X <= x && m.X + 256 > x && m.Y <= y && m.Y + 256 > y)
                {
                    int xdiff, ydiff;
                    xdiff = x - m.X;
                    ydiff = y - m.Y;

                    return m.MapColor[(xdiff * 256) + ydiff ];
                }
            }
            return 0;
        }
        public bool IsBlocking(int x,int y)
        {
            byte color = (byte)this.GetTileColor(x, y);

            switch (color)
            {
                case 0x72:
                    return true;                  
                case 0x28:
                    return true;
                case 0xB3:
                    return true;
                case 0xC0:
                    return true;
                case 0x1E:
                    return true;
                case 0x56:
                    return true;
                case 0xBA:
                    return true;
                case 0xD2:
                    return true;
                case 0x0:
                    return true;
                case 0x33:
                    return true;
                case 0x8c:
                    return true;
                case 12:
                    return true;
                default:
                    return false;
            }
        }
        private bool IsBlockin1g(int x,int y)
        {

            byte color = (byte)this.GetTileColor(x, y);
            switch(color)
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
        public void SetTileColor(int x, int y,byte value)
        {
            for (int i = 0; i < 9; i++)
            {
                MapFile m = Mapfiles[i];
                if (m.X <= x && m.X + 256 > x && m.Y <= y && m.Y + 256 > y)
                {
                    int xdiff, ydiff;
                    xdiff = x - m.X;
                    ydiff = y - m.Y;

                  m.MapColor[(xdiff * 256) + ydiff] = value;
                }
            }
        }
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
       public void Stop()
        {
            StopWalk = true;
            IsWalking = false;

        }
        public bool StopWalk { get; set; }
        #region UpdateMap
         

       public bool IsWalking { get; set; }

      
        #endregion
       public void Goto(Location loc, int skipNodes = 0)
       {
           if(loc.Equals(Core.client.PlayerLocation)){
               return;
           }

           IEnumerable<MyPathNode> path = GetPath(loc);
           if (path != null)
           {
               //  m_path = path;
               //   m_location = loc;
               //   m_walkTimer.Start();
               IsWalking = false;
               ProcessDirections(path, loc, skipNodes);
           }

       }
       public void Goto(Location loc,List<Location> Blocking)
       {

       }
  
       private bool isBlocking(Tile t)
       {
           if (t.Ground.ItemData.Blocking || t.Ground.ItemData.BlocksPath || t.Items.Any(i => i.ItemData.Blocking || i.ItemData.BlocksPath && !WalkAbleIds.Contains(i.Id)))
           {
               return true;
           }
           return false;
       }
        public IEnumerable<MyPathNode> GetPath(Objects.Location loc,List<Location> BlockingLocs = null)
        {
           lock (lockThis)
           {

       
            int maxX = 150;
            int maxY = 150;
      //      IsWalking = true;
       
               StopWalk = false;
               if(!loc.IsInRange(maxX,maxY))
               {
                   client.StatusBar = "Destination is out of range";
                   return null;
               }
    

            int playerX, playerY, playerZ, playerId;

            playerX = client.PlayerLocation.X;
            playerY = client.PlayerLocation.Y;
            playerZ = client.PlayerLocation.Z;
            int StartX, StartY, EndX, EndY;
            StartX = maxX;
            StartY = maxY;
            EndX = loc.X - playerX + maxX;
            EndY = loc.Y - playerY + maxY;     
            if (client.PlayerLocation.Z != loc.Z)
            {
                return null;
            }
         //   MyPathNode[,] grid = new MyPathNode[maxX * 2, maxY * 2];

         
            if (StopWalk)
            {
                return null;
            }
           
       //here



            grid[EndX, EndY] = new MyPathNode()
            {
                IsWall = false,
                X = EndX ,
                Y = EndY ,
                Cost = 0,
            };
      
           
                
            MySolver<MyPathNode, Object> aStar = new MySolver<MyPathNode, Object>(grid);      
            IEnumerable<MyPathNode> path = aStar.Search(new Point(StartX, StartY), new Point(EndX, EndY), null);
             return path;
           }
        }
        public bool ProcessDirections(IEnumerable<MyPathNode> path, Location destLocation, int skipNodes = 0)
        {
            List<MyPathNode> myPath = path.ToList();
            myPath.RemoveAt(0); // this is no directions let's skip this
            if (skipNodes > 0)
            {
                for (int i = 1; i <= skipNodes; i++)
                {
                    myPath.RemoveAt(myPath.Count() - i);
                }
                // we can remove last loc, becouse that is the targetLoc    

            }
            int lastX, lastY;
            lastX = 150;
            lastY = 150;
            IsWalking = true;
            foreach (MyPathNode node in myPath)
            {
                if (!IsWalking)
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
                if (isBlocking(t) && myPath.Count() > 1)
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



    }
}
