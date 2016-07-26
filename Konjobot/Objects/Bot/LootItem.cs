using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonjoBot.Objects.Bot
{   
    public class LootItem
    {
        public int Id;
        public Constants.LootDestination Destination;
        public int Cap;
        public byte ContainerDestionation;

        public LootItem()
        {
            Id = 0;
            Destination = Constants.LootDestination.Container;
        }
        public override string ToString()
        {
            return Id.ToString() + " " + Destination.ToString();
        }

    }
}
