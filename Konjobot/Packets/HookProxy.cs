using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.Threading;

using KonjoBot.Util;
using KonjoBot.Objects;
using KonjoBot.Constants;

namespace KonjoBot.Packets
{
    public class HookProxy
    {
        #region events
        public delegate void PacketListner(byte[] data);
        public delegate void TileAdd(Location location, byte stack, int itemid);

        public delegate void ItemUse(Location loc, byte stack, int itemId, byte index);
        public delegate void ContainerOpen(string name, byte number, int itemid);
        public delegate void GreenMessage(string Message);
        public delegate void CreatureSpeech(string Name, int level, string message);
        public delegate void CreatureMove(Location fromLocation, byte stack, Location toLocation);
        public event GreenMessage GreenConsoleMessage;
        public event CreatureSpeech CreatureSpeechPacket;

        public event ContainerOpen ContainerOpenPacket;
        public event PacketListner IncommingPacket;
        public event PacketListner OutgoingPacket;

        public event TileAdd TileAddThing;
        public event ItemUse ItemUsePacket;
        public event CreatureMove CreateMovePacket;
        #endregion

        public Client client;
        private NamedPipeServerStream pipeRecv;
        private NamedPipeClientStream pipeSend;

        byte[] buffer = new byte[1024 * 1024];
        private bool firstTime = true;
        private object LockEr = new object();
        private TibiaSock tibiaSock;
        public HookProxy(Client cl)
        {
  string dllPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath.ToString(), "Hooker.dll");
           // string dllPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath.ToString(), "InjectBooter.dll");
        
            client = cl;

            string name = "InjectClient" + Convert.ToString(client.Process.Id);
            pipeRecv = new NamedPipeServerStream(name, PipeDirection.InOut, -1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            pipeRecv.BeginWaitForConnection(BeginWaitForConnection, pipeRecv);
            bool injected = Inject(dllPath);
            SetUpPipes();
            tibiaSock = new TibiaSock(cl);
        }

