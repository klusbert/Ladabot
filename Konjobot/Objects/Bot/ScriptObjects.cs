﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Threading;
namespace KonjoBot.Objects.Bot
{
    public class ScriptObjects
    {
        Util.Timer m_timer;
        public ScriptObjects() {
            m_timer = new Util.Timer(500, true);
            m_timer.Execute += m_timer_Execute;
        }

        void m_timer_Execute()
        {
            try
            {

           
            foreach (Script s in Core.Global.ScriptList)
            {
                if(s.ShouldRun)
                {
                    Core.PreformScript(s.ScriptCode, false);
                }
            }
            }
            catch (Exception)
            {

            }
        }
        public interface IScript
        {
            void Run(Client core);
         
        }
        public object FindInterface(System.Reflection.Assembly DLL, string InterfaceName)
        {
            //Loop through types looking for one that implements the given interface
            foreach (Type t in DLL.GetTypes())
            {
                if ((t.GetInterface(InterfaceName, true) != null))
                {
                    return DLL.CreateInstance(t.FullName);
                }
            }
            return null;
        }
        private Thread RunningThread;
        public static Dictionary<string, IScript> CompiledScripts = new Dictionary<string, IScript>();
        public void DisposeScript(string code)
        {
            if (CompiledScripts.ContainsKey(code))
            {
                IScript script = CompiledScripts[code];
             //   script.Dispose();

               
            }
        }
        public void ClearComplipedScripts()
        {
            CompiledScripts.Clear();
        }
        public CompilerResults ExeCuteCode(string Source, Client client)
        {
            CompilerResults Results = null;

   
            try
            {
                if (CompiledScripts.ContainsKey(Source))
                {
                    
               /*   Thread t = new Thread(() => RunScript(CompiledScripts[Source]));
                  t.Start();*/
                    RunScript(CompiledScripts[Source]);
                

                }
                else
                {
                    CodeDomProvider P = default(CodeDomProvider);
                    P = new Microsoft.VisualBasic.VBCodeProvider();
                    CompilerParameters CP = new CompilerParameters();
              
                    CP.GenerateExecutable = false;
                    CP.GenerateInMemory = true;
                    CP.IncludeDebugInformation = false;
                    //CP.CompilerOptions = "/optimize"
                    CP.TreatWarningsAsErrors = false;

                    CP.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
                    CP.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                    CP.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll");
                    CP.ReferencedAssemblies.Add("System.Xml.dll");
                    CP.ReferencedAssemblies.Add("System.Drawing.dll");
                    CP.ReferencedAssemblies.Add("System.dll");
                  
                  //  CP.ReferencedAssemblies.Add(System.Application.StartupPath + "\\\\TibiaAPI.dll");
                    Results = P.CompileAssemblyFromSource(CP, Source);

                    IScript script = (IScript)FindInterface(Results.CompiledAssembly, "IScript");
                    CompiledScripts.Add(Source, script);
                  
                 /*   Thread t = new Thread(() => RunScript(script));
                    t.Start();
                  */
                    RunScript(script);
                                     
                   

                }
                return null;
            }
            catch (Exception ex)
            {
                      return Results;
            }

        }
        private void RunScript(IScript script)
        {
            try
            {
                script.Run(Core.client);
            }
            catch (Exception ex)
            {
                Core.client.StatusBar = ex.Message;
            }
        }
        public void UpdateGlobals()
        {

        }
            public string GenCode1(string code = "")
        {
            StringBuilder strB = new StringBuilder();
            strB.Append("Imports System" + System.Environment.NewLine);
            strB.Append("Imports Microsoft.VisualBasic" + System.Environment.NewLine);
            strB.Append("Imports System.Windows.Forms" + System.Environment.NewLine);
            strB.Append("Imports System.Collections.Generic" + System.Environment.NewLine);
            strB.Append("Imports System.Threading" + System.Environment.NewLine);

            strB.Append("Imports KonjoBot" + System.Environment.NewLine);
            strB.Append("Imports KonjoBot.Objects" + System.Environment.NewLine);
            strB.Append("Imports KonjoBot.Objects.Bot.ScriptObjects" + System.Environment.NewLine);
        
            strB.Append(System.Environment.NewLine);

            strB.Append("Public Class Script" + System.Environment.NewLine);
            strB.Append("  Implements IScript" + System.Environment.NewLine);
            strB.Append("  Dim WithEvents Client As Client" + System.Environment.NewLine);
            strB.Append("  Dim IsRunning As Boolean = True" + System.Environment.NewLine);

            strB.Append(System.Environment.NewLine);

            strB.Append("  Public Sub Run(ByVal _client As Client,ByVal _KeepRunning As Boolean) Implements IScript.Run" + System.Environment.NewLine);
            strB.Append("      Me.Client = _client" + System.Environment.NewLine);
            strB.Append("      Me.IsRunning = _KeepRunning" + System.Environment.NewLine);
            strB.Append("    While(IsRunning)" + System.Environment.NewLine);
            strB.Append("      'Your Code here" + System.Environment.NewLine);
            strB.Append("      Wait(500)" + System.Environment.NewLine);
            strB.Append("    End While" + System.Environment.NewLine);
            strB.Append("      " + code + System.Environment.NewLine);
            strB.Append(" End Sub" + System.Environment.NewLine);
            strB.Append(System.Environment.NewLine);

            strB.Append(System.Environment.NewLine);

            strB.Append(System.Environment.NewLine);
            strB.Append("End Class" + System.Environment.NewLine);
            return strB.ToString();
        }
        public string GenCode(string code,bool isScriptForm)
        {
            StringBuilder strB = new StringBuilder();
            strB.Append("Imports System" + System.Environment.NewLine);
            strB.Append("Imports Microsoft.VisualBasic" + System.Environment.NewLine);
            strB.Append("Imports System.Windows.Forms" + System.Environment.NewLine);
            strB.Append("Imports System.Collections.Generic" + System.Environment.NewLine);
            strB.Append("Imports System.Threading" + System.Environment.NewLine);

            strB.Append("Imports KonjoBot" + System.Environment.NewLine);
            strB.Append("Imports KonjoBot.Objects" + System.Environment.NewLine);
            strB.Append("Imports KonjoBot.Objects.Bot.ScriptObjects" + System.Environment.NewLine);

            strB.Append(System.Environment.NewLine);

            strB.Append("Public Class Script" + System.Environment.NewLine);
            strB.Append("  Implements IScript" + System.Environment.NewLine);
            strB.Append("  Dim WithEvents Client As Client" + System.Environment.NewLine);
            strB.Append(Core.Global.GlobalVariables);
            strB.Append(System.Environment.NewLine);

            strB.Append("  Public Sub Run(ByVal _client As Client) Implements IScript.Run" + System.Environment.NewLine);
            strB.Append("      Me.Client = _client" + System.Environment.NewLine);
            strB.Append("      Main()" + System.Environment.NewLine);
            strB.Append("  End Sub" + System.Environment.NewLine);
            strB.Append(System.Environment.NewLine);
            if (!isScriptForm)
            {        

            strB.Append("  Public Sub Main()" + System.Environment.NewLine);
            strB.Append("    'Your Code here" + System.Environment.NewLine);
            strB.Append("      " + code + System.Environment.NewLine);
            strB.Append("  End Sub" + System.Environment.NewLine);
            }
            else
            {
                strB.Append("      " + code + System.Environment.NewLine);
            }
            strB.Append(System.Environment.NewLine);
            strB.Append("End Class" + System.Environment.NewLine);
            return strB.ToString();
        }
    
