using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonjoBot.Adresses
{
   public  class Packet
    {
        public uint RecivePointer = 0x822A9C;
        public uint SendPointer = 0x822A58;
        public uint XteaKey = 0x91EBA4;
        public uint CreatePacket = 0x592AE0;
        public uint AddByteFunc = 0x592EA0;
        public uint SENDOUTGOINGPACKET = 0x5939B0;
        public uint INCOMINGDATASTREAM = 0xB78B68;
        public uint PARSERFUNC = 0x49B0E0;
        public uint GetNextPacketCall = 0x49B122;
        public uint SendPacketLenght = 0xB80040;
        public uint SendPacketData = 0x5986C0;
        private int baseAddress;
        public Packet(Objects.Client client)
        {
            baseAddress = client.Process.MainModule.BaseAddress.ToInt32();
        }
       public void RecalcAddress()
        {
            RecivePointer = RecivePointer.RecalAddress(baseAddress);
            SendPointer = SendPointer.RecalAddress(baseAddress);
            XteaKey = XteaKey.RecalAddress(baseAddress);
            CreatePacket = CreatePacket.RecalAddress(baseAddress);
            AddByteFunc = AddByteFunc.RecalAddress(baseAddress);
            SENDOUTGOINGPACKET = SENDOUTGOINGPACKET.RecalAddress(baseAddress);
            INCOMINGDATASTREAM = INCOMINGDATASTREAM.RecalAddress(baseAddress);
            PARSERFUNC = PARSERFUNC.RecalAddress(baseAddress);
            GetNextPacketCall = GetNextPacketCall.RecalAddress(baseAddress);
            SendPacketData = SendPacketData.RecalAddress(baseAddress);
            SendPacketLenght = SendPacketLenght.RecalAddress(baseAddress);


        }
    }
}
