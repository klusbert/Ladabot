using KonjoBot.Constants;
using KonjoBot.Objects;
using System;

namespace KonjoBot.Packets
{
    public class Packet
    {
        public PacketDestination Destination { get; set; }

        public bool Forward { get; set; }

        public Client Client { get; set; }

        public Packet(Client c)
        {
            this.Client = c;
            this.Forward = true;
        }

        public virtual void ToNetworkMessage(NetworkMessage msg)
        {
            throw new NotImplementedException();
        }

        public virtual bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            return false;
        }

        public virtual bool ParseMessage(
          NetworkMessage msg,
          PacketDestination destination,
          NetworkMessage outMsg)
        {
            return false;
        }

        public bool Send()
        {
            return true;
        }
    }
}
