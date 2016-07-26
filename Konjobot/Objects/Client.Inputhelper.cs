using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace KonjoBot.Objects
{
   public partial class Client
    {
       public class InputHelper
       {
 
           Client client;
           public InputHelper(Client cl)
           {
               client = cl;
           }
           public void SendKey(Keys key,bool withchar = true)
           {

               SendMessage(Util.WinApi.WM_KEYDOWN, Convert.ToUInt32(key), 0);
               if (withchar)
               {
                   SendMessage(Util.WinApi.WM_CHAR, Convert.ToUInt32(key), 0);
               }
               
               SendMessage(Util.WinApi.WM_KEYUP, Convert.ToUInt32(key), 0);
           }
           public void SendString(string s)
           {
               foreach (var c in s)
                   SendMessage(Util.WinApi.WM_CHAR, Convert.ToUInt32(c), 0);
           }
           public void CtrlA()
           {
               SendMessage(Util.WinApi.WM_KEYDOWN, 0x11, 0x1d0001);
               SendMessage(Util.WinApi.WM_KEYDOWN, 0x41, 0x1e0001);
               SendMessage(Util.WinApi.WM_CHAR, 0x1, 0x1e0001);
               SendMessage(Util.WinApi.WM_KEYUP, 0x41, 0xc01e0001);
               SendMessage(Util.WinApi.WM_KEYUP, 0x11, 0xc01e0001);


           }

           public void SendMessage(uint MessageId, uint wParam, uint lParam)
           {
               Util.WinApi.SendMessage(client.MainWindowHandle, MessageId, new UIntPtr(wParam), new UIntPtr(lParam));
           }
           public void LeftClick(uint x, uint y)
           {
               SendMessage(Util.WinApi.WM_LBUTTONUP, 0, 0);
               uint lpara = Util.WinApi.MakeLParam(x, y);
               SendMessage(Util.WinApi.WM_LBUTTONDOWN, 0, lpara);
               SendMessage(Util.WinApi.WM_LBUTTONUP, 0, lpara);
           }
           public void RightClick(uint x, uint y)
           {
               SendMessage(Util.WinApi.WM_RBUTTONUP, 0, 0);
               uint lpara = Util.WinApi.MakeLParam(x, y);
               SendMessage(Util.WinApi.WM_RBUTTONDOWN, 0, lpara);
               SendMessage(Util.WinApi.WM_RBUTTONUP, 0, lpara);
           }
           public void Look(uint x, uint y)
           {
               SendMessage(Util.WinApi.WM_RBUTTONUP, 0, 0);
               SendMessage(Util.WinApi.WM_LBUTTONUP, 0, 0);
               uint lpara = Util.WinApi.MakeLParam(x, y);
               SendMessage(Util.WinApi.WM_LBUTTONDOWN, 0, lpara);
               SendMessage(Util.WinApi.WM_RBUTTONDOWN, 0, lpara);
               Thread.Sleep(50);
               SendMessage(Util.WinApi.WM_LBUTTONUP, 0, lpara);
               SendMessage(Util.WinApi.WM_RBUTTONUP, 0, lpara);
           }
           public void Drag(Point start, Point end, bool useCtrl =true  )
           {
               uint lpara = Util.WinApi.MakeLParam((uint)start.X, (uint)start.Y);
               uint lpara2 = Util.WinApi.MakeLParam((uint)end.X, (uint)end.Y);
               SendMessage(Util.WinApi.WM_LBUTTONDOWN, 0, lpara);
               Thread.Sleep(100);
               Util.WinApi.SetCursorPos(Cursor.Position.X + 1, Cursor.Position.Y - 1);
               Thread.Sleep(100);
               Util.WinApi.SetCursorPos(Cursor.Position.X - 1, Cursor.Position.Y + 1);

               if (useCtrl)
               {
                   SendMessage(Util.WinApi.WM_KEYDOWN, 0x11, 0x1d0001);
                   SendMessage(Util.WinApi.WM_LBUTTONUP, 0, lpara2);
                   SendMessage(Util.WinApi.WM_KEYUP, 0x11, 0x1d0001);
               }
               else
               {
                   SendMessage(Util.WinApi.WM_LBUTTONUP, 0, lpara2);
                   SendKey(Keys.Enter);
               }
             
               Thread.Sleep(500);
           }
           public void RightClick(Point p)
           {
               RightClick((uint)p.X, (uint)p.Y);
           }
           public void LeftClick(Point p)
           {
               LeftClick((uint)p.X, (uint)p.Y);
           }
           public void Look(Point p)
           {
               Look((uint)p.X, (uint)p.Y);
           }
       }
    }
}
