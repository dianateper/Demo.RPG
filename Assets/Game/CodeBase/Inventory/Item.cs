using System;
using UnityEngine;

namespace Game.CodeBase.Inventory
{
    [Serializable]
    public class Item : IItem
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite;
        public ItemType ItemId => _itemType;
        public string Name => _name;
        public string Description => _description;
        public Sprite Sprite => _sprite;

        public IItem Clone()
        {
            return new Item
            {
                _itemType = _itemType,
                _name = _name,
                _description = _description,
                _sprite = _sprite
            };
        }
    }
}
