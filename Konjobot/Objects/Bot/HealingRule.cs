using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Objects.Bot
{
    public class HealingRule
    {
        public enum ActionType
        {
            Spell,
            Item,
        }


        public string Action { get; set; }

        public HealingRule.ActionType Type { get; set; }

        public string Condition { get; set; }

        public string Is { get; set; }

        public string Value { get; set; }
        public bool Enable { get; set; }

        public Action CheckRule(Client client)
        {
            if (this.Is == "Bellow")
            {
                if (this.GetVal(client) < int.Parse(this.Value))
                {
                    return delegate ()
                    {
                        this.DoAction(client);
                    };
                }
            }
            else if (this.Is == "Above" && this.GetVal(client) > int.Parse(this.Value))
            {
                return delegate ()
                {
                    this.DoAction(client);
                };
            }
            return null;
        }
        public void DoAction(Client client)
        {
            if (this.Type == HealingRule.ActionType.Item)
            {
                int itemId = this.GetItemId();
                if (itemId > 0)
                    client.Inventory.UseItemOnSelf((uint)itemId);
            }
            if (this.Type != HealingRule.ActionType.Spell)
                return;
            Spell spell = client.SpellList.FindSpell(this.Action);
            if (Core.Healer.Mana < spell.ManaPoints)
                return;

            Packets.OutGoing.Speech.SendConsole(client, this.Action);

        }
        private int GetVal(Client client)
        {

            switch (this.Condition)
            {
                case "Health":
                    return Core.Healer.Health;
                case "HealthPercent":
                    return Core.Healer.HealthPercent;
                case "Mana":
                    return Core.Healer.Mana;
                case "ManaPercent":
                    return Core.Healer.ManaPercent;

            }
            return 0;
        }
        private int GetItemId()
        {
            switch (this.Action)
            {
                case "Great Health Potion":
                    return 239;
                case "Great Mana Potion":
                    return 238;
                case "Great Spirit Potion":
                    return 7642;
                case "Health Potion":
                    return 266;
                case "Intense Healing Rune":
                    return 3152;
                case "Mana Potion":
                    return 268;
                case "Small Health Potion":
                    return 7876;
                case "Strong Health Potion":
                    return 236;
                case "Strong Mana Potion":
                    return 237;
                case "Supreme Health Potion":
                    return 23375;
                case "Ultimate Healing Rune":
                    return 3160;
                case "Ultimate Health Potion":
                    return 7643;
                case "Ultimate Mana Potion":
                    return 23373;
                case "Ultimate Spirit Potion":
                    return 23374;


            }
            return 0;
        }
    }

}
