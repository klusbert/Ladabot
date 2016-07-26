using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
using KonjoBot.Util;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace KonjoBot.Packets
{
    public class TibiaSock
    {
        private Client client;
        public TibiaSock(Client _client)
        {
            client = _client;
        }

        private int m_tibiaThread = 0;

        private int TibiaThread()
        {
            try
            {

          
            if (m_tibiaThread == 0)
            {
                Process proc = client.Process;

                double mtime = 0.0;
                double tmptime = 0.0;
                int pid = 0;
                foreach (ProcessThread pT in proc.Threads)
                {
                    tmptime = Math.Max(mtime, pT.UserProcessorTime.TotalSeconds);
                    if (tmptime > mtime)
                    {
                        pid = pT.Id;
                        mtime = tmptime;
                    }
                }
                m_tibiaThread = pid;

            }
            }
            catch (Exception)
            {
                return TibiaThread();
            }
            return m_tibiaThread;

        }
        private IntPtr OpenAndSuspendThread(int threadID)
        {

            IntPtr pOpenThread = default(IntPtr);

            pOpenThread = WinApi.OpenThread((WinApi.ThreadAccess.GET_CONTEXT | WinApi.ThreadAccess.SUSPEND_RESUME | WinApi.ThreadAccess.SET_CONTEXT), false, Convert.ToUInt32(TibiaThread()));
            WinApi.SuspendThread(pOpenThread);
            return pOpenThread;
        }
        private byte[] CreateOutgoingBuffer(byte[] dataBuffer, int length)
        {
            byte[] actualBuffer = new byte[1025];
            int size = Marshal.SizeOf(dataBuffer[0]) * dataBuffer.Length;
            IntPtr pnt = Marshal.AllocHGlobal(size);
            Marshal.Copy(dataBuffer, 0, pnt, length - 8);
            Marshal.Copy(pnt, actualBuffer, 8, length - 8);
            Marshal.FreeHGlobal(pnt);
            return actualBuffer;
        }
        private void ExecuteRemoteCode(IntPtr process, IntPtr codeAddress, uint arg)
        {
            IntPtr WorkThread = WinApi.CreateRemoteThread(process, IntPtr.Zero, 0, codeAddress, new IntPtr(arg), 0, IntPtr.Zero);

            WinApi.WaitForSingleObject(WorkThread, 0xffffffffu);
            WinApi.CloseHandle(WorkThread);

        }
        public void SendPacketToServerEx(byte[] dataBuffer, uint SendStreamData, uint SendStreamLength, uint SendPacketCall)
        {
            IntPtr MainThread = OpenAndSuspendThread(client.Process.Id);
            int OldLength = 0;
            byte[] OldData = new byte[1025];
            int length = dataBuffer.Length;
            IntPtr process = WinApi.OpenProcess(WinApi.PROCESS_ALL_ACCESS, 0, (uint)client.Process.Id);

            OldLength = Memory.ReadInt32(client.Handle, SendStreamLength);

            OldData = Memory.ReadBytes(client.Handle, SendStreamData, (uint)OldLength);
            length += 8;
            byte[] actualBuffer = CreateOutgoingBuffer(dataBuffer, length);

           
            client.Memory.WriteInt32(SendStreamLength, length);
            client.Memory.WriteBytes(SendStreamData, actualBuffer, (uint)length);
            CodeCaveHelper cv = new CodeCaveHelper();
            cv.AddLine((byte)0xb1, (byte)1); // talkmode   
            cv.AddLine((byte)0xB8, (uint)SendPacketCall); // this moves speakfunc address
            cv.AddLine((byte)0xff, (byte)0xD0); // call eax Thanks Darkstar

            cv.AddLine((byte)0xc3); //ret

            IntPtr CaveAddress = WinApi.VirtualAllocEx(client.Handle, IntPtr.Zero, (uint)cv.Data.Length, WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve, WinApi.MemoryProtection.ExecuteReadWrite);
            Memory.WriteBytes(client.Handle, CaveAddress.ToInt64(), cv.Data, (uint)cv.Data.Length);
         //   System.Windows.Forms.Clipboard.SetText(CaveAddress.ToString("X"));

            IntPtr thread = WinApi.CreateRemoteThread(client.Handle, IntPtr.Zero, 0, CaveAddress, IntPtr.Zero, 0, IntPtr.Zero);
            WinApi.WaitForSingleObject(thread, 0xFFFFFFFF);


            WinApi.VirtualFreeEx(client.Handle, CaveAddress, (uint)cv.Data.Length, WinApi.AllocationType.Release); //free up memory 
     
            
            client.Memory.WriteInt32(SendStreamLength, OldLength);
            client.Memory.WriteBytes(SendStreamData, OldData, (uint)OldLength);
            ResumeAndCloseThread(MainThread);
        }
        private void ResumeAndCloseThread(IntPtr thread)
        {
            WinApi.ResumeThread(thread);
            WinApi.CloseHandle(thread);
        }
        public void SendPacketToServer(byte[] dataBuffer)
        {

            SendPacketToServerEx(dataBuffer, (uint)0x94BE80, (uint)client.Addresses.Packet.SendPacketLenght, (uint)client.Addresses.Packet.SENDOUTGOINGPACKET);
        }
  
    }
}
