﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Threading;
namespace KonjoBot.Objects
{
   public  class MiniMap3
    {
       Client client;
       private string Path;
       private MapFile[] m_mapfile;
       private List<MyPathNode> myPath;
       private Thread WalkThread;
       public byte[,,] TibiaMap;
       List<int> WalkAbleIds = new List<int> { 3504 };
        public MiniMap3(Client cl)
       {
           client = cl;
           Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Tibia\\Automap\\";
           m_mapfile = new MapFile[9];
           LoadTibiaMap();
       }
        private void LoadTibiaMap()
        {
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Tibia\\Automap\\";
            int StartX = 31744;
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
                    FileStream fs = new FileStream(inf.FullName, FileMode.Open);
                    BufferedStream bs = new BufferedStream(fs);
                    bs.Read(ColorArray, 0, 65536);
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
        }
        public byte GetAutoMapColorAtLocation(int x, int y, int z)
        {
            int StartX = 31744;
            int StartY = 30976;
            int arrayX = x - StartX;
            int arrayY = y - StartY;
            return TibiaMap[z, arrayX, arrayY];
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

        #region MapProperties
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
        public bool StopWalk { get; set; }
        #region UpdateMap


        public bool IsWalking { get; set; }
       public void Stop()
       {
           if(WalkThread != null)
           {
               WalkThread.Abort();
           }
         
       }
       public MapFile[] Mapfiles
       {
           get
           {
               return m_mapfile;
           }
       }
       public byte GetTileCost(int x, int y)
       {
           for (int i = 0; i < 9; i++)
           {
               MapFile m = Mapfiles[i];
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
               if (m.X <= x && m.X + 256 > x && m.Y <= y && m.Y + 256 > y)
               {
                   int xdiff, ydiff;
                   xdiff = x - m.X;
                   ydiff = y - m.Y;
                   return m.MapColor[(xdiff * 256) + ydiff];
               }
           }
           return 0;
       }
       private bool IsBlocking(int x, int y)
       {

           byte color = (byte)this.GetTileColor(x, y);
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
       
       #region LoadMap

       private string LastLoadedMaps = "";
       public void LoadMapfiles()
       {
           int mapx = client.PlayerLocation.X / 256;
           int mapy = client.PlayerLocation.Y / 256;
           int mapz = client.PlayerLocation.Z;
           int i = 0;
           string CenterMap = (mapx).ToString("000") + (mapy).ToString("000") + mapz.ToString("00") + ".map";
           if(File.Exists(Path + CenterMap))
           {
               if(LastLoadedMaps == CenterMap)
               {
                   //no need to loadMap
                   return;
               }
               else
               {
                   LastLoadedMaps = CenterMap;
               }
           }

           for (int x = -1; x < 2; x++)
           {
               for (int y = -1; y < 2; y++)
               {
                   string fileName = (mapx + x).ToString("000") + (mapy + y).ToString("000") + mapz.ToString("00") + ".map";
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
       #endregion
        #region AstarStuff
       public void Goto(Location loc, int skipNodes = 0)
       {
           if (loc.Equals(Core.client.PlayerLocation))
           {
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
       private bool isBlocking(Tile t)
       {
           if (t.Ground.ItemData.Blocking || t.Ground.ItemData.BlocksPath || t.Items.Any(i => i.ItemData.Blocking || i.ItemData.BlocksPath && !WalkAbleIds.Contains(i.Id)))
           {
               return true;
           }
           return false;
       }
    
       public IEnumerable<MyPathNode> GetPath(Objects.Location loc, int maxX = 100, int maxY = 100, bool isTarget = false)
       {
                  

           System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
           System.Diagnostics.Stopwatch s2 = new System.Diagnostics.Stopwatch();
           System.Diagnostics.Stopwatch s3 = new System.Diagnostics.Stopwatch();
           s.Start();
           int playerX, playerY, playerZ, playerId;

           playerX = client.PlayerLocation.X;
           playerY = client.PlayerLocation.Y;
           playerZ = client.PlayerLocation.Z;
           int StartX, StartY, EndX, EndY;
           StartX = maxX;
           StartY = maxY;
           EndX = loc.X - playerX + maxX;
           EndY = loc.Y - playerY + maxY;
           playerId = client.Player.Id;
           if (client.PlayerLocation.Z != loc.Z)
           {
               return null;
           }
           MyPathNode[,] grid = new MyPathNode[maxX * 2, maxY * 2];
           this.LoadMapfiles();
           s.Stop();
           s2.Start();
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
           s2.Stop();
           s3.Start();

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

               grid[cx, cy] = new MyPathNode()
               {
                   IsWall = isWall,
                   X = cx,
                   Y = cy,
                   Cost = 0,
               };

           }
           grid[EndX, EndY] = new MyPathNode()
           {
               IsWall = false,
               X = EndX,
               Y = EndY,
               Cost = 0,
           };
           s3.Stop();
           client.StatusBar = "Load MapFiles = " + s.ElapsedMilliseconds.ToString() + " process mapfiles = " + s2.ElapsedMilliseconds.ToString() + " blist = " + s3.ElapsedMilliseconds.ToString();

           MySolver<MyPathNode, Object> aStar = new MySolver<MyPathNode, Object>(grid);
           IEnumerable<MyPathNode> path = aStar.Search(new Point(StartX, StartY), new Point(EndX, EndY), null);
           return path;
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
       private void Process()
       {
           int lastX, lastY;
           lastX = -1;
           lastY = -1;
           foreach (MyPathNode node in myPath)
           {
               if (lastX == -1)
               {
                   // skip first node
                   lastX = node.X;
                   lastY = node.Y;
               }
               else
               {
                   Objects.Location nextLocation = new Objects.Location(client.PlayerLocation.X + node.X - lastX, client.PlayerLocation.Y + node.Y - lastY, client.PlayerLocation.Z);
                   DateTime date = DateTime.Now.AddMilliseconds(1000 - client.Player.MoveMentSpeed);
                   int locX, locY;
                   locX = node.X - lastX;
                   locY = node.Y - lastY;

                   Constants.WalkDirection d = GetDirection(node.X - lastX, node.Y - lastY);
                   if (d == Constants.WalkDirection.Stop)
                   {
                       // stop packet so we stop now
                       myPath = null;
                       break;

                   }
                   else
                   {
                       lastX = node.X;
                       lastY = node.Y;
                       KonjoBot.Packets.Pipe.Walk.Send(client, locX, locY);
                       while (client.PlayerLocation != nextLocation && client.PlayerLocation.Z == nextLocation.Z)
                       {
                           if (date <= DateTime.Now)
                           {
                               myPath = null;
                           
                               return;
                           }
                           Thread.Sleep(5);
                       }
                   }
               }
           }
           myPath = null;
                 
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

        #endregion

    }
}
        #endregion
