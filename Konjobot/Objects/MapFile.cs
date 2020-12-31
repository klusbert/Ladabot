using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Objects
{
 public class MapFile
    {
        public MapFile()
        {
        }
        public int X;
        public int Y;
        public int Z;
        public byte[] MapMovment;
        public byte[] MapColor;
        public byte[] NoMapColor
        {
            get
            {
                byte[] color = new byte[65536];
                return color;
            }
        }
        public byte[] NoMapMovment
        {
            get
            {
                byte[] mov = new byte[65536];
                for (int i = 0; i < mov.Length; i++)
                {
                    mov[i] = 255;
                }
                return mov;
            }
        }

    }
}
