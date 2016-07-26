using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Objects
{

        public struct Rect
        {
            private int top;
            private int bottom;
            private int left;
            private int right;
            private int width;
            private int height;

            public int Top
            {
                get { return top; }
            }

            public int Bottom
            {
                get { return bottom; }
            }

            public int Left
            {
                get { return left; }
            }

            public int Rigth
            {
                get { return right; }
            }

            public int Height
            {
                get { return height; }
            }

            public int Width
            {
                get { return width; }
            }

            public Rect(Util.WinApi.RECT r)
            {
                top = r.top;
                bottom = r.bottom;
                left = r.left;
                right = r.right;
                width = r.right - r.left;
                height = r.bottom - r.top;
            }
        }


    }

