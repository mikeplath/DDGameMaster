using System;

namespace DDGameMaster.Models.Equipment
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public ItemType Type { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsConsumable { get; set; }
        public string IconPath { get; set; } = "";
        public bool IsEquipped { get; set; }
        
        public virtual void Use()
        {
            if (IsConsumable && Quantity > 0)
            {
                Quantity--;
            }
        }
        
        public override string ToString()
        {
            return Quantity > 1 ? $"{Name} ({Quantity})" : Name;
        }
    }
    
    public enum ItemType
    {
        Weapon,
        Armor,
        Shield,
        Tool,
        Consumable,
        Misc,
        QuestItem
    }
}