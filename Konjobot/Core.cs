using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
using System.CodeDom.Compiler;
using System.Threading;
using KonjoBot.Packets;


namespace KonjoBot
{
    public static class Core
    {
        public static Client client;
        public static WorkClasses.Looter Looter;
        public static WorkClasses.Walker Walker;
        public static WorkClasses.Attacker Attacker;
        public static WorkClasses.Healer Healer;
        public static Objects.Bot.ScriptObjects ScriptObjects;
        public static DateTime WaitForLoot;
        public static bool isLoaded = false;

        public static byte LastCorpse;
        public static Location LastCorpseLocation;
        public static int WaypointLine;
        public static Global Global;
        public static Forms.Cavebot CavebotForm;
        public static Forms.Healingform1 HealingForm;
        public static Forms.Mapviewer MiniMapForm;
       
        public static Forms.PacketScanner PacketScannerFrm;
        public static Forms.ScriptForm ScriptForm;
        public static Forms.Alarms Alarms;
        public static Forms.Hotkeys HotkeysForm;
        public static MainForm MainForm;
        public static int MinThreadWait = 500;
        public static int MaxThreadWait = 900;
        private static Thread SpeechThread;
        public static void InitilizeCore(Client m_client,MainForm m_mainForm)
        {
            client = m_client;
           
            Global = new Global();
            client.PrepereClient();
            Global.Waypoints = new System.ComponentModel.BindingList<Objects.Bot.Waypoint>();
            Global.LootList = new System.ComponentModel.BindingList<Objects.Bot.LootItem>();
            Global.TargetList = new System.ComponentModel.BindingList<Objects.Bot.Target>();
            Global.ScriptList = new System.ComponentModel.BindingList<Objects.Bot.Script>();
            Global.HealingRules = new System.ComponentModel.BindingList<Objects.Bot.HealingRule>();
            CavebotForm = new Forms.Cavebot();
            HealingForm = new Forms.Healingform1();
            PacketScannerFrm = new Forms.PacketScanner();
            MiniMapForm = new Forms.Mapviewer();
            ScriptForm = new Forms.ScriptForm();
            Alarms = new Forms.Alarms();
            HotkeysForm = new Forms.Hotkeys();
            MainForm = m_mainForm;
            Walker = new WorkClasses.Walker();
            Looter = new WorkClasses.Looter();
            Attacker = new WorkClasses.Attacker();
            Healer = new WorkClasses.Healer();
            ScriptObjects = new Objects.Bot.ScriptObjects();
            WaitForLoot = DateTime.Now;
            Core.client.HookProxy.ReceivedTextMessageIncomingPacket += Core.HookProxy_ReceivedTextMessageIncomingPacket;


        }

        private static bool HookProxy_ReceivedTextMessageIncomingPacket(IncomingPacket packet)
        {
            Packets.Incoming.TextMessagePacket textMessagePacket = (Packets.Incoming.TextMessagePacket)packet;
            if (textMessagePacket.Mode == 22 && textMessagePacket.Text.ToLower().Contains("you see"))
            {

                textMessagePacket.Text +="\n  [ID] = " + Core.client.Memory.ReadInt32((long)((ulong)Core.client.Addresses.Client.LastSeendId)).ToString();
            }
            if (textMessagePacket.Mode == 24 && textMessagePacket.PhysicalDmgColor == 30 && textMessagePacket.MagicDmgColor == 113)
            {
                Core.client.StatusBar = "you lose " + textMessagePacket.PhysicalDmgValue.ToString() + " poison";
            }
            return true;
        }
        public static void Print(string message)
        {
            NetworkMessage networkMessage = new NetworkMessage(false);
            networkMessage.Position = 0;
            networkMessage.AddByte(180);
            networkMessage.AddByte(22);
            networkMessage.AddString(message);
            Core.client.HookProxy.SendPacketToClient(networkMessage.Data);
        }
        public static bool IsLoggedIn
        {
            get { return client.LoggedIn; }
        }
        public static int GetRandomInt(int min,int Max)
        {  
            Random rnd = new Random();
            return rnd.Next(min, Max);
        }
        public static void SleepRandom()
        {
           
              System.Threading.Thread.Sleep(GetRandomInt(MinThreadWait,MaxThreadWait));
        }
        public static void StartAlarm(string str)
        {
            if (SpeechThread != null)
            {
                if (!SpeechThread.IsAlive)
                {
                    SpeechThread = new Thread(() => Alarm(str));
                    SpeechThread.Start();
                }
            }
            else
            {
                SpeechThread = new Thread(() => Alarm(str));
                SpeechThread.Start();
            }
        }
        private static void Alarm(string text)
        {
            System.Speech.Synthesis.SpeechSynthesizer p_objSynth = new System.Speech.Synthesis.SpeechSynthesizer();
           
            p_objSynth.Speak(text);
        }
        public static void PreformScript(string _script, bool genCode = true, bool inThread = true)
        {

            try
            {
              
                CompilerResults res;

                if (genCode)
                {
                    res = ScriptObjects.ExeCuteCode(ScriptObjects.GenCode(_script,false), client);
                }
                else
                {
                  //  res = ScriptObjects.ExeCuteCode(_script, client, true);
                    res = ScriptObjects.ExeCuteCode(ScriptObjects.GenCode(_script, true), client);
                }

                if (res != null)
                {
                  
                    foreach (CompilerError o in res.Errors)
                    {
                        string str = "Line " + o.Line.ToString() + " Error: " + o.ErrorText;
                        System.Windows.Forms.MessageBox.Show(str);
                        /*
                      if (ScriptForm.ListBox2.InvokeRequired)
                      {
                          ScriptForm.ListBox2.Invoke(() => ScriptForm.ListBox2.Items.Add(str));
                      }
                      else
                      {
                          ScriptForm.ListBox2.Items.Add(str);

                      }
                         */

                    }
                     
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Scripter " +ex.ToString());
            }

        }

    }
}
