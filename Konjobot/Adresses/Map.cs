using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Adresses
{
  public class Map
    {

      //maptileaddress = maptileNumber * 168 +firstmaptile(readmappointer())
      public uint MapPointer = 0xB73FF4;

      public uint TileNumberArray = 0xB78B24;
      public uint FullLightTrick = 0x587BB2;
      public byte[] FullLightTrickBytes = new byte[] { 0xBB, 0xFF, 0x00, 0x00, 0x00, 0xEB, 0x11, 0x90, 0x90, 0x90 };
      public byte[] OrginalLight = new byte[] { 0x8B, 0x1D, 0x00, 0x59, 0x94, 0x00, 0x85, 0xDB, 0x79, 0x04 };

      public uint StepTile = 368;
      public uint StepTileObject = 32;
      public uint DistanceTileItemsCount = 0;
      public uint DistanceTileItemOrder = 4;
      public uint DistanceTileItems = 44;
      public uint DistanceTileEffect = 164;
      public uint DistanceItemId = 8;
      public uint DistanceItemData = 4;
      public uint DistanceItemDataEx = 0;
      public uint MaxTileItems = 10;
      public uint MaxTiles = 2016;
      public uint MaxY =14;
      public uint MaxX = 18;
      public uint MaxZ = 8;
      public uint MaxTilesPerFloor;
      public uint TileNumberOffsetCenter = 116 *4;
      private int baseAddress;
      public Map(Objects.Client client)
      {
          baseAddress = client.Process.MainModule.BaseAddress.ToInt32();
          MaxTilesPerFloor = MaxTiles / MaxZ;
      }
      public void RecalcAddresses()
      {
          MapPointer = MapPointer.RecalAddress(baseAddress);
          TileNumberArray = TileNumberArray.RecalAddress(baseAddress);
          FullLightTrick = FullLightTrick.RecalAddress(baseAddress);
      }
      
    }
}
