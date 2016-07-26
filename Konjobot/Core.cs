using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
using System.CodeDom.Compiler;
using System.Threading;

namespace KonjoBot
{
    public static class Core
    {
        public static Client client;
        public static WorkClasses.Looter Looter;
        public static WorkClasses.Walker Walker;
        public static WorkClasses.Attacker Attacker;
        public static Objects.Bot.ScriptObjects ScriptObjects;
        public static DateTime WaitForLoot;

        public static byte LastCorpse;
        public static Location LastCorpseLocation;
        public static int WaypointLine;
        public static Global Global;
        public static Forms.Cavebot CavebotForm;
        public static Forms.HealingForm HealingForm;
        public static Forms.PacketScanner PacketScannerFrm;
        public static Forms.ScriptForm ScriptForm;
        public static Forms.Alarms Alarms;
        public static MainForm MainForm;
        public static int MinThreadWait = 500;
        public static int MaxThreadWait = 900;
        private static Thread SpeechThread;
        public static void InitilizeCore(Client m_client,MainForm m_mainForm)
        {
            client = m_client;
            client.PrepereClient();
            Global = new Global();
            Global.Waypoints = new System.ComponentModel.BindingList<Objects.Bot.Waypoint>();
            Global.LootList = new System.ComponentModel.BindingList<Objects.Bot.LootItem>();
            Global.TargetList = new System.ComponentModel.BindingList<Objects.Bot.Target>();
            Global.ScriptList = new System.ComponentModel.BindingList<Objects.Bot.Script>();
            CavebotForm = new Forms.Cavebot();
            HealingForm = new Forms.HealingForm();
            PacketScannerFrm = new Forms.PacketScanner();
            ScriptForm = new Forms.ScriptForm();
            Alarms = new Forms.Alarms();
            MainForm = m_mainForm;
            Walker = new WorkClasses.Walker();
            Looter = new WorkClasses.Looter();
            Attacker = new WorkClasses.Attacker();
            ScriptObjects = new Objects.Bot.ScriptObjects();
            WaitForLoot = DateTime.Now;
   
         
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
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

        }

    }
}
