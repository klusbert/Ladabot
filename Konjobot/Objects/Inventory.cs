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
        public IEnumerable<Container> ReadContisar()
        {
            int count = client.Memory.ReadInt32(client.Addresses.Container.ContainerPointer);
            count = client.Memory.ReadInt32(count + 8);

            int baseAddresses = client.Memory.ReadInt32(client.Addresses.Container.ContainerPointer);
            baseAddresses = client.Memory.ReadInt32(baseAddresses + 4);
            List<int> AlreadyRead = new List<int>();
            List<int> ToRead = new List<int>();
            List<Container> Conts = new List<Container>();
            ToRead.Add(client.Memory.ReadInt32(baseAddresses));
            do
            {
                if(ToRead.Count() > 0)
                {
                    int BpAdr = ToRead[0];
                    AlreadyRead.Add(BpAdr);
                    int left = client.Memory.ReadInt32(BpAdr);
                    int Right = client.Memory.ReadInt32(BpAdr + 0x8);
                   if(!AlreadyRead.Contains(left)){

                   }

                }           




            } while (count < Conts.Count());

            return null;
        }
        public List<Container> ReadContAdr()
        {
            int count = client.Memory.ReadInt32(client.Addresses.Container.ContainerPointer);
            count = client.Memory.ReadInt32(count + 8);
            int baseAddresses = client.Memory.ReadInt32(client.Addresses.Container.ContainerPointer);
            baseAddresses = client.Memory.ReadInt32(baseAddresses + 4);

            List<int> AlreadyRead = new List<int>();
            List<int> ToRead = new List<int>();
            List<Container> Containers = new List<Container>();
            ToRead.Add(client.Memory.ReadInt32(baseAddresses));
            do
            {
                if(ToRead.Count > 0)
                {

              
                int nodeAddress = ToRead[0];
                AlreadyRead.Add(nodeAddress);
                int left = client.Memory.ReadInt32(nodeAddress);
                int Center = client.Memory.ReadInt32(nodeAddress + 4);
                int right = client.Memory.ReadInt32(nodeAddress + 0x8);
                if (!AlreadyRead.Contains(left)) { ToRead.Add(left); }
                if (!AlreadyRead.Contains(Center)){ToRead.Add(Center);}
                if (!AlreadyRead.Contains(right) && right != left) { ToRead.Add(right); }

                if (client.Memory.ReadByte(nodeAddress + 13) == 0) //isnot null
                    {
                        int indexBp = client.Memory.ReadInt32(nodeAddress + 16); //offset pro index

                        int bpStructAddr = client.Memory.ReadInt32(nodeAddress + 20); //offset pro add da bp
                        
                        Containers.Add(new Container(client, (uint)bpStructAddr, (byte)indexBp));
                    }
                ToRead.RemoveAt(0);
                }


            } while (Containers.Count < count);
            return Containers;

           

        }



        private class Node
        {
            public int BaseAddress;
            public int Left;
            public int Center;
            public int Right;
            public bool IsNull;
            public Node(Client _client, int _baseAddress)
            {
                BaseAddress = _baseAddress;
                Left = _client.Memory.ReadInt32(BaseAddress);
                Right = _client.Memory.ReadInt32(BaseAddress +8);
                IsNull = Convert.ToBoolean(_client.Memory.ReadByte(BaseAddress + 13));
            }
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

            ler.Add(client.Memory.ReadInt32(baseAddresses)); // first tree entry




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
        public IEnumerable<Item> GetContainerItems()
        {
            foreach (Container c in GetContainers())
            {
                foreach (Item i in c.GetItems())
                {
                    yield return i;
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
            if(onTile == null){return;}
            Item myItem = null;
            foreach (Item i in GetContainerItems())
            {
                if(i.Id == id)
                {
                    myItem = i;
                    break;
                }
            }
            if(myItem != null)
            {
                Packets.OutGoing.ItemUseOn.Send(client, myItem.Location.ToLocation(), (ushort)id, myItem.Location.StackOrder, onTile.Location, (ushort)onTile.Ground.Id, 0);
            }
            else
            {
                // use hotkey instead
                Packets.OutGoing.ItemUseOn.Send(client, ItemLocation.FromHotkey().ToLocation(), (ushort)id, 0, onTile.Location, (ushort)onTile.Ground.Id, 0);
            }
            
        }
        public void UseItemOnItem(uint FromItemId, Item onItem)
        {
            if (onItem == null) { return; }
            Item myItem = null;
            foreach (Item i in GetContainerItems())
            {
                if (i.Id == FromItemId)
                {
                    myItem = i;
                    break;
                }
            }
            if (myItem != null)
            {
                Packets.OutGoing.ItemUseOn.Send(Core.client, myItem.Location.ToLocation(), (ushort)FromItemId, myItem.Location.StackOrder, onItem.Location.GroundLocation, (ushort)onItem.Id, onItem.Location.StackOrder);
            }
            else
            {
                Packets.OutGoing.ItemUseOn.Send(Core.client, ItemLocation.FromHotkey().ToLocation(), (ushort)FromItemId, 0, onItem.Location.GroundLocation, (ushort)onItem.Id, onItem.Location.StackOrder);
            }
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
        /*
     public enum SlotNumber
     {
         None = 0,
         Head = 1,
         Necklace = 2,
         Backpack = 3,
         Armor = 4,
         Right = 5,
         Left = 6,
         Legs = 7,
         Feet = 8,
         Ring = 9,
         Ammo = 10,
       
     }
         */
        public Item GetItemInSlot(Constants.SlotNumber slot)
        {                   
            int SlotNumb = client.Addresses.Inventory.MaxSlots -(int)slot;
            
            int offSet = SlotNumb * client.Addresses.Inventory.StepSlot;
            int Address = (int)client.Addresses.Inventory.SlotStart +offSet;
            int ItemId = client.Memory.ReadInt32(Address + client.Addresses.Inventory.DistanceId);
            int Count = client.Memory.ReadInt32(Address + client.Addresses.Inventory.DistanceCount);

            return new Item(client,ItemId ,Count,ItemLocation.FromSlot(slot));   
        }
        public List<Item>GetSlotItems()
        {
            List<Item> m_list = new List<Item>();
            for (byte i = 0; i <= client.Addresses.Inventory.MaxSlots; i++)
            {
                Constants.SlotNumber b = (Constants.SlotNumber)i;
                Item item = Core.client.Inventory.GetItemInSlot(b);
                m_list.Add(item);
            }

            return m_list;
        }
        public void UseItemOnLocation(int itemid, Location loc)
        {

        }


    }
}
