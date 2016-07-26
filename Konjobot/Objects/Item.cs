using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using KonjoBot.Packets;

namespace KonjoBot.Objects
{
   public class Item
    {
     private  int m_id;
     private int m_count;
     private Client client;
     private ItemLocation m_loc;
     private uint p;
 
   
   
     public Item(Client cl, int id, int count, ItemLocation loc,string name)
     {
         client = cl;
         m_id = id;
         m_count = count;
         m_loc = loc;
     }
     public Item(Client cl, int id, int count, ItemLocation loc)
         : this(cl, id, count, loc, "") { }

     public Item(Client cl, int id)
         : this(cl, id,0, null) { }
     public Item(int id, string name)
         : this(null,id,0,null,name) { }

     
 
     public bool HasExtraByte
     {
         get
         {

             return ItemData.IsStackable ||
                   ItemData.isLiquidContainer || //needs verification
                  ItemData.isLiquidPool;
         }
     }

   
     public DatReader.ItemData ItemData
     {
      get{
            return client.DatReader.GetData(this.Id);
      }
      
     }
         public int Id
         {
              get { return m_id; }
   
         }
         public int Count
         {
             get { return m_count; }
             set { m_count = value; }
         }
         public ItemLocation Location
         {
             get
             {
                 return m_loc;
             }
             set
             {
                 m_loc = value;
             }
         }
         public void Use(byte ContainerIndex =0)
         {
             Packets.OutGoing.ItemUse.Send(client, Location.ToLocation(), (ushort)Id, Location.StackOrder, ContainerIndex);

         }
      
         public void Use(Location loc)
         {
       

         }
         public void Move(Objects.ItemLocation toLocation)
         {
            if(toLocation !=null)
            {
                Move(toLocation, (byte)this.Count);
            }
             
         }

         public void Move(Objects.ItemLocation toLocation, byte count)
         {
             byte c = (byte)((count == 0) ? 1 : count);
             switch (Location.Type)
             {
                 case Constants.ItemLocationType.Ground :
                      Packets.OutGoing.MoveItem.Send(client, Location.ToLocation(), (ushort)Id, Location.StackOrder, toLocation.ToLocation(), c);
                      break;
                 case Constants.ItemLocationType.Container:
                      Packets.OutGoing.MoveItem.Send(client, Location.ToLocation(), (ushort)Id, Location.ContainerSlot, toLocation.ToLocation(), c);
                      break;
                 case Constants.ItemLocationType.Slot:
                      Packets.OutGoing.MoveItem.Send(client, Location.ToLocation(), (ushort)Id, (byte)Location.Slot, toLocation.ToLocation(), c);
                      break;
                
             }
         }
         public void Look()
         {
             //Point p = Location.ToPoint(client);
             //client.Input.Look(p);
         }

    }


   public class ItemLocation
   {
       public Constants.ItemLocationType Type;
       public byte ContainerId;
       public byte ContainerSlot;
       public Location GroundLocation;
       public byte StackOrder;
       public Constants.SlotNumber Slot;

       public ItemLocation() { }

       public static ItemLocation FromSlot(Constants.SlotNumber slot)
       {
           ItemLocation loc = new ItemLocation();
           loc.Type = Constants.ItemLocationType.Slot;
           loc.Slot = slot;
           return loc;
       }

       public static ItemLocation FromContainer(byte container, byte position)
       {
           ItemLocation loc = new ItemLocation();
           loc.Type = Constants.ItemLocationType.Container;
           loc.ContainerId = container;
           loc.ContainerSlot = position;
           loc.StackOrder = position;
           return loc;
       }

       public static ItemLocation FromLocation(Location location, byte stack)
       {
           ItemLocation loc = new ItemLocation();
           loc.Type = Constants.ItemLocationType.Ground;
           loc.GroundLocation = location;
           loc.StackOrder = stack;
           return loc;
       }

       public static ItemLocation FromLocation(Location location)
       {
           return FromLocation(location, 1);
       }

       public static ItemLocation FromItemLocation(ItemLocation location)
       {
           switch (location.Type)
           {
               case Constants.ItemLocationType.Container:
                   return ItemLocation.FromContainer(location.ContainerId, location.ContainerSlot);
               case Constants.ItemLocationType.Ground:
                   return ItemLocation.FromLocation(location.GroundLocation, location.StackOrder);
               case Constants.ItemLocationType.Slot:
                   return ItemLocation.FromSlot(location.Slot);
               default:
                   return null;
           }
       }

       /// <summary>
       /// Return a blank item location for packets (FF FF 00 00 00)
       /// </summary>
       /// <returns></returns>
       public static ItemLocation FromHotkey()
       {
           return FromSlot(Constants.SlotNumber.None);
       }

