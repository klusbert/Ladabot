using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Objects;
using System.Threading;

namespace KonjoBot.Packets
{

   public class Parser
    {
       private Client client;
       public delegate void TileAddThingDelegate(Item Item);
       public event TileAddThingDelegate TileAddThing;

       public Parser(Client _client)
       {
           client = _client;

    
       }



       
    }
}
