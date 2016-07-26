using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Adresses
{
    public static class Hotkey
    {
        public static uint TextStart = 0x7BE698;
        public static uint ObjectUseTypeStart = 0x7C0AC8;
        public static uint SendAutomaticallyStart = 0x7C0B68;
        public static uint ObjectStart = 0x7C0C30;
        public static uint TextStep = 0x100;
        public static uint ObjectUseTypeStep = 0x4;
        public static uint SendAutomaticallyStep = 0x1;
        public static uint ObjectStep = 0x4;
        public static uint MaxHotkeys = 36;


    }
}
