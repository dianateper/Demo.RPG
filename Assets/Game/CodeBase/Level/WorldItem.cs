using System;
using Game.CodeBase.Common;
using Game.CodeBase.Inventory;
using UnityEngine;

namespace Game.CodeBase.Level
{
    public class WorldItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemType _itemType;
        public event Action<WorldItem> OnWorldItemIteract;
        public ItemType ItemType => _itemType;
        
        public void Interact() => OnWorldItemIteract?.Invoke(this);

        public void SetItemType(ItemType itemType) => _itemType = itemType;
    }
}