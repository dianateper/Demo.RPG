using UnityEngine;

namespace Game.CodeBase.Inventory
{
    public interface IItem
    {
        public ItemType ItemId { get; }
        public string Description { get; }
        public string Name { get; }
        public Sprite Sprite { get; }
    }
}