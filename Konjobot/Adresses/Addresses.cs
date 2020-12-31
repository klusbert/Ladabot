using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonjoBot.Adresses
{
    public class Addresses
    {
        Objects.Client m_client;
        public Battlelist Battlelist;
        public Client Client;
        public Container Container;
        public Internal_Functions Internal_Functions;
        public Map Map;
        public Player Player;
        public Packet Packet;
        public TextDisplay TextDisplay;
        public Inventory Inventory;
    
        public Addresses(Objects.Client client)
        {
            m_client = client;
            Battlelist = new Battlelist(m_client);
            Client = new Client(m_client);
            Container = new Container(m_client);
            Internal_Functions = new Internal_Functions(m_client);
            Map = new Map(m_client);
            Packet = new Packet(m_client);
            Player = new Adresses.Player(m_client);
            TextDisplay = new TextDisplay(m_client);
            Inventory = new Adresses.Inventory(m_client);
          //  SetCurrentAddresses();
            setAddresses1(client.Process);
        }
        private void setAddresses1(System.Diagnostics.Process process)
        {
            MemoryScanner.Main m = new MemoryScanner.Main(process);
            m.StartSearch();
            
            Battlelist.Start =(uint)MemoryScanner.Addresses.MyAddresses.BlistStart.Address;
            Battlelist.Step = (uint)MemoryScanner.Addresses.MyAddresses.BlistStep.Address;
            Battlelist.MaxCreatures = (uint)MemoryScanner.Addresses.MyAddresses.MaxCreatures.Address;


            Client.GuiAddress = (uint)MemoryScanner.Addresses.MyAddresses.GuiPointer.Address;
            Client.Status = (uint)MemoryScanner.Addresses.MyAddresses.Status.Address;
            Client.StatusbarText = (uint)MemoryScanner.Addresses.MyAddresses.StatusBarText.Address;
            Client.StatusbarTime = (uint)MemoryScanner.Addresses.MyAddresses.StatusBarTime.Address;
            Client.Ping = (uint)MemoryScanner.Addresses.MyAddresses.PingAddress.Address;
            Client.PeekMessage = (uint)MemoryScanner.Addresses.MyAddresses.PeekMessageA.Address;
            Client.LastSeendId = (uint)MemoryScanner.Addresses.MyAddresses.LastSeenId.Address;
            Client.McAddress = (uint)MemoryScanner.Addresses.MyAddresses.Mc.Address;
            Client.Hunger = (uint)MemoryScanner.Addresses.MyAddresses.Food.Address;
      

            Container.ContainerPointer = (uint)MemoryScanner.Addresses.MyAddresses.ContainerPointer.Address;
   
            Internal_Functions.WalkFunction = (uint)MemoryScanner.Addresses.MyAddresses.WalkFunction.Address;
            Internal_Functions.AttackFunction = (uint)MemoryScanner.Addresses.MyAddresses.AttackFunction.Address;
            Internal_Functions.SpeakFunction = (uint)MemoryScanner.Addresses.MyAddresses.SpeakFunciton.Address;
            Internal_Functions.ItemUseFuction = (uint)MemoryScanner.Addresses.MyAddresses.ItemUseFunc.Address; 
            Internal_Functions.ItemMoveFunction = (uint)MemoryScanner.Addresses.MyAddresses.ItemMoveFunc.Address; 


            Inventory.SlotStart = (uint)MemoryScanner.Addresses.MyAddresses.Inventory.Address;   //Slot Start Ammo Count

            Inventory.DistanceCount = 0;
            Inventory.DistanceId = 4;
            Inventory.StepSlot = 32;
            Inventory.MaxSlots = 10;


            Map.MapPointer = (uint)MemoryScanner.Addresses.MyAddresses.MapPointer.Address; 
            Map.TileNumberArray = (uint)MemoryScanner.Addresses.MyAddresses.MapArray.Address; 
            Map.FullLightTrick = (uint)MemoryScanner.Addresses.MyAddresses.FullLight.Address;
            Map.StepTile = (uint)MemoryScanner.Addresses.MyAddresses.StepTile.Address;


            Player.Flags = (uint)MemoryScanner.Addresses.MyAddresses.PlayerFlags.Address;
            Player.Xor = (uint)MemoryScanner.Addresses.MyAddresses.XorKey.Address;
            Player.Experince = (uint)MemoryScanner.Addresses.MyAddresses.Experience.Address;
            Player.Level = (uint)MemoryScanner.Addresses.MyAddresses.Level.Address;
            Player.Mana = (uint)MemoryScanner.Addresses.MyAddresses.Mana.Address;
            Player.ManaMax = (uint)MemoryScanner.Addresses.MyAddresses.ManaMax.Address;
            Player.HP = (uint)MemoryScanner.Addresses.MyAddresses.Health.Address;
            Player.Cap = (uint)MemoryScanner.Addresses.MyAddresses.Cap.Address;
            Player.Id = (uint)MemoryScanner.Addresses.MyAddresses.PlayerId.Address;
            Player.X = (uint)MemoryScanner.Addresses.MyAddresses.PlayerX.Address;
            Player.Y = (uint)MemoryScanner.Addresses.MyAddresses.PlayerY.Address;
            Player.Z = (uint)MemoryScanner.Addresses.MyAddresses.PlayerZ.Address;
            Player.RedSquare = (uint)MemoryScanner.Addresses.MyAddresses.RedSqare.Address;
            Player.AttackCount = (uint)MemoryScanner.Addresses.MyAddresses.AttackCount.Address;

            TextDisplay.NopFps = (uint)MemoryScanner.Addresses.MyAddresses.NopFPS.Address;
            TextDisplay.PrintFps = (uint)MemoryScanner.Addresses.MyAddresses.PrintFPS.Address;
            TextDisplay.PrintTextFunction = (uint)MemoryScanner.Addresses.MyAddresses.PrintText.Address;
            TextDisplay.ShowFps = (uint)MemoryScanner.Addresses.MyAddresses.ShowFPS.Address;

            Packet.RecivePointer = (uint)MemoryScanner.Addresses.MyAddresses.RecivePointer.Address;
            Packet.SendPointer = (uint)MemoryScanner.Addresses.MyAddresses.SendPointer.Address;
            Packet.XteaKey = (uint)MemoryScanner.Addresses.MyAddresses.XTEA.Address;
            Packet.CreatePacket = (uint)MemoryScanner.Addresses.MyAddresses.CreatePacket.Address;
            Packet.AddByteFunc = (uint)MemoryScanner.Addresses.MyAddresses.AddPacketByte.Address;
            Packet.SENDOUTGOINGPACKET = (uint)MemoryScanner.Addresses.MyAddresses.SendPacket.Address;
            Packet.INCOMINGDATASTREAM = (uint)MemoryScanner.Addresses.MyAddresses.ReciveStream.Address;
            Packet.PARSERFUNC = (uint)MemoryScanner.Addresses.MyAddresses.ParseFunction.Address;
            Packet.GetNextPacketCall = (uint)MemoryScanner.Addresses.MyAddresses.GetnextPacket.Address;
            Packet.SendPacketData = (uint)MemoryScanner.Addresses.MyAddresses.OutGoingBuffer.Address;
            Packet.SendPacketLenght = (uint)MemoryScanner.Addresses.MyAddresses.OutGoingPacketLen.Address;

  
        }
        private void SetCurrentAddresses()
        {



            Battlelist.Start = 0xB6A0B0;
            Battlelist.Step = 0xDC;
            Battlelist.MaxCreatures = 0x514;
            Battlelist.End = Battlelist.Start + (Battlelist.Step * Battlelist.MaxCreatures);
            Battlelist.RecalcAddresses();

            Client.GuiAddress = 0x970744;
            Client.Status = 0x981FB4;
            Client.StatusbarText = 0x9C3BA0;
            Client.StatusbarTime = 0x9C3B98;
            Client.Ping = 0xB0E9F8;
            Client.PeekMessage = 0x851894;
            Client.LastSeendId = 0xb0A430;
            Client.McAddress = 0x5CE1B7;
            Client.Hunger = 0x970454;
            Client.RecalcAddresses();

            Container.ContainerPointer = 0xBB63DC;
            Container.RecalcAddresses();

            Internal_Functions.WalkFunction = 0x5811C0;
            Internal_Functions.AttackFunction = 0x422520;
            Internal_Functions.SpeakFunction = 0x4206C0;
            Internal_Functions.ItemUseFuction = 0x41F260;
            Internal_Functions.ItemMoveFunction = 0x41E200;
            Internal_Functions.RecalcAddresses();

            Inventory.SlotStart = 0xBB016C;  //Slot Start Ammo Count

            Inventory.DistanceCount = 0;
            Inventory.DistanceId = 4;
            Inventory.StepSlot = 32;
            Inventory.MaxSlots = 10;


            Map.MapPointer = 0xBB02D8;
            Map.TileNumberArray = 0xBB4E04;
            Map.FullLightTrick = 0x5AAD82;
            Map.StepTile = 0x170;
            Map.RecalcAddresses();

            Player.Flags = 0x9703DC;
            Player.Xor = 0x970458;
            Player.Experince = 0x970460;
            Player.Level = 0x970470;
            Player.Mana = 0x97048C;
            Player.ManaMax = 0x97045C;
            Player.HP = 0xB0E000;
            Player.Cap = 0xB0E040;
            Player.Id = 0xB0E050;
            Player.X = 0xB0E054;
            Player.Y = 0xB0E058;
            Player.Z = 0xB0E05C;
            Player.RedSquare = 0x970488;
            Player.AttackCount = 0xB1622C;
            Player.RecalcAddresses();

            TextDisplay.NopFps = 0x4A4E07;
            TextDisplay.PrintFps = 0x4A4FA9;
            TextDisplay.PrintTextFunction = 0x4A2DD0;
            TextDisplay.ShowFps = 0xB14359;
            TextDisplay.RecalcAddresses();


            Packet.RecivePointer = 0x851AAC;
            Packet.SendPointer = 0x851A4C;
            Packet.XteaKey = 0x958C34;
            Packet.CreatePacket = 0x5B5CC0;
            Packet.AddByteFunc = 0x5B6080;
            Packet.SENDOUTGOINGPACKET = 0x5B6BC0;
            Packet.INCOMINGDATASTREAM = 0xBB4E48;
            Packet.PARSERFUNC = 0x4A82A0;
            Packet.GetNextPacketCall = 0x4A82E2;
            Packet.SendPacketData = 0x980B90;
            Packet.SendPacketLenght = 0xBB4E5C;
            Packet.RecalcAddress();

        }
    }
}
