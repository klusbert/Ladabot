using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Adresses
{
   public static  class DatItem
    {
       /*
        
       */
        public static uint StepItems = 132;// 1021
        //public static uint StepItems = 136;// 1020
        public static uint Width = 32;
        public static uint Height = 36;
        public static uint MaxSizeInPixels = 40;
        public static uint Layers = 44;
        public static uint PatternX = 48;
        public static uint PatternY = 52;
        public static uint PatternDepth = 56;
        public static uint Phase = 60;
        public static uint Sprite = 64;
        public static uint Flags = 40;
        public static uint CanEquip = 0;
        public static uint CanLookAt = 0;
        public static uint WalkSpeed = 76;
        public static uint TextLimit = 80;
        public static uint LightRadius = 84;
        public static uint LightColor = 88;
        public static uint ShiftX = 92;
        public static uint ShiftY = 96;
        public static uint WalkHeight = 100;
        public static uint Automap = 104; // Minimap color
        //   public static uint LensHelp = 108;
        public static uint LensHelp = 132;

        public static uint ClothSlot = 112;
        public static uint MarketCategory = 116;
        public static uint MarketTradeAs = 120;
        public static uint MarketShowAs = 124;
        public static uint MarketName;
        public static uint MarketRestrictProfession = 128;
        public static uint MarketRestrictLevel = 132;
        private static readonly Dictionary<Flag, ulong> flagOffsets954 = new Dictionary<Flag, ulong>()
        { 
            { Flag.IsGround, 1 },
            { Flag.TopOrder1, 2 },
            { Flag.TopOrder2, 4 },
            { Flag.TopOrder3, 8 },
            { Flag.IsContainer, 16 },
            { Flag.IsStackable, 32 },
            { Flag.IsCorpse, 64 },
            { Flag.IsUsable, 128 },
            { Flag.IsWritable, 256 },
            { Flag.IsReadable, 512 },
            { Flag.IsFluidContainer, 1024 },
            { Flag.IsSplash, 2048 },
            { Flag.Blocking, 4096 },
            { Flag.IsImmovable, 8192 },
            { Flag.BlocksMissiles, 16384 },
            { Flag.BlocksPath, 32768 },
            { Flag.IsPickupable, 65536 },
            { Flag.IsHangable, 131072 },
            { Flag.IsHangableHorizontal, 262144 },
            { Flag.IsHangableVertical, 524288 },
            { Flag.IsRotatable, 1048576 },
            { Flag.IsLightSource, 2097152 },
            { Flag.DoNotHide, 4194304 },
            { Flag.Translucent, 8388608 },
            { Flag.IsShifted, 16777216 },
            { Flag.HasHeight, 33554432 },
            { Flag.LyingObject, 67108864 },
            { Flag.IsIdleAnimation, 134217728 },
            { Flag.HasAutoMapColor, 268435456 },
            { Flag.HasHelpLens, 536870912 },
            { Flag.FullBank, 1073741824 },
            { Flag.IgnoreStackpos, 2147483648 },
            { Flag.Clothes, 4294967296 },
            { Flag.Market, 8589934592 }
                           
        };
        public static ulong GetFlagOffset(Flag flag)
        {
            ulong offset;
            flagOffsets954.TryGetValue(flag, out offset);
           
            return offset;
        }
        public enum Flag : uint
        {
            IsGround,
            TopOrder1,
            TopOrder2,
            TopOrder3,
            IsContainer,
            IsStackable,
            IsCorpse,
            IsUsable,
            IsRune,
            IsWritable,
            IsReadable,
            IsFluidContainer,
            IsSplash,
            Blocking,
            IsImmovable,
            BlocksMissiles,
            BlocksPath,
            IsPickupable,
            IsHangable,
            IsHangableHorizontal,
            IsHangableVertical,
            IsRotatable,
            IsLightSource,
            DoNotHide,
            Translucent,
            Floorchange,
            IsShifted,
            HasHeight,
            IsLayer,
            LyingObject,
            IsIdleAnimation,
            HasAutoMapColor,
            HasHelpLens,
            FullBank,
            Unknown,
            IgnoreStackpos,
            Clothes,
            Market
        }
        public enum Help
        {
            IsLadder = 0x44C,
            IsSewer = 0x44D,
            IsDoor = 0x450,
            IsDoorWithLock = 0x451,
            IsRopeSpot = 0x44E,
            IsSwitch = 0x44F,
            IsStairs = 0x452,
            IsMailbox = 0x453,
            IsDepot = 0x454,
            IsTrash = 0x455,
            IsHole = 0x456,
            HasSpecialDescription = 0x457,
            IsReadOnly = 0x458
        }

    }
              


          
}
