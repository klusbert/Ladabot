using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
namespace KonjoBot.Objects
{
  public partial class Client
    {
        public Adresses.Addresses Addresses;
        public Process Process;
        public IntPtr Handle;
        public IntPtr MainWindowHandle;
        private MemoryHelper m_memory;
        private Inventory m_inventory;
        private WindowHelper m_windowhelper;
        private InputHelper m_input;
        private MiniMap m_minimap;
        private Battlelist m_battlelist;
        private DatReader m_datreader;
        private Map m_map;
        private Util.AStarPathFinder m_pathfinder;

        private Bot.Settnings m_settnings;
        private FormHelper m_formhelper;
        private FloorChanger m_floorChange;
        private Player m_player;
        private Thread LooginChecker;
        private bool m_loggedIn;        
        public bool IsPreperd;

        public Packets.Parser Parser;
  

        public Packets.HookProxy HookProxy;

        public static uint McAddress = 0x5AF8F7;
         public Client(Process pr)
        {
            try
            {
                    

        
            Process = pr;
           Handle = Util.WinApi.OpenProcess(Util.WinApi.PROCESS_ALL_ACCESS, 0, (uint)pr.Id);
            Process.EnableRaisingEvents = true;
            Process.Exited += (process_Exited);
            MainWindowHandle = pr.MainWindowHandle;
            Addresses = new Adresses.Addresses(this);
            m_memory = new MemoryHelper(this);
            m_battlelist = new Battlelist(this);
          

            LooginChecker = new Thread(new ThreadStart(CheckForLogin));
            LooginChecker.Start();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }
        }
        public void PrepereClient()
        {
            m_formhelper = new FormHelper(this);
            m_inventory = new Inventory(this);
            m_windowhelper = new WindowHelper(this);
            m_input = new InputHelper(this);
            m_datreader = new DatReader(this);
            m_minimap = new MiniMap(this);
            m_floorChange = new FloorChanger(this);
        //    m_hookproxy = new Packets.HookProxy(this);

            Parser = new Packets.Parser(this);
            HookProxy = new Packets.HookProxy(this);

            m_map = new Map(this);
            m_pathfinder = new Util.AStarPathFinder(this);      
            m_settnings = new Bot.Settnings();        
           
        // m_map.FullLightOn();
            IsPreperd = true;
        }
        public string StatusBar
        {
            set
            {
                Memory.WriteString(Addresses.Client.StatusbarText, value);
                Memory.WriteByte(Addresses.Client.StatusbarTime, 40);
            }
        }    
     public DatReader DatReader
        {
            get { return m_datreader; }
        }
        public Player Player
        {
            get { 

                if(m_player == null)
                {
                    m_player = Battlelist.GetPlayer();
                }
                return m_player; 
            }

        }
        public FloorChanger FloorChanger
        {
            get { return m_floorChange; }
        }
        public FormHelper Forms
        {
            get { return m_formhelper; }
        }

        public Bot.Settnings Settnings
        {
            get { return m_settnings; }
        }
            

        public Util.AStarPathFinder PathFinder
        {
            get { return m_pathfinder; }
        }
        public Map Map
        {
            get { return m_map; }
        }
        
        public Battlelist Battlelist
        {
            get { return m_battlelist; }
        }
        public MiniMap MiniMap
        {
            get { return m_minimap; }
        }
        public InputHelper Input
        {
            get { return m_input; }
        }
        public MemoryHelper Memory
        {
            get { return m_memory; }
        }
        public WindowHelper Window
        {
            get { return m_windowhelper; }
        }
        public Inventory Inventory
        {
            get { return m_inventory; }
        }
        public Location PlayerLocation
        {
            get
            {

                return new Location(Memory.ReadInt32(Addresses.Player.X), Memory.ReadInt32(Addresses.Player.Y), Memory.ReadInt32(Addresses.Player.Z));
            }
        }

      public int Ping
        {
            get
            {
                return Memory.ReadInt32(Addresses.Client.Ping);
            }
        }
     
