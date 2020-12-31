using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
namespace KonjoBot.Objects.Bot
{
    public class Corpse
    {
        public Location Location;
        public Item Item;
        public DateTime Time;

        public Corpse(Location _loc,Item _item,DateTime _date)
        {
            Location = _loc;
            Item = _item;
            Time = _date;
        }
        public bool OpenCorpse(int ContIndex)
        {
            TimeSpan span = DateTime.Now - Time;
            int RandomIn = Core.GetRandomInt(Core.MinThreadWait, Core.MaxThreadWait);
            while(span.TotalMilliseconds <= RandomIn)
            {
                System.Threading.Thread.Sleep(10);
                span = DateTime.Now - Time;
            }
            UpdateCorpse();
            if(Item != null)
            {
                Item.Use((byte)Core.client.Inventory.ContainersCount());
                System.DateTime maxwait = System.DateTime.Now;
                while (Core.client.Inventory.ContainersCount() == ContIndex)
                {
                     span = System.DateTime.Now - maxwait;
                    if (span.TotalMilliseconds > 1000)
                    {                    
                        return false;
                    }
                    System.Threading.Thread.Sleep(10);
                }
                return true;
            }
            return false;
        }
        private void UpdateCorpse()
        {
            Tile tile = Core.client.Map.GetTile(Location);
           
            Item i = null;
            foreach (Item item in tile.Items)
            {
                if(item.ItemData.IsContainer ||item.ItemData.IsCorpse)
                {
                    i = item;
                }
            }
            Item = i;
         
        }
    }
}
