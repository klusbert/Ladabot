using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using KonjoBot.Objects;
namespace KonjoBot.Objects
{
   public partial class Client
    {
       public class WindowHelper
       {
           Client client;
           public WindowHelper(Client cl)
           {
               client = cl;

           }
           public void Activate()
           {
               Util.WinApi.SetForegroundWindow(client.MainWindowHandle);
           }
           public Objects.Rect  Size
           {
               get
               {
                   Util.WinApi.RECT r = new Util.WinApi.RECT();
                   Util.WinApi.GetWindowRect(client.MainWindowHandle , ref r);

                   return new Rect(r);
               }
           }
         
           public Rect WindowRectangle()
           {
           Util.WinApi.RECT r = new Util.WinApi.RECT();
           Util.WinApi.GetWindowRect(client.MainWindowHandle, ref r);
           return new Rect(r);
           }
           public string Title
           {
               get { return client.Process.MainWindowTitle; }
               set { Util.WinApi.SetWindowText(client.MainWindowHandle, value); }
           }

            public Size GameWindowSize()
            {

                int num = client.Memory.ReadInt32(client.Addresses.Client.GuiAddress);
                num = client.Memory.ReadInt32(num + 0x30);
                return new Size(client.Memory.ReadInt32(num + 0x38), client.Memory.ReadInt32(num + 0x3c));
            }
            public Point GameWindowStart()
            {
                int num = client.Memory.ReadInt32(client.Addresses.Client.GuiAddress);
                num = client.Memory.ReadInt32(num + 0x30);
                num = client.Memory.ReadInt32(num + 0x24);
                return new Point(client.Memory.ReadInt32(num + 0x14), client.Memory.ReadInt32(num + 0x18));
            }
            public double TileSize()
            {
                return GameWindowSize().Height / 11;
            }
          
            public Point LocationToPoint(Location loc)
            {
                int CenterX, CenterY;
                int xdiff, ydiff;
                int newx, newy;

                CenterX = GameWindowStart().X + (int)(TileSize() * 8) - (int)TileSize() / 2;
                CenterY = GameWindowStart().Y + (int)(TileSize() * 6) - (int)TileSize() / 2;


                xdiff = loc.X - client.PlayerLocation.X;
                ydiff = loc.Y - client.PlayerLocation.Y;

                newx = CenterX + (int)(TileSize() * xdiff);
                newy = CenterY + (int)(TileSize() * ydiff);

                return new Point(newx, newy);

            }




        }
    }
}