       public byte[] ToBytes()
       {
           byte[] bytes = new byte[5];

           switch (Type)
           {
               case Constants.ItemLocationType.Container:
                   bytes[00] = 0xFF;
                   bytes[01] = 0xFF;
                   bytes[02] = (byte)(0x40 + ContainerId);
                   bytes[03] = 0x00;
                   bytes[04] = ContainerSlot;
                   break;
               case Constants.ItemLocationType.Slot:
                   bytes[00] = 0xFF;
                   bytes[01] = 0xFF;
                   bytes[02] = (byte)Slot;
                   bytes[03] = 0x00;
                   bytes[04] = 0x00;
                   break;
               case Constants.ItemLocationType.Ground:
                   bytes[00] = GroundLocation.X.Low();
                   bytes[01] = GroundLocation.X.High();
                   bytes[02] = GroundLocation.Y.Low();
                   bytes[03] = GroundLocation.Y.High();
                   bytes[04] = (byte)GroundLocation.Z;
                   break;
           }

           return bytes;
       }

       public Location ToLocation()
       {
           Location newPos = new Location();

           switch (Type)
           {
               case Constants.ItemLocationType.Container:
                   newPos.X = 0xFFFF;
                   newPos.Y = (int)BitConverter.ToUInt16(new byte[] { (byte)(0x40 + ContainerId), 0x00 }, 0);
                   newPos.Z = (int)ContainerSlot;
                   break;
               case Constants.ItemLocationType.Slot:
                   newPos.X = 0xFFFF;
                   newPos.Y = (int)BitConverter.ToUInt16(new byte[] { (byte)Slot, 0x00 }, 0);
                   newPos.Z = 0;
                   break;
               case Constants.ItemLocationType.Ground:
                   newPos = GroundLocation;
                   break;
           }

           return newPos;
       }
   }
   public class Food : Item
   {
       public uint RegenerationTime;

       public Food(int id, string name, uint regenerationTime)
           : base(null, id,0,null,name)
       {
           RegenerationTime = regenerationTime;
       }

       public Food(Client client, int id, string name, uint regenerationTime)
           : base(client, id, 0, null,name)
       {
           RegenerationTime = regenerationTime;
       }
   }
   public class TransformingItem : Item
   {
       public Spell Spell;
       public uint SoulPoints;
       public Item OriginalItem;

       /// <summary>
       /// Default constructor.
       /// </summary>
       /// <param name="id">item id</param>
       /// <param name="name">item name</param>
       /// <param name="spell">spell used to create the item</param>
       /// <param name="soulPoints">amount of soul points needed to make the item</param>
       /// <param name="originalItem">the item that the spell words are used on to create this item</param>
       public TransformingItem(Client client, int id, string name, Spell spell, uint soulPoints, Item originalItem)
           : base(client,id,0,null,name)
       {
           Spell = spell;
           SoulPoints = soulPoints;
           OriginalItem = originalItem;
       }

       public TransformingItem(int id, string name, Spell spell, uint soulPoints, Item originalItem)
           : base(null,id,0,null,name)
       {
           Spell = spell;
           SoulPoints = soulPoints;
           OriginalItem = originalItem;
       }

   }
   public class Rune : TransformingItem
   {
       /// <summary>
       /// Default rune constructor.
       /// </summary>
       /// <param name="id">item id</param>
       /// <param name="name">item name</param>
       /// <param name="spell">spell used to create the rune</param>
       /// <param name="soulPoints">amount of soul points needed to make the rune</param>
       public Rune(Client client, int id, string name, Spell spell, uint soulPoints)
           : base(client, id, name, spell, soulPoints, Constants.Items.Rune.Blank)
       {
       }

       public Rune(int id, string name, Spell spell, uint soulPoints)
           : base(null, id, name, spell, soulPoints, Constants.Items.Rune.Blank)
       {
       }
   }

   public class ItemLocation1
   {
       public Location GroundLocation;
       public byte ContainerNumer;
       public byte ContainerSlot;
       public Constants.ItemLocationType Type;
       public Point PointLoc;
       public Constants.SlotNumber Slot;
       public static ItemLocation1 FromGround(Location loc)
       {
           ItemLocation1 j = new ItemLocation1();
           j.GroundLocation = loc;
           j.Type = Constants.ItemLocationType.Ground;
                
           return j;
       }
       public static ItemLocation1 FromContainer(byte containerSlot, byte containerNumer)
       {
           ItemLocation1 j = new ItemLocation1();
           j.Type = Constants.ItemLocationType.Container;
           j.ContainerNumer = containerNumer;
           j.ContainerSlot = containerSlot;

           return j;
       }
       public static ItemLocation1 FromSlot(Constants.SlotNumber s)
       {
           ItemLocation1 j = new ItemLocation1();
           j.Type = Constants.ItemLocationType.Slot;
           j.Slot = s;
           return j;
       }
    

     
   }
}
