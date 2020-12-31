using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace KonjoBot.Objects
{

  public class DatReader
    {
        public ItemData[] dataArray;
      public enum DAT
      {
        IsGround = 0,
        TopOrder1 = 1,
        TopOrder2 = 2,
        TopOrder3 = 3,
        IsContainer = 4,
        IsStackable = 5,
        IsCorpse = 6,
        IsUsable = 254,   
        IsMultiUse = 7,
        IsWritable = 8,
        IsReadable = 9,
        IsFluidContainer = 10,
        IsSplash = 11,
        Blocking = 12,
        IsImmovable = 13,
        BlocksMissiles = 14,
        BlocksPath = 15,
        noMovmentAction = 16,
        IsPickupable = 17,
        IsHangable = 18,
        IsHangableHorizontal = 19,
        IsHangableVertical = 20,
        IsRotatable = 21,
        IsLightSource = 22,
       Flag_DontHide = 23,
       Flag_Translucent = 24,
       Flag_Shift = 25,
       Flag_Height = 26,
       Flag_LyingObject = 27,
       Flag_AnimateAlways = 28,
       Flag_Automap = 29,
       Flag_LensHelp = 30,
       Flag_FullBank = 31,
       Flag_IgnoreLook = 32,
       Flag_Clothes = 33,
       Flag_Market = 34,
       Flag_DefaultAction = 35,
       BreakFlag = 255,

      }
        public int MaxItems;
    
        public enum MarketCategory : byte
        {
            Armors = 1,
            Amulets = 2,
            Boots = 3,
            Containers = 4,
            Decoration = 5,
            Food = 6,
            Helmets_Hats = 7,
            Legs = 8,
            Others = 9,
            Potions = 10,
            Rings = 11,
            Runes = 12,
            Shields = 13,
            Tools = 14,
            Valuables = 15,
            Ammunition = 16,
            Axes = 17,
            Clubs = 18,
            DistanceWeapons = 19,
            Swords = 20,
            Wands_Rods = 21,
            MetaWeapons = 22
            //all weapons
        }

        public struct ItemData
        {
            public int ID;

            public bool IsGround;
            public int Waypoints;
            public bool TopOrder1;
            public bool TopOrder2;
            public bool TopOrder3;
            public bool IsContainer;
            public bool IsStackable;
            public bool isUseAble;
            public bool IsCorpse;
            public bool isMultiUse;
            public bool isWriteable;
            public int MaxTextLength;
            public bool isWriteableOnce;
            public bool isLiquidContainer;
            public bool isLiquidPool;
            public bool Blocking;
            public bool isUnmoveable;
            public bool BlocksMissiles;
            public bool BlocksPath;
            public bool isTakeable;
            public bool isHangable;
            public bool isHookSouth;
            public bool isHookEast;
            public bool isRotateable;
            public bool noMovmentAction;
            public bool isLight;
            public int Brightness;
            public int LightColor;
            public bool isDontHide;
            public bool isTranslucent;
            public bool isDisplaced;
            public int DisplacementX;
            public int DisplacementY;
            public bool isHeight;
            public int Elevation;
            public bool isLyingObject;
            public bool isAnimateAlways;
            public bool isAutomap;
            public int isAutomapColor;
            public bool isLensHelp;
            public int LensHelp;
            public bool isFullBank;
            public bool isIgnoreLook;
            public bool isCloth;
            public bool FloorChangeUp;
            public bool FloorChangeDown;
            public bool RequireUse;
            public bool RequireShovel;
            public bool RequireRope;
            public int ClothSlot;
            public bool isMarket;
            public MarketCategory MarketCategory;
            public int MarketTradeAs;
            public int MarketShowAs;
            public string MarketName;
            public int MarketRestrictProfession;
            public int MarketRestrictLevel;
            public bool DefaultAction;
            public int Width;
            public int Height;
            public int ExactSize;
            public int Layers;
            public int PatternWidth;
            public int PatternHeight;
            public int PatternDepth;
            public int Phases;
            public int NumberOfSprites;

            public int[] Sprites;
        }
        private Client client;
        private string path;
        private List<ItemData> Objects;
        public DatReader(Client _client)
        {
            client = _client;
           path = Path.Combine(Path.GetDirectoryName(client.Process.MainModule.FileName), "Tibia.dat");
           ParseDat(path);
        }
        public ItemData GetData(int id)
        {
            if (id >= 100 && id - 100 <= Objects.Count)
            {

              //  return dataArray[id - 100];
                return Objects[id - 100];
            }
            return new ItemData();
        }
        public void ParseDat(string path)
        {
            string FileName = path;
   
            using (System.IO.BinaryReader BinaryReader = new System.IO.BinaryReader(System.IO.File.OpenRead(FileName)))
            {
                uint DatVersion = BinaryReader.ReadUInt32();
                int ItemCount = BinaryReader.ReadUInt16();
                int OutfitCount = BinaryReader.ReadUInt16();
                int EffectCount = BinaryReader.ReadUInt16();
                int ProjectileCount = BinaryReader.ReadUInt16();
                int MaxID = ItemCount + OutfitCount + EffectCount + ProjectileCount;
                Objects = new List<ItemData>(MaxID);
                int ID = 100;
                DAT Flag = 0;

                while (ID < MaxID)
                {
                    ItemData CurrentObject = new ItemData();
                    CurrentObject.ID = ID;
                    do
                    {
                        Flag = (DAT)BinaryReader.ReadByte();
                        switch (Flag)
                        {
                            case DAT.IsGround:
                                CurrentObject.IsGround = true;
                                CurrentObject.Waypoints = BinaryReader.ReadUInt16();
                                break;
                            case DAT.TopOrder1:
                                CurrentObject.TopOrder1 = true;
                                break;
                            case DAT.TopOrder2:
                                CurrentObject.TopOrder2 = true;
                                break;
                            case DAT.TopOrder3:
                                CurrentObject.TopOrder3 = true;
                                break;
                            case DAT.IsContainer:
                                CurrentObject.IsContainer = true;
                                break;
                            case DAT.IsStackable:
                                CurrentObject.IsStackable = true;
                                break;
                            case DAT.IsUsable:
                                CurrentObject.isUseAble = true;
                                break;
                            case DAT.IsCorpse:
                                CurrentObject.IsCorpse = true;
                                break;
                            case DAT.IsMultiUse:
                                CurrentObject.isMultiUse = true;
                                break;
                            case DAT.IsWritable:
                                CurrentObject.isWriteable = true;
                                CurrentObject.MaxTextLength = BinaryReader.ReadUInt16();
                                break;
                            case DAT.IsReadable:
                                CurrentObject.isWriteableOnce = true;
                                CurrentObject.MaxTextLength = BinaryReader.ReadUInt16();
                                break;
                            case DAT.IsFluidContainer:
                                CurrentObject.isLiquidContainer = true;
                                break;
                            case DAT.IsSplash:
                                CurrentObject.isLiquidPool = true;
                                break;
                            case DAT.Blocking:
                                CurrentObject.Blocking = true;
                                break;
                            case DAT.IsImmovable:
                                CurrentObject.isUnmoveable = true;
                                break;
                            case DAT.BlocksMissiles:
                                CurrentObject.BlocksMissiles = true;
                                break;
                            case DAT.BlocksPath:
                                CurrentObject.BlocksPath = true;
                                break;
                            case DAT.noMovmentAction:
                                CurrentObject.noMovmentAction = true;
                                break;
                            case DAT.IsPickupable:
                                CurrentObject.isTakeable = true;
                                break;
                            case DAT.IsHangable:
                                CurrentObject.isHangable = true;
                                break;
                            case DAT.IsHangableHorizontal:
                                CurrentObject.isHookSouth = true;
                                break;
                            case DAT.IsHangableVertical:
                                CurrentObject.isHookEast = true;
                                break;
                            case DAT.IsRotatable:
                                CurrentObject.isRotateable = true;
                                break;
                            case DAT.IsLightSource:
                                CurrentObject.isLight = true;
                                CurrentObject.Brightness = BinaryReader.ReadUInt16();
                                CurrentObject.LightColor = BinaryReader.ReadUInt16();
                                break;
                            case DAT.Flag_DontHide:
                                CurrentObject.isDontHide = true;
                                break;
                            case DAT.Flag_Translucent:
                                CurrentObject.isTranslucent = true;
                                break;
                            case DAT.Flag_Shift:
                                CurrentObject.isDisplaced = true;
                                CurrentObject.DisplacementX = BinaryReader.ReadUInt16();
                                CurrentObject.DisplacementY = BinaryReader.ReadUInt16();
                                break;
                            case DAT.Flag_Height:
                                CurrentObject.isHeight = true;
                                CurrentObject.Elevation = BinaryReader.ReadUInt16();
                                break;
                            case DAT.Flag_LyingObject:
                                CurrentObject.isLyingObject = true;
                                break;
                            case DAT.Flag_AnimateAlways:
                                CurrentObject.isAnimateAlways = true;
                                break;
                            case DAT.Flag_Automap:
                                CurrentObject.isAutomap = true;
                                CurrentObject.isAutomapColor = BinaryReader.ReadUInt16();
                                break;
                            case DAT.Flag_LensHelp:
                                CurrentObject.isLensHelp = true;
                                CurrentObject.LensHelp = BinaryReader.ReadUInt16();
                                break;
                            case DAT.Flag_FullBank:
                                CurrentObject.isFullBank = true;
                                break;
                            case DAT.Flag_IgnoreLook:
                                CurrentObject.isIgnoreLook = true;
                                break;
                            case DAT.Flag_Clothes:
                                CurrentObject.isCloth = true;
                                CurrentObject.ClothSlot = BinaryReader.ReadUInt16();
                                break;
                            case DAT.Flag_DefaultAction:
                                CurrentObject.DefaultAction = true;
                                int Action = BinaryReader.ReadUInt16();                            
                                break;
                            case DAT.Flag_Market:
                                CurrentObject.isMarket = true;
                                CurrentObject.MarketCategory = (MarketCategory)BinaryReader.ReadUInt16();
                                CurrentObject.MarketTradeAs = BinaryReader.ReadUInt16();
                                CurrentObject.MarketShowAs = BinaryReader.ReadUInt16();
                                int MarketNameLength = BinaryReader.ReadUInt16();
                                CurrentObject.MarketName = System.Text.Encoding.GetEncoding("iso-8859-1").GetString(BinaryReader.ReadBytes(MarketNameLength));
                                CurrentObject.MarketRestrictProfession = BinaryReader.ReadUInt16();
                                CurrentObject.MarketRestrictLevel = BinaryReader.ReadUInt16();
                                break;
                            case DAT.BreakFlag:
                                
                                break;
                            default:
                                break;
                        }
                    } while ((byte)Flag != 255);
                    int FrameGroupCount = (ID > ItemCount && ID <= (ItemCount + OutfitCount)) ? BinaryReader.ReadByte() : 1;
                    for (int k = 0; k < FrameGroupCount; k++)
                    {
                        int FrameGroupID = (ID > ItemCount && ID <= (ItemCount + OutfitCount)) ? BinaryReader.ReadByte() : 0;
                 
                    CurrentObject.Width = BinaryReader.ReadByte();
                    CurrentObject.Height = BinaryReader.ReadByte();
                    if (CurrentObject.Width > 1 | CurrentObject.Height > 1)
                    {
                        CurrentObject.ExactSize = BinaryReader.ReadByte();
                    }

                    CurrentObject.Layers = BinaryReader.ReadByte();
                    CurrentObject.PatternWidth = BinaryReader.ReadByte();
                    CurrentObject.PatternHeight = BinaryReader.ReadByte();
                    CurrentObject.PatternDepth = BinaryReader.ReadByte();
                    CurrentObject.Phases = BinaryReader.ReadByte();
                    if (CurrentObject.Phases > 1)
                    {

                        int loc8 = 0;
                        byte unkNown = BinaryReader.ReadByte();
                        int unknown1 = BinaryReader.ReadInt32();
                        byte unkNown2 = BinaryReader.ReadByte();
                        while (loc8 < CurrentObject.Phases)
                        {
                            int unknown3 = BinaryReader.ReadInt32();
                            int unknown4 = BinaryReader.ReadInt32();

                            loc8 += 1;

                        }
                    }
                    int numSpr = CurrentObject.Width * CurrentObject.Height;
                    numSpr *= CurrentObject.Layers * CurrentObject.PatternWidth;
                    numSpr *= CurrentObject.PatternHeight * CurrentObject.PatternDepth;
                    numSpr *= CurrentObject.Phases;

                    CurrentObject.NumberOfSprites = numSpr;

                    CurrentObject.Sprites = (int[])Array.CreateInstance(typeof(int), CurrentObject.NumberOfSprites);
                    for (int i = 0; i <= CurrentObject.NumberOfSprites - 1; i++)
                    {
                        CurrentObject.Sprites[i] = BinaryReader.ReadInt32();
                    }
                    }
                    ID++;
                    Objects.Add(CurrentObject);
                }
            }
        }
       
    }
}