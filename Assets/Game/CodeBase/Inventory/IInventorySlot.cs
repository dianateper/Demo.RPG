namespace Game.CodeBase.Inventory
{
    public interface IInventorySlot
    {
        public string Id { get; }
        public IItem Item { get; }
        public int Count { get; }
        public int Capacity { get; }
        public bool IsFull { get; }
        public bool IsEmpty { get; }

        public void AddItemToSlot(IItem item);
        public IItem GetItemFromSlot();
    }
}