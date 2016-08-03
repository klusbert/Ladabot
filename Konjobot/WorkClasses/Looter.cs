using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
using System.Threading;
using KonjoBot.Objects.Bot;
namespace KonjoBot.WorkClasses
{
   
    public class Looter
    {
        private Util.Timer timer;
        public List<Corpse> CorpseList;
    
        private byte[] LastCorpeses;
        public Looter()
        {
            CorpseList = new List<Corpse>();
            timer = new Util.Timer(500, true);
            Core.client.HookProxy.TileAddThing += HookProxy_TileAddThing;
            LastCorpeses = new byte[16];

            timer.Execute += Timer_Execute;
        }

        private void Timer_Execute()
        {
            try
            {

         
            if (Core.IsLoggedIn && Core.Global.LooterEnabled && !Core.Global.WalkerEnabled)
            {
                foreach (Container  c in Core.client.Inventory.GetContainers())
                {
                    for (int j = 0; j < 16; j++)
                    {
                        if(LastCorpeses[j] == 1 && c.Number == j)
                        {
                            List<Item> Items = c.GetItems().ToList();
                            Items.Reverse();

                            foreach (Item i in Items)
                            {
                                if (IsLootItem(Convert.ToInt32(i.Id)))
                                {
                                    TakeItem(i, Core.Global.LootList.FirstOrDefault(o => o.Id == i.Id));
                                    Thread.Sleep(300);
                                }
                                else if (IsFood(Convert.ToInt32(i.Id)))
                                {
                                    i.Use();
                                    Thread.Sleep(300);
                                }
                            }
                           retry:

                            foreach (Item i in c.GetItems())
                            {
                                if (i.ItemData.IsContainer)
                                {
                                    Core.WaitForLoot = System.DateTime.Now.AddMilliseconds(3);
                                    i.Use(Convert.ToByte(c.Number));
                                    System.DateTime d = System.DateTime.Now;

                                    while (Core.client.Inventory.GetContainer(c.Number).ID != i.Id)
                                    {
                                        TimeSpan tSpan = System.DateTime.Now - d;
                                        if (tSpan.TotalSeconds >= 2)
                                        {
                                            LastCorpeses[j] = 0;
                                        }
                                        Thread.Sleep(100);
                                    }

                                    goto retry;
                                }
                            }
                           Core.WaitForLoot = DateTime.Now;
                            LastCorpeses[j] = 0;
                        }
                    }
                }
            }
            }
            catch (Exception)
            {

                
            }
        }

