using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace KonjoBot.Objects
{
   public class Container
    {
        Client client;
        public uint Adreess;
        private int m_number;
       public Container(Client cl, uint adr,int _num)
        {
            client = cl;
            Adreess = adr;
            m_number = _num;

          
        }
       public IEnumerable<Item> GetItems()
       {
           uint Adr = Adreess + client.Addresses.Container.DistanceItemStart;
           Adr = client.Memory.ReadUInt32(Adr);
           for (uint i = 0; i < Ammount; i++)
           {
               uint iAdr = Adr + i * client.Addresses.Container.StepContainerSlot;
               int Count = client.Memory.ReadInt32(iAdr + client.Addresses.Container.DistanceItemCount);
               int ItemId = client.Memory.ReadInt32(iAdr + client.Addresses.Container.DistanceItemId);
               yield return new Item(client, ItemId, Count, ItemLocation.FromContainer((byte)Number,(byte)i));
           }

          
       }
        public int Number
        {
            get { return m_number; }
        }
        public string Name
        {
            get
            {
                return client.Memory.ReadString(Adreess + client.Addresses.Container.DistanceName);
            }
        }
        public int Ammount
        {
            get
            {
                return client.Memory.ReadInt32(Adreess + client.Addresses.Container.DistanceAmount);
            }

        }
      
        public int Volume
        {
            get
            {
                return client.Memory.ReadInt32(Adreess + client.Addresses.Container.DistanceVolume);
            }
        }
        public int ID
        {
            get
            {
                return client.Memory.ReadInt32(Adreess + client.Addresses.Container.DistanceId);
            }
        }

      
        #region GUIStuff

     
       public void Close()
       {
           Packets.OutGoing.CloseContainer.Send(client, Number);
       }
      

        #endregion

    }
}
