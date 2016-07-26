using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Adresses
{
  public  class Container
    {
      public uint ContainerPointer = 0xB7A2A4;
     
      public uint MaxContainers = 16;
      
      public uint DistanceIndex = 0;
      public uint DistanceUnknown1 = 4;
      public  uint DistanceUnknown2 = 8;
      public  uint DistanceId = 12;
      public  uint DistanceName = 36;
      public  uint DistanceVolume = 64;
      public  uint DistanceAmount = 68;
      public  uint DistanceItemStart = 76;

      public uint StepContainerSlot = 32;
      public uint DistanceItemCount = 4;
      public uint DistanceItemId = 8;
      public int baseAddress;
      public Container(Objects.Client client)
      {
          baseAddress = client.Process.MainModule.BaseAddress.ToInt32();
      }
      public void RecalcAddresses()
      {
          ContainerPointer = ContainerPointer.RecalAddress(baseAddress);
      }
    }
}
