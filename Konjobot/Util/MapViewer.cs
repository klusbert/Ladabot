using KonjoBot.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KonjoBot.Util
{
    public class MapViewer : Panel
    {

		public event MapViewer.MouseClickevent MouseClicked;	
		private List<MapFile> MapFiles = new List<MapFile>();
		private int currentZ;		
		private Point startingPos;
		private Point imagePos = new Point(0, 0);
		private Size imageSize;
		private double zoomFactor = 1.0;
		private Image mapImage;
		public Location GlobalLocation;
		public Color ClickedColor = Color.Black;
		private Timer timer1;
		private List<Color> BlockingColors = new List<Color>();
		private object lockMe = new object();

		public delegate void MouseClickevent();

		private void InitializeComponent()
		{
			this.timer1 = new Timer();
			base.SuspendLayout();
			this.timer1.Interval = 100L;
			base.ResumeLayout(false);
		}
		public void LoadMapfiles(string folderPath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
			List<FileInfo> list = (from s in directoryInfo.GetFiles()
								   where s.Extension == ".bmp"
								   select s).ToList<FileInfo>();
			List<MapFile> list2 = new List<MapFile>();
			foreach (FileInfo fileInfo in list)
			{
				MapFile item = new MapFile(fileInfo.FullName);
				list2.Add(item);
			}
			this.MapFiles = (from s in list2
							 orderby s.Floor
							 select s).ToList<MapFile>();
			this.LoadMap(this.GlobalLocation);
			this.BlockingColors.Add(Color.FromArgb(153, 51, 0));
			this.BlockingColors.Add(Color.FromArgb(51, 0, 204));
			this.BlockingColors.Add(Color.FromArgb(204, 255, 255));
			this.BlockingColors.Add(Color.FromArgb(255, 102, 0));
			this.BlockingColors.Add(Color.FromArgb(0, 255, 0));
			this.BlockingColors.Add(Color.FromArgb(102, 102, 102));
			this.BlockingColors.Add(Color.FromArgb(255, 51, 0));
			this.BlockingColors.Add(Color.FromArgb(51, 102, 153));
			this.BlockingColors.Add(Color.FromArgb(153, 255, 102));
			this.BlockingColors.Add(Color.FromArgb(0, 102, 0));
			Core.isLoaded = true;
		}
		public void LoadMap(Location loc)
		{
			object obj = this.lockMe;
			lock (obj)
			{
				this.GlobalLocation = loc;
				this.mapImage = this.MapFiles[this.GlobalLocation.Z].myPicture;
				this.imageSize = this.MapFiles[this.GlobalLocation.Z].myPicture.Size;
				if (this.GlobalLocation.X > 0)
				{
					this.currentZ = this.GlobalLocation.Z;
					this.SetMapCenter(this.GlobalLocation);
				}
				base.Invalidate();
			}
		}


		public void LevelUp()
		{
			if (this.currentZ > 0)
			{
				this.currentZ--;
				this.mapImage = this.MapFiles[this.currentZ].myPicture;
				this.imageSize = this.MapFiles[this.currentZ].myPicture.Size;
				Location globalLocation = this.GlobalLocation;
				globalLocation.Z = this.currentZ;
				this.SetMapCenter(globalLocation);
				base.Invalidate();
			}
		}


		public void LevelDown()
		{
			if (this.currentZ < 15)
			{
				this.currentZ++;
				this.mapImage = this.MapFiles[this.currentZ].myPicture;
				this.imageSize = this.MapFiles[this.currentZ].myPicture.Size;
				Location globalLocation = this.GlobalLocation;
				globalLocation.Z = this.currentZ;
				this.SetMapCenter(globalLocation);
				base.Invalidate();
			}
		}

		public MapViewer()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			base.UpdateStyles();
		}


		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (this.Zoom((e.Delta > 0) ? 2.0 : 0.5))
			{
				this.SetMapCenter(this.PointToMapCoors(new Point(e.X, e.Y)));
			}
			base.Invalidate();
		}


		protected override void OnPaint(PaintEventArgs e)
		{
			this.RedrawMap(e.Graphics);
		}

		private void RedrawMap(Graphics g)
		{
			this.RedrawMap(true, g);
		}

		private void RedrawMap(bool clear, Graphics g)
		{
			object obj = this.lockMe;
			lock (obj)
			{
				g.Clear(Color.Black);
				new SolidBrush(Color.Red);
				g.DrawImage(this.MapFiles[this.currentZ].myPicture, new Rectangle(this.imagePos, this.imageSize));
				this.DrawCoordinates(g);
			}
		}

		public bool Zoom(double factor)
		{
			bool result;
			if (this.zoomFactor < 1.0 && factor == 0.5)
			{
				result = false;
			}
			else if (this.zoomFactor > 4.0 && factor == 2.0)
			{
				result = false;
			}
			else
			{
				Location mapCenter = this.GetMapCenter();
				this.zoomFactor *= factor;
				this.imageSize.Height = (int)((double)this.imageSize.Height * factor);
				this.imageSize.Width = (int)((double)this.imageSize.Width * factor);
				this.SetMapCenter(mapCenter);
				result = true;
			}
			return result;
		}


		private void DrawCoordinates(Graphics g)
		{
			Location mapCenter = this.GetMapCenter();
			Font font = new Font("Tahoma", 10f, FontStyle.Bold);
			Rectangle rectangle = new Rectangle(base.Width - 120, 0, 120, font.Height);
			g.FillRectangle(Brushes.Black, rectangle);
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			g.DrawString(mapCenter.X + ", " + mapCenter.Y, font, Brushes.White, rectangle, stringFormat);
		}


		private void DrawCrosshairs(Graphics g)
		{
			this.DrawCrosshairs(this.GetMapCenterPoint(), g);
		}


		private void DrawCrosshairs(Point p, Graphics g)
		{
			int x = p.X;
			int y = p.Y;
			g.DrawLine(new Pen(Color.White, 2f), new Point(x - 5, y), new Point(x + 5, y));
			g.DrawLine(new Pen(Color.White, 2f), new Point(x, y - 5), new Point(x, y + 5));
		}


		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			this.SetMapCenter(this.PointToMapCoors(new Point(e.X, e.Y)));
			base.Invalidate();
		}


		protected override void OnMouseDown(MouseEventArgs e)
		{
			MouseButtons button = e.Button;
			MouseButtons mouseButtons = button;
			if (mouseButtons != MouseButtons.Left)
			{
				if (mouseButtons != MouseButtons.Right)
				{
					if (mouseButtons != MouseButtons.Middle)
					{
					}
				}
				else
				{
					this.GlobalLocation = this.PointToMapCoors(new Point(e.X, e.Y));
				}
			}
			else
			{
				this.startingPos.X = e.X;
				this.startingPos.Y = e.Y;
				this.GlobalLocation = this.PointToMapCoors(new Point(e.X, e.Y));
				base.Invalidate();
				this.MouseClicked();
			}
		}


		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.imagePos.X = this.imagePos.X + (e.X - this.startingPos.X);
				this.imagePos.Y = this.imagePos.Y + (e.Y - this.startingPos.Y);
				this.startingPos.X = e.X;
				this.startingPos.Y = e.Y;
				base.Invalidate();
			}
		}


		public Location GetMapCenter()
		{
			return this.PointToMapCoors(this.GetMapCenterPoint());
		}


		public Color GetColor(Location location)
		{
			Color result;
			if (location.Z > 15 || location.Z < 0)
			{
				result = Color.Black;
			}
			else
			{
				Rectangle boundary = this.MapFiles[location.Z].Boundary;
				Bitmap myPicture = this.MapFiles[location.Z].myPicture;
				if (location.X > boundary.Left + boundary.Width)
				{
					result = Color.Black;
				}
				else if (location.Y > boundary.Top + boundary.Height)
				{
					result = Color.Black;
				}
				else
				{
					int x = location.X - boundary.Left;
					int y = location.Y - boundary.Top;
					result = myPicture.GetPixel(x, y);
				}
			}
			return result;
		}

		public bool IsBlocking(Location location)
		{
			object obj = this.lockMe;
			bool result;
			lock (obj)
			{
				Color color = this.GetColor(location);
				foreach (Color left in this.BlockingColors)
				{
					if (left == color)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}


		public Point GetMapCenterPoint()
		{
			int x = base.Width / 2;
			int y = base.Height / 2;
			return new Point(x, y);
		}


		public Location PointToMapCoors(Point p)
		{
			int x = (int)((double)(p.X - this.imagePos.X) / this.zoomFactor + (double)this.MapFiles[this.currentZ].Boundary.Left);
			int y = (int)((double)(p.Y - this.imagePos.Y) / this.zoomFactor + (double)this.MapFiles[this.currentZ].Boundary.Top);
			return new Location(x, y, this.currentZ);
		}

		public void SetMapCenter(Location l)
		{
			this.currentZ = l.Z;
			Point mapCenterPoint = this.GetMapCenterPoint();
			int x = (int)((double)(l.X - this.MapFiles[this.currentZ].Boundary.Left) * this.zoomFactor * -1.0 + (double)mapCenterPoint.X);
			int y = (int)((double)(l.Y - this.MapFiles[this.currentZ].Boundary.Top) * this.zoomFactor * -1.0 + (double)mapCenterPoint.Y);
			this.imagePos.X = x;
			this.imagePos.Y = y;
			base.Invalidate();
		}


		public void FollowCharacter(Location location)
		{
			this.currentZ = location.Z;
			this.GlobalLocation = location;
			this.LoadMap(location);
		}


	}
}
