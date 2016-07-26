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
            SetCurrentAddresses();
        }
        private void SetCurrentAddresses()
        {



            Battlelist.Start = 0xB33790;
            Battlelist.Step = 0xDC;
            Battlelist.MaxCreatures = 0x514;
            Battlelist.End = Battlelist.Start + (Battlelist.Step * Battlelist.MaxCreatures);
            Battlelist.RecalcAddresses();

            Client.GuiAddress = 0x93BA48;
            Client.Status = 0x94D264;
            Client.StatusbarText = 0x98EEA0;
            Client.StatusbarTime = 0x98EE98;
            Client.Ping = 0xAD9E60;
            Client.PeekMessage = 0x828884;
            Client.LastSeendId =0xAD55AC;
            Client.McAddress = 0x5AF8F7;
            Client.RecalcAddresses();

            Container.ContainerPointer = 0xB7FA4C;
            Container.RecalcAddresses();

            Internal_Functions.WalkFunction = 0x562F20;
            Internal_Functions.AttackFunction = 0x421260;
            Internal_Functions.SpeakFunction = 0x41F400;
            Internal_Functions.ItemUseFuction = 0x41DFA0;
            Internal_Functions.ItemMoveFunction = 0x41CF40;
            Internal_Functions.RecalcAddresses();

            Map.MapPointer = 0xB7998C;
            Map.TileNumberArray = 0xB7E4B8;
            Map.FullLightTrick = 0x58C892;
            Map.StepTile = 0x170;
            Map.RecalcAddresses();

            Player.Flags = 0x93B6DC;
            Player.Xor = 0x93B760;
            Player.Experince = 0x93B76C;
            Player.Level = 0x93B778;
            Player.Mana = 0x93B794;
            Player.ManaMax = 0x93B764;
            Player.HP = 0xAD9000;
            Player.Cap = 0xAD9040;
            Player.Id = 0xAD9050;
            Player.X = 0xAD9054;
            Player.Y = 0xAD9058;
            Player.Z = 0xAD905C;
            Player.RedSquare = 0x93B790;
            Player.AttackCount = 0xAD9E20;
            Player.RecalcAddresses();

            TextDisplay.NopFps = 0x49AB87;
            TextDisplay.PrintFps = 0x49AD29;
            TextDisplay.PrintTextFunction = 0x498B50;
            TextDisplay.ShowFps = 0xADEFDF;
            TextDisplay.RecalcAddresses();


            Packet.RecivePointer = 0x828A9C;
            Packet.SendPointer = 0x828A58;
            Packet.XteaKey = 0x925BA4;
            Packet.CreatePacket = 0x5977F0;
            Packet.AddByteFunc = 0x597BB0;
            Packet.SENDOUTGOINGPACKET = 0x5986C0;
            Packet.INCOMINGDATASTREAM = 0xB7E4FC;
            Packet.PARSERFUNC = 0x49DDC0;
            Packet.GetNextPacketCall = 0x49DE02;
            Packet.SendPacketData = 0x94BEA0;
            Packet.SendPacketLenght = 0xB7E510;
            Packet.RecalcAddress();

        }
    }
}
