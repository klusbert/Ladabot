using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KonjoBot.Objects
{
   public class Battlelist
    {
        Client client;
        DateTime lastcall;
  
        IEnumerable<Creature> m_creatues;
        public Battlelist(Client cl)
        {
            client = cl;
         //   lastcall = DateTime.Now;
         
        }

       
        public IEnumerable<Creature> GetCreatures()
        {
            TimeSpan span = DateTime.Now - lastcall;
        
            if(span.TotalMilliseconds >= 1000)
            {
                lastcall = DateTime.Now;
                List<Creature> _creatues = new List<Creature>();
                byte[] cache = client.Memory.ReadBytes(client.Addresses.Battlelist.Start, client.Addresses.Battlelist.MaxCreatures * client.Addresses.Battlelist.Step);
                for (int i = 0; i < client.Addresses.Battlelist.MaxCreatures; i++)
                {
                    uint adr = client.Addresses.Battlelist.Start + (uint)i * client.Addresses.Battlelist.Step;
                    bool IsViseble = BitConverter.ToBoolean(cache, i * (int)client.Addresses.Battlelist.Step + 164);
                    if (IsViseble)
                    {
                        _creatues.Add(new Creature(client, adr));
                    }
                }
                m_creatues = _creatues;
                return m_creatues;
            }
            else
            {
                return m_creatues;
            }
           
         
        }
   
        public IEnumerable<Creature> GetScreenCreatures()
        {
            foreach (Creature cr in GetCreatures())
            {
                if (cr.IsOnScreen)
                {
                    yield return cr;
                }
            }
        }

     

        
        public Player GetPlayer()
        {
           
            foreach (Creature c in GetCreatures())
            {
                if (c.Id == client.Memory.ReadInt32(client.Addresses.Player.Id))
                {
                    return new Player(client,c.Address);
                }
            }
            return null;
        }
    }
}
