using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.CodeBase.Inventory
{
    public class Inventory : IInventory
    {
        private readonly List<IInventorySlot> _inventorySlots;
        public bool IsFull => _inventorySlots.All(slot => slot.IsFull) && _inventorySlots.Count >= Capacity;
        public int DefaultSlotsCapacity { get; }
        public int Capacity { get; }
        public List<IInventorySlot> Slots => _inventorySlots;

        public Inventory(int capacity, int slotCapacity = 1)
        {
            Capacity = capacity;
            DefaultSlotsCapacity = slotCapacity;
            _inventorySlots = new List<IInventorySlot>(DefaultSlotsCapacity);
        }

        public bool TryAddItemToSlot(IItem item)
        {
            var slot = _inventorySlots.Find(t => t.Item.ItemId == item.ItemId && t.IsFull == false);
            
            if (IsFull && slot == null) 
                return false;
            
            if (slot == null)
            {
                slot = new InventorSlot(DefaultSlotsCapacity);
                _inventorySlots.Add(slot);
            }

            slot.AddItemToSlot(item);
            
            return true;
        }

        public IItem GetItemFromSlot(ItemType itemType)
        {
            var slot = _inventorySlots.Find(t => t.Item.ItemId == itemType);

            if (slot == null)
                return null;
            
            var item = slot.GetItemFromSlot();

            if (slot.IsEmpty) 
                _inventorySlots.Remove(slot);

            return item;
        }
    }
}