        public static void NextWaypoint()
        {
           
            if (Core.WaypointLine == Core.Global.Waypoints.Count - 1)
            {
                Core.WaypointLine = 0;
            }
            else
            {
                Core.WaypointLine += 1;
            }
        }
        public static void Say(string val)
        {
            Packets.OutGoing.Speech.SendConsole(Core.client, val);
        }
        public static void NpcSay(string val)
        {
            Packets.OutGoing.Speech.SendToNpc(Core.client, val);
        }
        public static void Wait(int time)
        {
            System.Threading.Thread.Sleep(time);
        }
        public static int GoldCount()
        {
            return ItemCount(3031);
        }
        public static bool Invisible()
        {
          if(Core.client.Memory.ReadByte(Core.client.Player.Address + 204) == 0)
          {
              return true;
          }
          return false;
        }
        public static void UseOnTarget(int ItemID)
        {
            int CreatureId = Core.client.Player.RedSquare;
            if(CreatureId > 0)
            {
                Packets.OutGoing.ItemUseOnCreature.Send(Core.client, ItemLocation.FromHotkey().ToLocation(), (ushort)ItemID, 1, (uint)CreatureId);
            }
          
        }
        public static bool Walk(int x,int y,int z)
        {
            Location l = new Location(x, y, z);
            Core.client.MiniMap.Goto(l);
            IEnumerable<Objects.MiniMap.MyPathNode> path = Core.client.MiniMap.GetPath(l);
            if (path != null)
            {
                DateTime d = DateTime.Now.AddSeconds(5);
                Core.client.MiniMap.ProcessDirections(path, l, 0);
                while (Core.client.MiniMap.IsWalking)
                {
                    if (DateTime.Now >= d)
                    {
                        Print("Walk Failed " + l.ToString());
                        break;
                    }
                    Thread.Sleep(10);
                }
                if(Core.client.PlayerLocation == l)
                {
                    return true;
                }
            }          
            return false;
        }
        public static void Reach(string creatureName)
        {
            DateTime d = DateTime.Now.AddSeconds(5);
            foreach (Creature c in Core.client.Battlelist.GetCreatures())
            {
                if(c.Name.ToLower() == creatureName.ToLower())
                {
                    Core.client.MiniMap.Goto(c.Location, 1);
                     while(!Core.client.PlayerLocation.IsAdjacentTo(c.Location))
                     {
                         if(DateTime.Now >= d)
                         {
                             Print("did not find the creature");
                             break;
                         }
                         Thread.Sleep(10);
                     }
                    break;
                }
            }
           
        }
        public static int ItemCount(int itemID)
        {
            int count = 0;
            foreach (Container c in Core.client.Inventory.GetContainers())
            {
                foreach (Item i in c.GetItems())
                {
                    if (i.Id == itemID)
                    {
                        count += i.Count;
                    }
                }
            }
            return count;
        }
        public static void Print(string message)
        {
            Packets.NetworkMessage msg = new Packets.NetworkMessage();
            msg.Position = 0;
            msg.AddByte(0xB4);
            msg.AddByte(0x16);
            msg.AddString(message);
            Core.client.HookProxy.SendPacketToClient(msg.Data);

        }
        public static bool DashWest()
        {
        
            Packets.OutGoing.Walk.Send(Core.client, Constants.WalkDirection.WalkWest);
            return true;
        }
        public static bool DashNorth()
        {
            Packets.OutGoing.Walk.Send(Core.client, Constants.WalkDirection.WalkNorth);
            return true;
        }
        public static bool DashSouth()
        {
            Packets.OutGoing.Walk.Send(Core.client, Constants.WalkDirection.WalkSouth);
            return true;
        }
        public static bool DashEast()
        {
            Packets.OutGoing.Walk.Send(Core.client, Constants.WalkDirection.WalkEast);
            return true;
        }