        Location TargetLoc;
        void HookProxy_TileAddThing(Location location, byte stack, int itemid)
        {
            if(location.Z != Core.client.PlayerLocation.Z)
            {
                return;
            }
           Item Corpse = new Item(Core.client, itemid);
           Corpse.Location = ItemLocation.FromLocation(location, stack);
            if(Corpse.ItemData.IsCorpse != true)
            {
                return;
            }
            if (Core.Global.OpenCorpses && Core.Global.LooterEnabled)
           {
               if(Core.Global.LootFriendly)
               {
                  
                   if(Core.client.Player.RedSquare > 0)
                   {
                       Creature c = Core.client.Battlelist.GetScreenCreatures().FirstOrDefault(t => t.Id == Core.client.Player.RedSquare);
                       if(c!= null)
                       {
                           TargetLoc = c.Location;
                       }
                       if(location.Equals(TargetLoc))
                       {

                           if (Core.Global.WalkerEnabled)
                           {
                               CorpseList.Add(new Corpse(location,Corpse,DateTime.Now ));
                           }
                           else
                           {
                                  byte containerIndex = (byte)Core.client.Inventory.ContainersCount();
                                  Core.LastCorpse = containerIndex;
                                   LastCorpeses[containerIndex] = 1;
                                  Core.SleepRandom();
                                  Corpse.Use(containerIndex);
                                  
                              
                           }
                       }
                   }
               }
               else
               {
                   if(location.DistanceTo(Core.client.PlayerLocation) <= 8)
                   {
                       if (Core.Global.WalkerEnabled)
                       {
                           CorpseList.Add(new Corpse(location, Corpse, DateTime.Now));
                       }
                       else
                       {
                              byte containerIndex = (byte)Core.client.Inventory.ContainersCount();
                              Core.LastCorpse = containerIndex;
                              LastCorpeses[containerIndex] = 1;
                              Core.SleepRandom();
                              Corpse.Use(containerIndex);                             
                          

                        }
                   }
               }
             
           }
        }
      public ItemLocation LootToIndex(Item item,byte index)
      {
            Objects.Container c = Core.client.Inventory.GetContainer(index);

            ItemLocation loc;
            if (c.Ammount < c.Volume)
            {
                if (c.Ammount == 0)
                {
                    loc = ItemLocation.FromContainer((byte)c.Number, 0);
                    return loc;
                }
                Item LootItem = c.GetItems().FirstOrDefault((Item j) => j.Id == item.Id & j.Count < 100);
                if (LootItem != null)
                {
                    loc = LootItem.Location;
                    return loc;
                }
                else
                {
                    loc = ItemLocation.FromContainer((byte)c.Number, (byte)c.Ammount);
                    return loc;
                }
            }
            else
            {
                if(item.ItemData.IsStackable)
                {
                    foreach (Item o in c.GetItems())
                    {
                        if (o.Id == item.Id && o.Count < 100)
                        {
                            loc = o.Location;
                            return loc;

                        }
                    }
                }

              
                //this means the bp is full lets see if we find a bp insde 
                if(Core.Global.OpenNextBp)
                {
                      foreach (Item o in c.GetItems())
                    {
                        if(o.ItemData.IsContainer)
                        {
                            o.Use(index);
                            Core.SleepRandom();
                            return LootToIndex(item, index);
                        }
                    }
                }
            }
                return null;
      }
        public ItemLocation LootTo(Item item)
        {
            ItemLocation loc = default(ItemLocation);
            for (int i = 0; i <= Core.LastCorpse - 1; i++)
            {
                Objects.Container c = Core.client.Inventory.GetContainer(Convert.ToByte(i));
                if (c.Ammount < c.Volume)
                {
                    if (c.Ammount == 0)
                    {
                        loc = ItemLocation.FromContainer((byte)c.Number, 0);
                        break; 
                    }
                    Item LootItem = c.GetItems().FirstOrDefault((Item j) => j.Id == item.Id & j.Count < 100);
                    if (LootItem != null)
                    {
                        loc = LootItem.Location;
                        break; 
                    }
                    else
                    {
                        loc = ItemLocation.FromContainer((byte)c.Number, (byte)c.Ammount);
                        break; 
                    }
                }
                else
                {
                    foreach (Item o in c.GetItems())
                    {
                        if (o.Id == item.Id && o.Count < 100)
                        {
                            loc = o.Location;
                            break; 

                        }
                    }
                }

            }
            return loc;
        }
        private void TakeItem(Item i, KonjoBot.Objects.Bot.LootItem lot)
        {
            if (lot.Cap <= Core.client.Player.Capacity)
            {
                if (lot.Cap * i.Count > Core.client.Player.Capacity)
                {
                    i.Count = 1;
                }

                switch (lot.Destination)
                {
                    case Constants.LootDestination.Arrow:
                        i.Move(ItemLocation.FromSlot(Constants.SlotNumber.Ammo));
                        break;
                    case Constants.LootDestination.Container:
                        if(lot.ContainerDestionation == 16)// any
                        {
                            i.Move(LootTo(i));
                        }
                        else
                        {
                            i.Move(LootToIndex(i, lot.ContainerDestionation));
                        }
                      
                        break;
                    case Constants.LootDestination.Ground:
                        i.Move(ItemLocation.FromLocation(Core.client.PlayerLocation));
                        break;
                    case Constants.LootDestination.LeftHand:
                        i.Move(ItemLocation.FromSlot(Constants.SlotNumber.Left));
                        break;
                    case Constants.LootDestination.RightHand:
                        i.Move(ItemLocation.FromSlot(Constants.SlotNumber.Right));
                        break;

                }
                
            }
        }

