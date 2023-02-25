using System;
using Game.CodeBase.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Game.CodeBase.UI.Inventory
{
    public class ItemDescriptionWindow : ItemDescriptionBaseWindow
    {
        [SerializeField] private Button _addToInventory;
        public event Action<WorldItem> OnAddToInventoryClick;

        private WorldItem _item;
        
        public override void Initialize()
        {
            base.Initialize();
            _addToInventory.onClick.AddListener(AddToInventory);
        }

        private void AddToInventory() => OnAddToInventoryClick?.Invoke(_item);

        public void SetWorldItem(WorldItem worldItem) => _item = worldItem;
    }
}