using System.Collections.Generic;

namespace Game.CodeBase.Inventory
{
    public interface IInventory
    {
        public bool IsFull { get; }
        public int Capacity { get; }
        public int DefaultSlotsCapacity { get; }
        List<IInventorySlot> Slots { get; }
        public bool TryAddItemToSlot(IItem item);
        public IItem GetItemFromSlot(ItemType itemType);
    }
}