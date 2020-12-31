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


        public delegate void ItemUse(Location loc, byte stack, int itemId, byte index);

        public event PacketListner IncommingPacket;
        public event PacketListner OutgoingPacket;


        public event ItemUse ItemUsePacket;
      
        #endregion

        public delegate bool IncomingPacketListener(Packets.IncomingPacket packet);
        public virtual event IncomingPacketListener ReceivedInitGameIncomingPacket;
        public event IncomingPacketListener ReceivedGMActionsIncomingPacket;
        public event IncomingPacketListener ReceivedLoginErrorIncomingPacket;
        public event IncomingPacketListener ReceivedLoginAdviceIncomingPacket;
        public event IncomingPacketListener ReceivedLoginWaitIncomingPacket;
        public event IncomingPacketListener ReceivedPingBackIncomingPacket;
        public event IncomingPacketListener ReceivedPingIncomingPacket;
        public event IncomingPacketListener ReceivedChallengeIncomingPacket;
        public event IncomingPacketListener ReceivedDeathIncomingPacket;

        public event IncomingPacketListener ReceivedFullMapIncomingPacket;
        public event IncomingPacketListener ReceivedTopRowIncomingPacket;
        public event IncomingPacketListener ReceivedRightRowIncomingPacket;
        public event IncomingPacketListener ReceivedBottomRowIncomingPacket;
        public event IncomingPacketListener ReceivedLeftRowIncomingPacket;
        public event IncomingPacketListener ReceivedUpdateTileIncomingPacket;
        public event IncomingPacketListener ReceivedCreateOnMapIncomingPacket;
        public event IncomingPacketListener ReceivedChangeOnMapIncomingPacket;
        public event IncomingPacketListener ReceivedDeleteOnMapIncomingPacket;
        public event IncomingPacketListener ReceivedMoveCreatureIncomingPacket;
        public event IncomingPacketListener ReceivedOpenContainerIncomingPacket;
        public event IncomingPacketListener ReceivedCloseContainerIncomingPacket;
        public event IncomingPacketListener ReceivedCreateContainerIncomingPacket;
        public event IncomingPacketListener ReceivedChangeInContainerIncomingPacket;
        public event IncomingPacketListener ReceivedDeleteInContainerIncomingPacket;
        public event IncomingPacketListener ReceivedSetInventoryIncomingPacket;
        public event IncomingPacketListener ReceivedDeleteInventoryIncomingPacket;
        public event IncomingPacketListener ReceivedOpenNPCTradeIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerGoodsIncomingPacket;
        public event IncomingPacketListener ReceivedCloseNPCTradeIncomingPacket;
        public event IncomingPacketListener ReceivedOwnTradeIncomingPacket;
        public event IncomingPacketListener ReceivedCounterTradeIncomingPacket;
        public event IncomingPacketListener ReceivedCloseTradeIncomingPacket;
        public event IncomingPacketListener ReceivedAmbientIncomingPacket;
        public event IncomingPacketListener ReceivedGraphicalEffectIncomingPacket;
        public event IncomingPacketListener ReceivedTextEffectIncomingPacket;
        public event IncomingPacketListener ReceivedMissileEffectIncomingPacket;
        public event IncomingPacketListener ReceivedMarkCreatureIncomingPacket;
        public event IncomingPacketListener ReceivedTrappersIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureHealthIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureLightIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureOutfitIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSpeedIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureSkullIncomingPacket;
        public event IncomingPacketListener ReceivedCreaturePartyIncomingPacket;
        public event IncomingPacketListener ReceivedCreatureUnpassIncomingPacket;
        public event IncomingPacketListener ReceivedEditTextIncomingPacket;
        public event IncomingPacketListener ReceivedEditListIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerDataBasicIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerDataCurrentIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerSkillsIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerStateIncomingPacket;
        public event IncomingPacketListener ReceivedClearTargetIncomingPacket;
        public event IncomingPacketListener ReceivedSpellDelayIncomingPacket;
        public event IncomingPacketListener ReceivedSpellGroupDelayIncomingPacket;
        public event IncomingPacketListener ReceivedMultiUseDelayIncomingPacket;
        public event IncomingPacketListener ReceivedTalkIncomingPacket;
        public event IncomingPacketListener ReceivedChannelsIncomingPacket;
        public event IncomingPacketListener ReceivedOpenChannelIncomingPacket;
        public event IncomingPacketListener ReceivedOpenPrivateChannelIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationChannelIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationRemoveIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationCancelIncomingPacket;
        public event IncomingPacketListener ReceivedRuleViolationLockIncomingPacket;
        public event IncomingPacketListener ReceivedOpenOwnChannelIncomingPacket;
        public event IncomingPacketListener ReceivedCloseChannelIncomingPacket;
        public event IncomingPacketListener ReceivedTextMessageIncomingPacket;
        public event IncomingPacketListener ReceivedCancelWalkIncomingPacket;
        public event IncomingPacketListener ReceivedWalkWaitIncomingPacket;
        public event IncomingPacketListener ReceivedFloorChangeUpIncomingPacket;
        public event IncomingPacketListener ReceivedFloorChangeDownIncomingPacket;
        public event IncomingPacketListener ReceivedChooseOutfitIncomingPacket;
        public event IncomingPacketListener ReceivedVipAddIncomingPacket;
        public event IncomingPacketListener ReceivedVipLoginIncomingPacket;
        public event IncomingPacketListener ReceivedVipLogoutIncomingPacket;
        public event IncomingPacketListener ReceivedTutorialHintIncomingPacket;
        public event IncomingPacketListener ReceivedAutomapFlagIncomingPacket;
        public event IncomingPacketListener ReceivedQuestLogIncomingPacket;
        public event IncomingPacketListener ReceivedQuestLineIncomingPacket;
        public event IncomingPacketListener ReceivedChannelEventIncomingPacket;
        public event IncomingPacketListener ReceivedItemInfoIncomingPacket;
        public event IncomingPacketListener ReceivedPlayerInventoryIncomingPacket;
        public event IncomingPacketListener ReceivedMarketEnterIncomingPacket;
        public event IncomingPacketListener ReceivedMarketLeaveIncomingPacket;
        public event IncomingPacketListener ReceivedMarketDetailIncomingPacket;
        public event IncomingPacketListener ReceivedMarketBrowseIncomingPacket;
        public event IncomingPacketListener ReceivedShowModalDialogIncomingPacket;

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
                    System.Threading.Thread t = new Thread(() => ReadingPackets(msg));
                    t.Start();
                }
              
                pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), pipe);
            }
            catch (Exception ex)
            {

                pipe.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(BeginRead), pipe);
            }
        }
        private void ReadingPackets(NetworkMessage msg)
        {

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
                
                    if (IncommingPacket != null)
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
               
                case PipePacketType.test:
                    uint adr = msg.GetUInt32();
                    System.Windows.Forms.MessageBox.Show(adr.ToString("X"));
                    break;

            }
        }
           
        private void ParseIncomingPacket(NetworkMessage msg)
        {
            IncomingPacketType packetType = (IncomingPacketType)msg.GetByte();
            IncomingPacket incomingPacket;
            switch (packetType)
            {
                case IncomingPacketType.CreateOnMap:
                    incomingPacket = new Packets.Incoming.TileAddThing(client);
                    incomingPacket.ParseMessage(msg, PacketDestination.Client);
                    if (ReceivedCreateOnMapIncomingPacket != null)
                    {
                        ReceivedCreateOnMapIncomingPacket.Invoke(incomingPacket);
                    }
                    break;
                case IncomingPacketType.PlayerData:
                    incomingPacket = new Packets.Incoming.PlayerDataPacket(client);
                    incomingPacket.ParseMessage(msg, PacketDestination.Client);
                    if (ReceivedPlayerDataCurrentIncomingPacket != null)
                    {
                        ReceivedPlayerDataCurrentIncomingPacket(incomingPacket);
                    }
                    break;
                case IncomingPacketType.MoveCreature:
                    incomingPacket = new Packets.Incoming.MoveCreaturePacket(this.client);
                    incomingPacket.ParseMessage(msg, PacketDestination.Client);
                    if (ReceivedMoveCreatureIncomingPacket != null)
                    {
                        ReceivedMoveCreatureIncomingPacket(incomingPacket);
                    }
                    break;
                case IncomingPacketType.DeleteInContainer:
                    incomingPacket = new Packets.Incoming.DeleteInContainerPacket(this.client);
                    incomingPacket.ParseMessage(msg, PacketDestination.Client);
                    if (ReceivedDeleteInContainerIncomingPacket != null)
                    {
                        ReceivedDeleteInContainerIncomingPacket(incomingPacket);
                    }
                    break;
                case IncomingPacketType.MissileEffect:
                    incomingPacket = new Packets.Incoming.MissileEffect(this.client);
                    incomingPacket.ParseMessage(msg, PacketDestination.Client);
                    if (ReceivedMissileEffectIncomingPacket != null)
                    {
                        ReceivedMissileEffectIncomingPacket(incomingPacket);
                    }
                    break;
                case IncomingPacketType.TextMessage:
                    incomingPacket = new Packets.Incoming.TextMessagePacket(this.client);
                    incomingPacket.ParseMessage(msg, PacketDestination.Client);
                    NetworkMessage networkMessage = new NetworkMessage(false);
                    if (this.ReceivedTextMessageIncomingPacket != null && this.ReceivedTextMessageIncomingPacket(incomingPacket))
                    {
                        incomingPacket.ToNetworkMessage(networkMessage);
                        this.SendPacketToClient(networkMessage.Data);
                    }
                    break;
                case IncomingPacketType.CreatureHealth:
                    incomingPacket = new Packets.Incoming.CreatureHealthPacket(this.client);
                    incomingPacket.ParseMessage(msg, PacketDestination.Client);
                    if (this.ReceivedCreatureHealthIncomingPacket != null)
                    {
                        this.ReceivedCreatureHealthIncomingPacket(incomingPacket);
                    }
                    break;
            }
        }
        private void ForwardCreatureLight(uint creatureId, byte intense, byte color)
        {
            NetworkMessage pack = new NetworkMessage();
            pack.Position = 0;
            if (creatureId == client.Player.Id)
            {
                color = 255;
                intense = 27;

            }
            pack.AddByte(0x8D);
            pack.AddUInt32((uint)Core.client.Player.Id);
            pack.AddByte(27);
            pack.AddByte(250);
            SendPacketToClient(pack.Data);
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