        public static bool DashNorthWest()
        {
            Packets.OutGoing.Walk.Send(Core.client, Constants.WalkDirection.WalkNorthWest);
            return true;
        }
        public static bool DashNorthEast()
        {
            Packets.OutGoing.Walk.Send(Core.client, Constants.WalkDirection.WalkNorthEast);
            return true;
        }
        public static bool DashSouthWest()
        {
            Packets.OutGoing.Walk.Send(Core.client, Constants.WalkDirection.WalkSouthWest);
            return true;
        }
        public static bool DashSouthEast()
        {
            Packets.OutGoing.Walk.Send(Core.client, Constants.WalkDirection.WalkSouthEast);
            return true;
        }
        public static void Deposit(int BoxIndex)
        {
            Tile t = ReachDepot();
            Container Container= OpenDepot(t,BoxIndex);
            AddItemsToDepot(Container);

        }
        private static Container OpenDepot(Tile t,int boxIndex)
        {
            List<int> DepotList = new List<int> { 3497, 3498, 3499, 3500 };
            int ret = 0 ;
            foreach (Item i in t.Items)
            {
                if (DepotList.Contains(i.Id))
                {
                    int index = Core.client.Inventory.ContainersCount();
                    ret = index;
                    i.Use((byte)index);
                    DateTime d = DateTime.Now.AddSeconds(5);
                    while (Core.client.Inventory.ContainersCount() == index)
                    {
                        if (d <= DateTime.Now)
                        {
                            Print("Could not open depot :'(");
                            return null;
                        }
                    }
                    Core.SleepRandom();
                    Core.SleepRandom();
                    foreach (Item j in Core.client.Inventory.GetContainer(index).GetItems())
                    {                        
                        j.Use((byte)index);
                        break;
                    }
                    Core.SleepRandom();
                    Core.SleepRandom();               
                    int boxVal = 1;
                    foreach (Item j in Core.client.Inventory.GetContainer(index).GetItems())
                    {
                        if (boxVal == boxIndex)
                        {
                            j.Use((byte)index);
                        }
                        boxVal += 1;
                    }                   
                }           
                
            }
            Core.SleepRandom();
            Core.SleepRandom();     
            return Core.client.Inventory.GetContainer(ret);
        }
        public static Tile ReachDepot()
        {
            foreach (Tile t in Core.client.Map.GetTilesSameFloor().OrderBy(k => k.Location.DistanceTo(Core.client.PlayerLocation)).ToList())
            {
                if (HaveDepot(t))
                {
                    IEnumerable<Objects.MiniMap.MyPathNode> path = Core.client.MiniMap.GetPath(t.Location);
                    if (path != null)
                    {
                        DateTime d = DateTime.Now.AddSeconds(5);
                        Core.client.MiniMap.ProcessDirections(path, t.Location);
                        while (Core.client.PlayerLocation.IsAdjacentTo(t.Location) == false)
                        {
                            if (d <= DateTime.Now)
                            {
                                Print("Did not find any depot :'(");
                                return null;
                            }
                        }
                        return t;
                    }
                    else { continue; }
                }

            }
            return null;
        }
        public static void BuyItem(int ItemId,byte Count)
        {
            Packets.OutGoing.ShopBuy.Send(Core.client, ItemId, Count, true, false);
        }
        private static bool HaveDepot(Tile t)
        {
            List<int> DepotList = new List<int> { 3497, 3498, 3499, 3500 };
            foreach (Item i in t.Items)
            {
                if (DepotList.Contains(i.Id)) { return true; }
            }
            return false;
        }
        private static void AddItemsToDepot(Container TCont)
        {
            foreach (Container c in Core.client.Inventory.GetContainers())
            {
                List<Item> Items = c.GetItems().ToList();
                Items.Reverse();
                if(c.Number != TCont.Number)
                {
                    foreach (Item i in Items)
                    {                      
                        foreach (LootItem l in Core.Global.LootList)
                        {
                            if(l.Id == i.Id && l.DropInDepot)
                            {
                                i.Move(ItemLocation.FromContainer((byte)TCont.Number, (byte)(TCont.Ammount + 1)));
                                Core.SleepRandom();
                            }
                        }
                        
                     
                    }
                }                
            }
            Core.SleepRandom();
            TCont.Close();
        }
        public static void WaitRandom()
        {
            Core.SleepRandom();
        }
        public static void Wait()
        {
            Core.SleepRandom();
        }
        public static bool Fish(bool CountFish = false)
        {
            List<Tile> FishTiles = Core.client.Map.GetFishTiles().ToList();
            Tile myTile = null;
            int Count = 0;
            foreach (Tile t in FishTiles)
            {
                if (t.Location.DistanceTo(Core.client.PlayerLocation) <= 8)
                {
                    if (t.Location.IsShootable(Core.client, Core.client.PlayerLocation))
                    {                   
                        if(CountFish)
                        {
                            if (myTile == null) { myTile = t; }
                            Count += 1;
                        }
                        else
                        {
                            Core.client.Inventory.UseItemOnTile(3483, t);
                            return true;
                        }
                    }
                }
            }
            if(Count == 0)
            {
                return false;
            }
            if(CountFish)
            {
                Print (Count.ToString()+ " fish left.");
                Core.client.Inventory.UseItemOnTile(3483, myTile);
                return true;
            }
            return false;        
        }
        public static void EatFood()
        {
            if(Core.client.Hunger <180 )//less than 3 min
            {
                foreach (Item i in Core.client.Inventory.GetContainerItems())
                {
                    if(Core.Looter.IsFood(i.Id))
                    {
                        i.Use();
                        Thread.Sleep(1000);
                        return;
                    }
                }
            }
        }
        public static void OpenMainBp()
        {
            Item Item = Core.client.Inventory.GetItemInSlot(Constants.SlotNumber.Backpack );
            if(Item != null)
            {
                if(Item.Id > 0)
                {
                    Item.Use((byte)Core.client.Inventory.ContainersCount());
                }
            }
        }
        public static void CloseWindows()
        {
            int count = Core.client.Inventory.ContainersCount() -1;
            for (int i = count; i >= 0; i-=1)
            {
                Core.client.Inventory.GetContainer(i).Close();
                WaitRandom();
            }
        }
        public static void OpenBps()
        {
            Retry:

            OpenMainBp();
            Wait();
            Container c = Core.client.Inventory.GetContainer(0);
            if(c == null)
            {
                goto Retry;
            }
            foreach (Item item in c.GetItems())
            {
                if (item.ItemData.IsContainer)
                {
                    item.Use((byte)Core.client.Inventory.ContainersCount());
                    Wait();
                }
            }
        }
        public static void ResetBps()
        {
            CloseWindows();
            OpenBps();
        }
        public static void GoDown()
        {
            Core.client.FloorChanger.GoDown();
        }
        public static void GoUp()
        {
            Core.client.FloorChanger.GoUp();
        }
        public static string GotoLabel
        {
            set
            {
                for (int i = 0; i <= Core.Global.Waypoints.Count - 1; i++)
                {
                    Waypoint w = Core.Global.Waypoints[i];
                    if (w.Comment.ToLower() == value.ToLower())
                    {                     
                        Core.WaypointLine = i;
                        return;
                    }
                }
            }
        }
        public bool LooterEnabled
        {
            get { return Core.Global.LooterEnabled; }
            set { Core.Global.LooterEnabled = value; }
        }
        public bool WalkerEnabled
        {
            get { return Core.Global.WalkerEnabled; }
            set { Core.Global.WalkerEnabled = value; }
        }
        public bool AttackerEnabled
        {
            get { return Core.Global.AttackerEnabled; }
            set { Core.Global.AttackerEnabled = value; }
        }

    }
}
