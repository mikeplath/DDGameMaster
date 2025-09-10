using System;
using System.Collections.Generic;
using System.Linq;

namespace DDGameMaster.Models.Equipment
{
    public class Inventory
    {
        private readonly List<Item> _items = new List<Item>();
        public const int MaxSlots = 9;
        
        public event EventHandler<InventoryChangedEventArgs>? InventoryChanged;
        
        public IReadOnlyList<Item> Items => _items.AsReadOnly();
        
        public bool AddItem(Item item)
        {
            // Check if item already exists and is stackable
            var existingItem = _items.FirstOrDefault(i => i.Name == item.Name && i.IsConsumable);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                OnInventoryChanged();
                return true;
            }
            
            // Add new item if there's space
            if (_items.Count < MaxSlots)
            {
                _items.Add(item);
                OnInventoryChanged();
                return true;
            }
            
            return false; // Inventory full
        }
        
        public bool RemoveItem(Item item)
        {
            var success = _items.Remove(item);
            if (success)
            {
                OnInventoryChanged();
            }
            return success;
        }
        
        public void UseItem(Item item)
        {
            item.Use();
            if (item.IsConsumable && item.Quantity <= 0)
            {
                RemoveItem(item);
            }
            OnInventoryChanged();
        }
        
        private void OnInventoryChanged()
        {
            InventoryChanged?.Invoke(this, new InventoryChangedEventArgs());
        }
    }
    
    public class InventoryChangedEventArgs : EventArgs { }
}