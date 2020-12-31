using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonjoBot.Constants;
using KonjoBot.Objects;
using KonjoBot.Objects.Bot;
using KonjoBot.Packets;
using KonjoBot.Packets.Incoming;
using KonjoBot.Util; 
namespace KonjoBot.WorkClasses
{
    public  class Healer
    {
        Timer m_timer;
        public DateTime LastEat;
        public int Health { get; set; }
        public int HealthPercent { get; set; }

        public int Mana { get; set; }

        public int ManaPercent { get; set; }

        public int Hunger { get; set; }
        public Healer()
        {
            m_timer = new Util.Timer(500, true);
            m_timer.Execute += m_timer_Execute;
            this.LastEat = DateTime.Now;
            Core.client.HookProxy.ReceivedPlayerDataCurrentIncomingPacket += this.HookProxy_ReceivedPlayerDataCurrentIncomingPacket;
        }

        private bool HookProxy_ReceivedPlayerDataCurrentIncomingPacket(IncomingPacket packet)
        {
            PlayerDataPacket playerDataPacket = (PlayerDataPacket)packet;
            this.Health = (int)playerDataPacket.Health;
            this.HealthPercent = playerDataPacket.HealthPercent;
            this.Mana = (int)playerDataPacket.Mana;
            this.ManaPercent = playerDataPacket.ManaPercent;
            this.Hunger = (int)playerDataPacket.Regeneration;
            return true;
        }

        void m_timer_Execute()
        {
            foreach (HealingRule healingRule in Core.Global.HealingRules)
            {
                if (healingRule.Enable)
                {
                    Action action = healingRule.CheckRule(Core.client);
                    if (action != null)
                    {
                        action();
                        return;
                    }
                }
            }
            CheckEat();
            CheckOtGold();
            CheckFish();
            CheckForRefill();
        }

        private void CheckEat()
        {
            if (!Core.Global.AutoEat) return;
            if (this.Hunger < 100)
            {
                foreach (Item containerItem in Core.client.Inventory.GetContainerItems())
                {
                    if (containerItem.IsFood)
                    {
                        containerItem.Use((byte)0);
                        return;
                    }
                }
            }
        }
        private void CheckOtGold()
        {
            if (!Core.Global.AutChangeGold) return;
            foreach (Item item in Core.client.Inventory.GetContainerItems())
            {
                if (item.Id == 3031 && item.Count == 100)
                {
                    item.Use(0);
                    return;
                }
                if (item.Id == 3035 && item.Count == 100)
                {
                    item.Use(0);
                    return;
                }
            }
        }
        private void CheckFish()
        {
            if (!Core.Global.AutoFish) return;
            
                Core.client.Map.Fish(true);
            
        }
        private void CheckForRefill()
        {
            if (!Core.Global.RefillAmmo) return;
            Item weapon = Core.client.Inventory.GetItemInSlot(SlotNumber.Weapon);
            Item ammo = Core.client.Inventory.GetItemInSlot(SlotNumber.Ammo);
            if(ammo.Id > 0 && ammo.ItemData.IsStackable)
            {
                //search for refill
                foreach (Item containerItem in Core.client.Inventory.GetContainerItems())
                {
                    if (containerItem.Id == ammo.Id)
                    {
                        containerItem.Move(ItemLocation.FromSlot(SlotNumber.Ammo));
                        break;
                    }
                }
            }
            if(weapon.Id > 0 && weapon.ItemData.IsStackable)
            {
                foreach (Item containerItem in Core.client.Inventory.GetContainerItems())
                {
                    if (containerItem.Id == weapon.Id)
                    {
                        containerItem.Move(ItemLocation.FromSlot(SlotNumber.Weapon));
                        break;
                    }
                }
            }


        }

    }
}
