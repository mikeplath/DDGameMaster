using System;
using System.Collections.Generic;

namespace DDGameMaster.Models.Equipment
{
    public class EquipmentSlots
    {
        private Dictionary<EquipmentSlot, Item?> _equippedItems = new Dictionary<EquipmentSlot, Item?>();
        
        public event EventHandler<EquipmentChangedEventArgs>? EquipmentChanged;
        
        public EquipmentSlots()
        {
            // Initialize all slots as empty
            foreach (EquipmentSlot slot in Enum.GetValues<EquipmentSlot>())
            {
                _equippedItems[slot] = null;
            }
        }
        
        public Item? GetEquippedItem(EquipmentSlot slot)
        {
            return _equippedItems.TryGetValue(slot, out var item) ? item : null;
        }
        
        public bool EquipItem(EquipmentSlot slot, Item item)
        {
            if (CanEquipToSlot(item, slot))
            {
                _equippedItems[slot] = item;
                item.IsEquipped = true;
                OnEquipmentChanged();
                return true;
            }
            return false;
        }
        
        public Item? UnequipItem(EquipmentSlot slot)
        {
            var item = _equippedItems[slot];
            if (item != null)
            {
                _equippedItems[slot] = null;
                item.IsEquipped = false;
                OnEquipmentChanged();
            }
            return item;
        }
        
        private bool CanEquipToSlot(Item item, EquipmentSlot slot)
        {
            return slot switch
            {
                EquipmentSlot.Head => item.Type == ItemType.Armor,
                EquipmentSlot.Chest => item.Type == ItemType.Armor,
                EquipmentSlot.Feet => item.Type == ItemType.Armor,
                EquipmentSlot.MainHand => item.Type == ItemType.Weapon,
                EquipmentSlot.OffHand => item.Type == ItemType.Weapon || item.Type == ItemType.Shield,
                _ => false
            };
        }
        
        private void OnEquipmentChanged()
        {
            EquipmentChanged?.Invoke(this, new EquipmentChangedEventArgs());
        }
    }
    
    public enum EquipmentSlot
    {
        Head,
        Chest,
        Feet,
        MainHand,
        OffHand
    }
    
    public class EquipmentChangedEventArgs : EventArgs { }
}