        public bool IsLootItem(int id)
        {
            foreach (var t in Core.Global.LootList)
            {
                if (t.Id == id)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsFood(int id)
        {
            return Constants.ItemLists.Foods.ContainsKey(id);
        }
        private Container GetLootContainer()
        {
            foreach (Container c in Core.client.Inventory.GetContainers())
            {
                if(c.Number == Core.LastCorpse)
                {
                    return c;
                }
            }
            return null;
        }
        public bool IsLooting
        {
            get
            {
                if (Core.WaitForLoot >= System.DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value == true)
                {
                    Core.WaitForLoot = System.DateTime.Now.AddSeconds(3);
                }
                else
                {
                    Core.WaitForLoot = System.DateTime.Now;
                }
            }
        }
        private int CorpseCount(Location loc)
        {
            int count = 0;
            foreach (Corpse i in CorpseList)
            {
                if (i.Location == loc)
                {
                    count += 1;
                }
            }
            return count;
        }
        public bool LootCorpse(Corpse item, bool isInCorpseList = true)
        {

            try
            {
                if (item.Location.Z != Core.client.PlayerLocation.Z && isInCorpseList)
                {
                    CorpseList.Remove(item);
                    return true;
                }              

                Core.client.MiniMap.Stop();
                int ContIndex = Core.client.Inventory.ContainersCount();
                if (isInCorpseList && !item.OpenCorpse(ContIndex))
                {                  
                   CorpseList.Remove(item);                  

                   Core.WaitForLoot = System.DateTime.Now.AddSeconds(3);              
                                                     
                }

             
            retry:
                Core.LastCorpse = (byte)ContIndex;
                Thread.Sleep(500);
          
                foreach (Container c in Core.client.Inventory.GetContainers())
                {
                    if (c.Number == ContIndex)
                    {
                        if (c.ID == 3497)
                        {
                            Core.LastCorpse = 16;
                            return false;
                        }
                        List<Item> Items = c.GetItems().ToList();
                        Items.Reverse();

                        foreach (Item i in Items)
                        {
                            if (IsLootItem(Convert.ToInt32(i.Id)))
                            {
                                TakeItem(i, Core.Global.LootList.FirstOrDefault(o => o.Id == i.Id));
                                Core.SleepRandom();
                            }
                            else if (IsFood(Convert.ToInt32(i.Id)))
                            {
                                i.Use();
                                Core.SleepRandom();
                            }
                        }
                        foreach (Item i in c.GetItems())
                        {
                            if (i.ItemData.IsContainer)
                            {
                                Core.WaitForLoot = System.DateTime.Now.AddMilliseconds(3);
                                i.Use(Convert.ToByte(c.Number));
                                System.DateTime d = System.DateTime.Now;

                                while (Core.client.Inventory.GetContainer(c.Number).ID != i.Id)
                                {
                                    TimeSpan tSpan = System.DateTime.Now - d;
                                    if (tSpan.TotalSeconds >= 2)
                                    {
                                        return false;
                                    }
                                    Core.SleepRandom();
                                }
                                
                                goto retry;
                            }
                        }
                        if (CorpseCount(item.Location) > 1)
                        {
                            /* add browse field
                             
                            for (int x = -2; x <= 2; x++)
                            {
                                for (int y = -2; y <= 2; y++)
                                {
                                    Tile t = Core.client.Map.GetTile(new Location(Core.client.PlayerLocation.X + x, Core.client.PlayerLocation.Y + y, Core.client.PlayerLocation.Z));
                                    if (t.IsBlocking() == false)
                                    {
                                        item.Move(ItemLocation.FromLocation(t.Location));
                                        goto endfor;
                                    }
                                }
                            }
                             */
                        }
                    endfor:
                     

                        c.Close();
                        Core.LastCorpse = 16;
                        Core.WaitForLoot = System.DateTime.Now;
                        Core.LastCorpseLocation = Location.Invalid;
                        if (isInCorpseList)
                        {
                            CorpseList.Remove(item);
                        }
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
