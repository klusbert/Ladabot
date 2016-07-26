using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonjoBot.Adresses
{
    public class TextDisplay
    {
        public uint PrintTextFunction = 0x495E70;
        public uint PrintFps = 0x498049;
        public uint ShowFps = 0xAD7A11;
        public uint NopFps = 0x497EA7;
        private int baseAddress;
        public TextDisplay(Objects.Client client)
        {
            baseAddress = client.Process.MainModule.BaseAddress.ToInt32();
        }
        public void RecalcAddresses()
        {
            PrintTextFunction = PrintTextFunction.RecalAddress(baseAddress);
            PrintFps = PrintFps.RecalAddress(baseAddress);
            ShowFps = ShowFps.RecalAddress(baseAddress);
            NopFps = NopFps.RecalAddress(baseAddress);

        }
    }
}
