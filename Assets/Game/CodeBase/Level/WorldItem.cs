using System;
using Game.CodeBase.Inventory;
using UnityEngine;

namespace Game.CodeBase.Level
{
    public class WorldItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemType _itemId;
        public event Action<WorldItem> OnWorldItemIteract;
        public ItemType ItemType => _itemId;
        
        public void Interact()
        {
            OnWorldItemIteract?.Invoke(this);
        }
    }
}