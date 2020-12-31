using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MemoryScanner;
namespace MemoryScanner
{
    public class Main
    {
        private Process Process;
        MemoryScanner memScan;
        MemoryReader memRead;
        List<Addresses.GetAddresses> list;
        public Main(Process m_process)
        {
            Process = m_process;
            memScan = new MemoryScanner(m_process);
            memRead = new MemoryReader(m_process);
            list = new List<Addresses.GetAddresses>();
            SetUp();

        }
        private void SetUp()
        {

            Addresses.MyAddresses.Food = new Addresses.Food(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.Inventory = new Addresses.InventoryStart(memRead, memScan, Addresses.GetAddresses.AddressType.Client);

            Addresses.MyAddresses.LastSeenId = new Addresses.lastSeenID(memRead, memScan, Addresses.GetAddresses.AddressType.Client);
            Addresses.MyAddresses.AttackCount = new Addresses.AttackCount(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.GuiPointer = new Addresses.GuiPointer(memRead, memScan, Addresses.GetAddresses.AddressType.Client);
            Addresses.MyAddresses.CreatePacket = new Addresses.CreatePacket(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.AddPacketByte = new Addresses.AddPacketByte(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.Experience = new Addresses.Experience(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.Level = new Addresses.Level(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.MapPointer = new Addresses.MapPointer(memRead, memScan, Addresses.GetAddresses.AddressType.Map);
            Addresses.MyAddresses.MapArray = new Addresses.MapArray(memRead, memScan, Addresses.GetAddresses.AddressType.Map);
            Addresses.MyAddresses.FullLight = new Addresses.FullLight(memRead, memScan, Addresses.GetAddresses.AddressType.Map);
            Addresses.MyAddresses.StepTile = new Addresses.StepTile(memRead, memScan, Addresses.GetAddresses.AddressType.Map);
            Addresses.MyAddresses.Mc = new Addresses.Mc(memRead, memScan, Addresses.GetAddresses.AddressType.Client);
            Addresses.MyAddresses.WalkFunction = new Addresses.WalkFunction(memRead, memScan, Addresses.GetAddresses.AddressType.InternalFunction);
            Addresses.MyAddresses.SendPacket = new Addresses.SendPacket(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.Health = new Addresses.Health(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.XorKey = new Addresses.XorKey(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.ManaMax = new Addresses.ManaMax(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.PeekMessageA = new Addresses.PeekMessageA(memRead, memScan, Addresses.GetAddresses.AddressType.Client);
            Addresses.MyAddresses.Mana = new Addresses.Mana(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.PlayerId = new Addresses.PlayerId(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.PlayerX = new Addresses.PlayerX(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.PlayerY = new Addresses.PlayerY(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.PlayerZ = new Addresses.PlayerZ(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.RedSqare = new Addresses.RedSquare(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.Cap = new Addresses.Cap(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.GetnextPacket = new Addresses.GetNextPacket(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.ParseFunction = new Addresses.ParseFunction(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.Status = new Addresses.Status(memRead, memScan, Addresses.GetAddresses.AddressType.Client);
            Addresses.MyAddresses.ReciveStream = new Addresses.ReciveStream(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.PlayerFlags = new Addresses.PlayerFlags(memRead, memScan, Addresses.GetAddresses.AddressType.Player);
            Addresses.MyAddresses.BlistStart = new Addresses.BlistStart(memRead, memScan, Addresses.GetAddresses.AddressType.BattleList);
            Addresses.MyAddresses.BlistStep = new Addresses.BlistStep(memRead, memScan, Addresses.GetAddresses.AddressType.BattleList);
            Addresses.MyAddresses.MaxCreatures = new Addresses.MaxCreatures(memRead, memScan, Addresses.GetAddresses.AddressType.BattleList);
            Addresses.MyAddresses.ContainerPointer = new Addresses.ContainerPointer(memRead, memScan, Addresses.GetAddresses.AddressType.Container);
            Addresses.MyAddresses.RecivePointer = new Addresses.RecivePointer(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.SendPointer = new Addresses.SendPointer(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.StatusBarText = new Addresses.StatusBarText(memRead, memScan, Addresses.GetAddresses.AddressType.Client);
            Addresses.MyAddresses.StatusBarTime = new Addresses.StatusBarTime(memRead, memScan, Addresses.GetAddresses.AddressType.Client);
            Addresses.MyAddresses.OutGoingBuffer = new Addresses.OutgoingBuffer(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.OutGoingPacketLen = new Addresses.OutGoingPacketLen(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.XTEA = new Addresses.Xtea(memRead, memScan, Addresses.GetAddresses.AddressType.Packet);
            Addresses.MyAddresses.AttackFunction = new Addresses.Attack(memRead, memScan, Addresses.GetAddresses.AddressType.InternalFunction);
            Addresses.MyAddresses.SpeakFunciton = new Addresses.SpeakFunction(memRead, memScan, Addresses.GetAddresses.AddressType.InternalFunction);
            Addresses.MyAddresses.ItemMoveFunc = new Addresses.ItemMove(memRead, memScan, Addresses.GetAddresses.AddressType.InternalFunction);
            Addresses.MyAddresses.ItemUseFunc = new Addresses.ItemUse(memRead, memScan, Addresses.GetAddresses.AddressType.InternalFunction);
            Addresses.MyAddresses.PingAddress = new Addresses.Ping(memRead, memScan, Addresses.GetAddresses.AddressType.Client);

            Addresses.MyAddresses.PrintFPS = new Addresses.PrintFps(memRead, memScan, Addresses.GetAddresses.AddressType.TextDisplay);
            Addresses.MyAddresses.PrintText = new Addresses.PrintText(memRead, memScan, Addresses.GetAddresses.AddressType.TextDisplay);
            Addresses.MyAddresses.ShowFPS = new Addresses.ShowFPS(memRead, memScan, Addresses.GetAddresses.AddressType.TextDisplay);
            Addresses.MyAddresses.NopFPS = new Addresses.NopFps(memRead, memScan, Addresses.GetAddresses.AddressType.TextDisplay);
        }
       public void StartSearch()
        {
            list.Add(Addresses.MyAddresses.Inventory);
            list.Add(Addresses.MyAddresses.LastSeenId);
            list.Add(Addresses.MyAddresses.Food);
            list.Add(Addresses.MyAddresses.AttackCount);
            list.Add(Addresses.MyAddresses.PeekMessageA);
            list.Add(Addresses.MyAddresses.GuiPointer);
            list.Add(Addresses.MyAddresses.CreatePacket);
            list.Add(Addresses.MyAddresses.AddPacketByte);
            list.Add(Addresses.MyAddresses.Experience);
            list.Add(Addresses.MyAddresses.Level);
            list.Add(Addresses.MyAddresses.MapPointer);
            list.Add(Addresses.MyAddresses.MapArray);
            list.Add(Addresses.MyAddresses.FullLight);
            list.Add(Addresses.MyAddresses.StepTile);
            list.Add(Addresses.MyAddresses.Mc);
            list.Add(Addresses.MyAddresses.WalkFunction);
            list.Add(Addresses.MyAddresses.SendPacket);
            list.Add(Addresses.MyAddresses.Health);
            list.Add(Addresses.MyAddresses.XorKey);
            list.Add(Addresses.MyAddresses.Mana);
            list.Add(Addresses.MyAddresses.ManaMax);
            list.Add(Addresses.MyAddresses.Cap);
            list.Add(Addresses.MyAddresses.PlayerId);
            list.Add(Addresses.MyAddresses.PlayerX);
            list.Add(Addresses.MyAddresses.PlayerY);
            list.Add(Addresses.MyAddresses.PlayerZ);
            list.Add(Addresses.MyAddresses.RedSqare);
            list.Add(Addresses.MyAddresses.PlayerFlags);
            list.Add(Addresses.MyAddresses.GetnextPacket);
            list.Add(Addresses.MyAddresses.ParseFunction);
            list.Add(Addresses.MyAddresses.Status);
            list.Add(Addresses.MyAddresses.ReciveStream);
            list.Add(Addresses.MyAddresses.PrintFPS);
            list.Add(Addresses.MyAddresses.PrintText);
            list.Add(Addresses.MyAddresses.ShowFPS);
            list.Add(Addresses.MyAddresses.NopFPS);
            list.Add(Addresses.MyAddresses.BlistStart);
            list.Add(Addresses.MyAddresses.BlistStep);
            list.Add(Addresses.MyAddresses.MaxCreatures);
            list.Add(Addresses.MyAddresses.ContainerPointer);
            list.Add(Addresses.MyAddresses.RecivePointer);
            list.Add(Addresses.MyAddresses.SendPointer);
            list.Add(Addresses.MyAddresses.StatusBarText);
            list.Add(Addresses.MyAddresses.StatusBarTime);
            list.Add(Addresses.MyAddresses.OutGoingBuffer);
            list.Add(Addresses.MyAddresses.OutGoingPacketLen);
            list.Add(Addresses.MyAddresses.XTEA);
            list.Add(Addresses.MyAddresses.AttackFunction);
            list.Add(Addresses.MyAddresses.SpeakFunciton);
            list.Add(Addresses.MyAddresses.ItemUseFunc);
            list.Add(Addresses.MyAddresses.ItemMoveFunc);
            list.Add(Addresses.MyAddresses.PingAddress);
            foreach (Addresses.GetAddresses val in list)
            {
                val.GetString();
            }
        }
    }
}