        void HookProxy_OutgoingPacket(byte[] data)
        {
            NetworkMessage msg = new NetworkMessage(client, data);
           
            if (OutgoingPacket != null)
            {
                OutgoingPacket.Invoke(data);
              
            }
            byte Type = msg.GetByte();

            switch (Type)
            {
                case 0x82:
                    Location loc = msg.GetLocation();
                    uint itemId = msg.GetUInt16();
                    byte stackPos = msg.GetByte();
                    byte Index = msg.GetByte();
                    if (ItemUsePacket != null)
                    {
                        ItemUsePacket.Invoke(loc, stackPos, (int)itemId, Index);
                    }

                    break;
                case 0x96:
                    byte type = msg.GetByte();
                    string message = msg.PeekString();
                    msg.AddString(message);
                    break;
                case 101:
                case 102:
                case 103:
                case 104:
                case 106:
                case 107:
                case 108:
                case 109:
                    client.Map.PlayerHavedMoved = true;
                   // client.StatusBar = "moved " + Type.ToString("X");
                    break;

            }



        }
        private void SetUpPipes()
        {
            string name = "InjectServer" + Convert.ToString(client.Process.Id);

            pipeSend = new NamedPipeClientStream(".", name, PipeDirection.InOut);

            pipeSend.Connect();
               SetAddresses();
                    EnableHook();

        }
        private void BeginWaitForConnection(IAsyncResult ar)
        {

            NamedPipeServerStream pipe = ar.AsyncState as NamedPipeServerStream;
            pipe.EndWaitForConnection(ar);

            if (pipe.IsConnected)
            {
                if (firstTime)
                {
                    // if this is the first time we sets our addresses and enable the hooks.
                 
                    firstTime = false;
                }

                pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), pipe);
            }
        }

        private void BeginRead(IAsyncResult ar)
        {
            NamedPipeServerStream pipe = ar.AsyncState as NamedPipeServerStream;
            int length = pipe.EndRead(ar);
            try
            {
                if (length > 1)
                {
                    byte[] tmpBytes = new byte[length];
                    Array.Copy(buffer, 0, tmpBytes, 0, length);
                    NetworkMessage msg = new NetworkMessage(tmpBytes);

                    msg.Position = 0;
                    UInt16 packLen = msg.GetUInt16();
                    PipePacketType type = (PipePacketType)msg.GetByte();


                    switch (type)
                    {

                        case PipePacketType.RecivedOutgoingPacket:                  
                                byte[] buf = new byte[msg.Data.Length - 3];
                                Array.Copy(msg.Data, 3, buf, 0, buf.Length);
                                HookProxy_OutgoingPacket(buf);
                            break;            
                        case PipePacketType.RecivedIncommingPacket:
             
                          
                             byte[] buf2 = new byte[msg.Data.Length - 3];
                                Array.Copy(msg.Data, 3, buf2, 0, buf2.Length);
                       
                            if(IncommingPacket != null)
                            {
                                IncommingPacket.Invoke(buf2);
                            }
                            break;
                        case PipePacketType.ParsedPacket:                           
                            ParseIncomingPacket(msg);
                            break;
                        case PipePacketType.ConnectionStatusChanged:
                            System.Windows.Forms.MessageBox.Show(msg.GetUInt32().ToString());
                            break;
                        case PipePacketType.FullIncommingPacket:
                           
                            if(IncommingPacket != null)
                            {
                                byte[] data = new byte[msg.Data.Length - 3];
                                Array.Copy(msg.Data, 3, data, 0, msg.Data.Length - 3);
                                IncommingPacket.Invoke(data);
                            }
                            break;
                        case  PipePacketType.test:
                            uint adr = msg.GetUInt32();
                            System.Windows.Forms.MessageBox.Show(adr.ToString("X"));
                            break;

                    }
                }
                pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), pipe);
            }
            catch (Exception ex)
            {

                pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), pipe);
            }
        }

        private void SplitPackets()
        {

        }
        private void ParseIncomingPacket(NetworkMessage msg)
        {
            int x, y, id;
            byte z, stack, number, Color;
            Location location;
            string message, name;
            byte PacketType = msg.GetByte();
            switch (PacketType)
            {
                case 0xF3:
                    
                    uint Len = msg.GetUInt32();
                    byte[] incommingData = new byte[Len];
                    for (int k = 0; k < Len; k++)
                    {
                        incommingData[k] = msg.GetByte();
                    }
                    if (IncommingPacket != null)
                    {
                        IncommingPacket.Invoke(incommingData);
                    }
                 break;
                case 0xB4:

                    Color = msg.GetByte();
                    message = msg.GetString();
                    if (message.ToLower().Contains("you see"))
                    {
                        message += "\n  [ID] = " + client.Memory.ReadInt32(client.Addresses.Client.LastSeendId).ToString();
                    }
                    ForwardTextPacket(Color, message);
                    break;
                case 0x6A:
                    x = msg.GetUInt16();
                    y = msg.GetUInt16();
                    z = msg.GetByte();
                    stack = msg.GetByte();
                    stack -= 1;
                    id = msg.GetUInt16();
                    Item i = new Item(client, id);
                 
                    if (TileAddThing != null)
                    {
                        TileAddThing.Invoke(new Location(x, y, z), stack, id);
                    }
                    break;
                case 0x6D:
                    Location FromLoc, ToLocation;
                    byte stackOrder;                
                    FromLoc = msg.GetLocation();
                    stackOrder = msg.GetByte();
                  
                    ToLocation = msg.GetLocation();
                  if(CreateMovePacket  !=null)
                  {
                      CreateMovePacket.Invoke(FromLoc, stackOrder, ToLocation);
                  }
                 break;
                case 0x84:
                 Location loc = msg.GetLocation();
                 byte color = msg.GetByte();
                message = msg.GetString();
                client.StatusBar = message + " " + color.ToString() + " " + loc.ToString();
                 break;


            }
        }
        private void ForwardTextPacket(byte colr, string text)
        {

            NetworkMessage pack = new NetworkMessage();
            pack.Position = 0;

            pack.AddByte(0xb4);
            pack.AddByte(colr);
            pack.AddString(text);
            SendPacketToClient(pack.Data);
        }
        public void SendPacketToClient(byte[] data)
        {


            NetworkMessage pack = new NetworkMessage();
            pack.Position = 0;

            pack.AddByte((byte)PipePacketType.SendPacketToClient);
            pack.AddUInt16((ushort)data.Length);
            pack.AddBytes(data);

            SendPipePacket(pack.Data);
        }
        public void SendPacketToServer(byte[] data)
        {
            lock (LockEr)
            {

            //    tibiaSock.SendPacketToServer(data);
          
            NetworkMessage msg = new NetworkMessage();
            msg.Position = 0;

            msg.AddByte((byte)PipePacketType.SendPacketToServer);
            msg.AddUInt16((ushort)data.Length);

            msg.AddBytes(data);
            SendPipePacket(msg.Data);
            
            }
        }

        private void SendToServer(byte[] data)
        {
            NetworkMessage pack = new NetworkMessage(client);
            pack.Position = 0;

            pack.AddByte((byte)PipePacketType.SendPacketToServer);
            pack.AddUInt16((ushort)data.Length);
            pack.AddBytes(data);

            SendPipePacket(pack.Data);

        }
        public void SendPipePacket(byte[] data)
        {
            pipeSend.Write(data, 0, data.Length);
        }
        private void SetAddresses()
        {
            //text
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetTextfunc, client.Addresses.TextDisplay.PrintTextFunction));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetShowFps, client.Addresses.TextDisplay.ShowFps));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetPrintFps, client.Addresses.TextDisplay.PrintFps));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetNopFps, client.Addresses.TextDisplay.NopFps));

            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetSendPointer, client.Addresses.Packet.SendPointer));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetRecvPointer, client.Addresses.Packet.RecivePointer));

            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetGetNextPacket, client.Addresses.Packet.GetNextPacketCall));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetReciveStream, client.Addresses.Packet.INCOMINGDATASTREAM));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetParserFunction, client.Addresses.Packet.PARSERFUNC));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetCreatePacket, client.Addresses.Packet.CreatePacket));

            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetSENDOUTGOINGPACKET, client.Addresses.Packet.SENDOUTGOINGPACKET));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetOutgoingDATASTREAM, client.Addresses.Packet.SendPacketData));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetOUTGOINGDATALEN, client.Addresses.Packet.SendPacketLenght));

            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SetAddByteFunc, client.Addresses.Packet.AddByteFunc));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.PeekMessage, client.Addresses.Client.PeekMessage));

            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.Connection, client.Addresses.Client.Status));
       



            /*  
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.WalkFunction, client.Addresses.Internal_Functions.WalkFunction));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.AttackFunfion, client.Addresses.Internal_Functions.AttackFunction));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.ItemMove, client.Addresses.Internal_Functions.ItemMoveFunction));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.ItemUseFunction, client.Addresses.Internal_Functions.ItemUseFuction));
            SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType1.SpeakFunction, client.Addresses.Internal_Functions.SpeakFunction));
              SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType.SetDatPointr , Adresses.Client.DatPointer ));
                SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType.SetOutgoingDATASTREAM, Adresses.Client.OutGotingDataStream));
                SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType.SetOUTGOINGDATALEN, Adresses.Client.OutGoingDataLenght));
                SendPipePacket(Pipe.SetAdressPipe.CreatePacket((byte)SetAddressType.SetSENDOUTGOINGPACKET, Adresses.Client.SendOutGoingPacket));
                 */

        }



        public void RemoveText(string Textname)
        {
            NetworkMessage msg = new NetworkMessage();
            msg.AddByte((byte)PipePacketType.RemoveDisplayText);
            msg.AddString(Textname);
            pipeSend.Write(msg.Data, 0, msg.Data.Length);

        }

        public void EnableHook()
        {
            pipeSend.WriteByte((byte)PipePacketType.EnableHook);
        }
        public void DissableHook()
        {
            pipeSend.WriteByte((byte)PipePacketType.DisableHook);
        }
        public void UnLoad()
        {
            pipeSend.WriteByte((byte)PipePacketType.Unload);
        }
        
        public bool Inject(string filename)
        {

            // Get a block of memory to store the filename in the client
            IntPtr remoteAddress = WinApi.VirtualAllocEx(
                client.Handle,
                IntPtr.Zero,
                (uint)filename.Length,
                WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                WinApi.MemoryProtection.ExecuteReadWrite);

            // Write the filename to the client's memory
            Memory.WriteStringNoEncoding(client.Handle, remoteAddress.ToInt32(), filename);

            // Start the remote thread, first loading our library
            IntPtr thread = WinApi.CreateRemoteThread(
                client.Handle, IntPtr.Zero, 0,
                WinApi.GetProcAddress(WinApi.GetModuleHandle("Kernel32"), "LoadLibraryA"),
                remoteAddress, 0, IntPtr.Zero);

            WinApi.WaitForSingleObject(thread, 0xFFFFFFFF); // Infinite

            // Free the memory used for the filename
            WinApi.VirtualFreeEx(
                client.Handle,
                remoteAddress,
                (uint)filename.Length,
                WinApi.AllocationType.Release);

            return thread.ToInt32() > 0 && remoteAddress.ToInt32() > 0;
        }

    }
}
