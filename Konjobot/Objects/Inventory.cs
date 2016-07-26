using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
namespace KonjoBot.Objects
{
   public class Inventory
    {
       [DllImport("kernel32.dll")]
       static extern int ReadProcessMemory(
           IntPtr pHandle,
           UInt32 address,
           IntPtr buffer,
           int size,
           out int readedBytes
       );

        Client client;
      
        public Inventory(Client cl)
        {
            client = cl;


        }

       
        private int ContainerPointer()
        {
            return client.Memory.ReadInt32(client.Addresses.Container.ContainerPointer) + 4;
        }
        public int ContainersCount()
        {

            return client.Memory.ReadInt32(ContainerPointer() + 4);

        }
        public IEnumerable<Container> GetContainers()
        {
            int count = client.Memory.ReadInt32(client.Addresses.Container.ContainerPointer);
            count = client.Memory.ReadInt32(count + 8);

            int baseAddresses = client.Memory.ReadInt32(client.Addresses.Container.ContainerPointer);
            baseAddresses = client.Memory.ReadInt32(baseAddresses + 4);

      
            List<int> lido = new List<int>(); //already read

            List<int> ler = new List<int>(); //to read

            List<int> toRead = new List<int>();

            List<int> toRemove = new List<int>();

            ler.Add(client.Memory.ReadInt32(baseAddresses));



            int lastBp = 0;

            if (count > 0)
            {

            again:

                foreach (int BpAddr in ler)
                {

                    lido.Add(BpAddr);



                    int address1 = client.Memory.ReadInt32(BpAddr);

                    int address2 = client.Memory.ReadInt32(BpAddr + 0x4);

                    int address3 = client.Memory.ReadInt32(BpAddr + 0x8);



                    if (address1 != baseAddresses) { if (!lido.Contains(address1)) { toRead.Add(address1); } }

                    if (address2 != baseAddresses) { if (!lido.Contains(address2)) { toRead.Add(address2); } }

                    if (address3 != baseAddresses) { if (!lido.Contains(address3)) { toRead.Add(address3); } }




                    int indexBp = client.Memory.ReadInt32(BpAddr + 16); //offset pro index

                    int bpStructAddr = client.Memory.ReadInt32(BpAddr + 20); //offset pro add da bp



                    yield return new Container(client, (uint)bpStructAddr, (byte)indexBp);

                }

                foreach (int i in lido)

                    ler.RemoveAll(ind => i == ind);



                if (toRead.Count > 0)
                {

                    ler.AddRange(toRead);

                    toRead.Clear();

                    goto again;

                }

            }
        }
      
        public Item GetItem(int id)
        {
            foreach (Container c in GetContainers())
            {
                foreach (Item  i in c.GetItems())
                {
                    if (i.Id == id)
                    {
                        return i;
                    }
                }
            }
            return null;
        }
   
  
        public void UseItemOnTile(uint id, Tile onTile)
        {
             Packets.OutGoing.ItemUseOn.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, 0, onTile.Location, (ushort)onTile.Ground.Id, 0);
        }

        public Container GetContainer(int number)
        {
            foreach (Container c in GetContainers())
            {
                if (c.Number == number)
                {
                    return c;
                }
            }
            return null;
        }
        public void UseItemOnCreature(uint id, byte stack, int creatureId)
        {
          
             Packets.OutGoing.ItemUseOnCreature.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, stack, (uint)creatureId);
        }
        public void UseItemOnSelf(uint id)
        {
             UseItemOnCreature(id, 0, client.Memory.ReadInt32(client.Addresses.Player.Id));
        }

        public void UseItemOnLocation(int itemid, Location loc)
        {

        }


    }
}
