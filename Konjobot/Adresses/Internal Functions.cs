using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonjoBot.Adresses
{
    public class Internal_Functions
    {
        public uint WalkFunction = 0x55E3C0;
        public uint SpeakFunction = 0x41F3C0;
        public uint ItemMoveFunction = 0x41CF00;
        public uint ItemUseFuction = 0x41DF60;
        public uint AttackFunction = 0x421220;
        private int BaseAddress;
        public Internal_Functions(Objects.Client client)
        {
            BaseAddress = client.Process.MainModule.BaseAddress.ToInt32();

        }
        public void RecalcAddresses()
        {
            WalkFunction = WalkFunction.RecalAddress(BaseAddress);
            SpeakFunction = SpeakFunction.RecalAddress(BaseAddress);
            ItemMoveFunction = ItemMoveFunction.RecalAddress(BaseAddress);
            ItemUseFuction = ItemUseFuction.RecalAddress(BaseAddress);
            AttackFunction = AttackFunction.RecalAddress(BaseAddress);

        }
    }
}
