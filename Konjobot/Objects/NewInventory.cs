using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace KonjoBot.Objects
{
    public class NewInventory
    {
     

      public  struct TreeEntry
        {
            public uint head; //Head is not the tree root, instead, Head.Parent is.
            public uint count;
        }
      public struct TreeNode
      {
          public uint left, parent, right;
          public byte color;
          public byte isNull;
          public uint key, ContainerStruct;
      }


        private Client client;
        public NewInventory(Client _client)
        {
            client = _client;
            
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
            List<Container> ContainerList = new List<Container>();
            ReadTree(ref ContainerList);
          return ContainerList.OrderBy(c => c.Number).ToList();
         //  return ContainerList;
        }

        private TreeEntry ReadTree(ref List<Container> containerList)
        {
            TreeEntry Entry = new TreeEntry();
            uint add = TreeHeadAddress;
            Entry.head = client.Memory.ReadUInt32(add + 4);
            Entry.count = client.Memory.ReadUInt32(add + 8);
            TreeNode Node = ReadTreeNode(Entry.head,ref containerList,Entry.count);
            ReadTreeNode(Node.parent, ref containerList, Entry.count);
            return Entry;
        }
        private TreeNode ReadTreeNode(uint nodeAddress, ref List<Container> containerList,uint count)
        {
            TreeNode Node = new TreeNode();    
           byte[] nodeStruct = client.Memory.ReadBytes(nodeAddress, 24);
            Packets.NetworkMessage msg = new Packets.NetworkMessage(nodeStruct);
        
            Node.left = msg.GetUInt32();
            Node.parent = msg.GetUInt32();
            Node.right = msg.GetUInt32();
            Node.color = msg.GetByte();
            Node.isNull = msg.GetByte();
            msg.Position += 2;
            Node.key = msg.GetUInt32();
            Node.ContainerStruct = msg.GetUInt32();
          
            if(Node.isNull == 0 && Node.key <= 16 && count > containerList.Count())
            {
                containerList.Add(new Container(client, Node.ContainerStruct, (int)Node.key));
                ReadTreeNode(Node.left,ref containerList,count);
                ReadTreeNode(Node.right, ref containerList,count);
            }
            return Node;

        }
        public uint TreeHeadAddress
        {
            get{
                return client.Memory.ReadUInt32(client.Addresses.Container.ContainerPointer);
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
                foreach (Item i in c.GetItems())
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
            if (onTile == null) { return; }
            Item myItem = null;
            foreach (Item i in GetContainerItems())
            {
                if (i.Id == id)
                {
                    myItem = i;
                    break;
                }
            }
            if (myItem != null)
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
            int SlotNumb = client.Addresses.Inventory.MaxSlots - (int)slot;

            int offSet = SlotNumb * client.Addresses.Inventory.StepSlot;
            int Address = (int)client.Addresses.Inventory.SlotStart + offSet;
            int ItemId = client.Memory.ReadInt32(Address + client.Addresses.Inventory.DistanceId);
            int Count = client.Memory.ReadInt32(Address + client.Addresses.Inventory.DistanceCount);

            return new Item(client, ItemId, Count, ItemLocation.FromSlot(slot));
        }
        public List<Item> GetSlotItems()
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
      
      
    }
}
