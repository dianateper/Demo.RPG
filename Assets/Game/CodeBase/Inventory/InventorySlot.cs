using System;

namespace Game.CodeBase.Inventory
{
    public class InventorSlot : IInventorySlot
    {
        public string Id { get; }
        public IItem Item { get; private set; }
        public int Count { get; private set; }
        public int Capacity { get; }

        public bool IsFull => Capacity == Count;
        public bool IsEmpty => Count == 0;
        
        public void AddItemToSlot(IItem item)
        {
            Count++;
            Item = item;
        }

        public IItem GetItemFromSlot()
        {
            Count--;
            return Item;
        }

        public InventorSlot(int capacity)
        {
            Id = new Guid().ToString();
            Count = 0;
            Capacity = capacity;
        }
    }
}