        public Constants.LoginStatus Status
        {
            get { return (Constants.LoginStatus)Memory.ReadByte(Addresses.Client.Status); }
        }
        public bool LoggedIn
        {
            get { return m_loggedIn; }
        }
        public uint[] XteaKey()
        {

            return this.Memory.ReadBytes(Addresses.Packet.XteaKey, 16).ToUInt32Array();
        }
        public override string ToString()
        {
         
            if (this.LoggedIn)
            {
                return Player.Name;
            }
            else {
                return "NotLoggedIn";
            }
        }
        public static List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            Client client = null;
            foreach (Process process in Process.GetProcesses())
            {
                StringBuilder classname = new StringBuilder();
                Util.WinApi.GetClassName(process.MainWindowHandle, classname, 12);

                if (classname.ToString().Equals("TibiaClient", StringComparison.CurrentCultureIgnoreCase) || classname.ToString().Equals("ÙbiaClient",StringComparison.CurrentCultureIgnoreCase))
                {
                    client = new Client(process);
                    clients.Add(client);
                }
               
               
            }
           /*
            foreach (Process p in Process.GetProcessesByName("tibia"))
        	{
                StringBuilder classname = new StringBuilder();
                Util.WinApi.GetClassName(p.MainWindowHandle, classname, 12);
                System.Windows.Forms.MessageBox.Show(classname.ToString());
                System.IO.File.WriteAllText(@"C:\test.txt", classname.ToString());
		        client = new Client(p);
                clients.Add(client);
	        }
        */


            return clients;
        }
      public static Client OpenMc()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Tibia\tibia.exe");
            if (System.IO.File.Exists(path))
            {
                return OpenMc(path);
            }
            else
            {
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.Filter =
                   "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                dialog.Title = "Select a Tibia client executable";
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return OpenMc(dialog.FileName);
                }
            }
            return null;
           
        }
     public static Client OpenMc(string path)
        {

            KonjoBot.Util.WinApi.PROCESS_INFORMATION pi = new KonjoBot.Util.WinApi.PROCESS_INFORMATION();
            KonjoBot.Util.WinApi.STARTUPINFO si = new KonjoBot.Util.WinApi.STARTUPINFO();
       
            string arguments = "";

            KonjoBot.Util.WinApi.CreateProcess(path, " " + arguments, IntPtr.Zero, IntPtr.Zero,
                false, KonjoBot.Util.WinApi.CREATE_SUSPENDED, IntPtr.Zero,
                System.IO.Path.GetDirectoryName(path), ref si, out pi);
            IntPtr handle = KonjoBot.Util.WinApi.OpenProcess(KonjoBot.Util.WinApi.PROCESS_ALL_ACCESS, 0, pi.dwProcessId);
            Process p = Process.GetProcessById(Convert.ToInt32(pi.dwProcessId));

            Util.Memory.WriteByte(p.Handle, McAddress, 0xEB);
            KonjoBot.Util.WinApi.ResumeThread(pi.hThread);
            p.WaitForInputIdle();
            Util.Memory.WriteByte(p.Handle, McAddress, 0x75);
            KonjoBot.Util.WinApi.CloseHandle(handle);
            KonjoBot.Util.WinApi.CloseHandle(pi.hProcess);
            KonjoBot.Util.WinApi.CloseHandle(pi.hThread);
            return new Client(p);
        }
      #region LoginChecker
        private void CheckForLogin()
        {
            while (Status != Constants.LoginStatus.LoggedIn)
            {
              
                Thread.Sleep(100);
            }
            m_player = Battlelist.GetPlayer();
            m_loggedIn = true;
       
            LooginChecker = new Thread(new ThreadStart(CheckForLogOut));
            LooginChecker.Start();
        }
        private void CheckForLogOut()
        {
            while (Status == Constants.LoginStatus.LoggedIn)
            {
                Thread.Sleep(100);
            }
            m_player = null;
            m_loggedIn = false;
            LooginChecker = new Thread(new ThreadStart(CheckForLogin));
            LooginChecker.Start();

        }
        public void Abort()
        {
            LooginChecker.Abort();
        }
      #endregion

        private void process_Exited(object sender, EventArgs e)
        {

          
            LooginChecker.Abort();
            if(Core.client !=null)
            {
                Core.client.Abort();
            }
         
            Environment.Exit(0);
        }
    }
}
