using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Adresses
{
    public class Inventory
    {

        public uint SlotStart;
        public int DistanceCount;
        public uint DistanceId;
        public int StepSlot;
        public byte MaxSlots;


        private int baseAddress;
        public Inventory(Objects.Client client)
        {
            baseAddress = client.Process.MainModule.BaseAddress.ToInt32();


        }
        public void RecalcAddresses()
        {
        
            SlotStart = SlotStart.RecalAddress(baseAddress);
        }

    }
}
