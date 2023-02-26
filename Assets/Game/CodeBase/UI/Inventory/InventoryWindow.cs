using System;
using System.Collections.Generic;
using Game.CodeBase.Inventory;
using UnityEngine;

namespace Game.CodeBase.UI.Inventory
{
    public class InventoryWindow : WindowBase
    {
        [SerializeField] private List<SlotView> _slotViews;
        private IInventory _inventory;
      
        public event Action<ItemType> OnItemClick;
        public event Action<ItemType> OnRemoveFromInventoryClick;
        
        public void Show(IInventory inventory)
        {
            _inventory = inventory;
            for (int i = 0; i < _inventory.Slots.Count; i++)
            {
                var slot = _inventory.Slots[i];
                _slotViews[i].SetSlot(slot);
                _slotViews[i].OnItemClick += ShowItemDetail;
                _slotViews[i].OnRemoveItemFromInventoryClick += RemoveFromInventory;
            }

            Show();
        }

        public override void Hide()
        {
            base.Hide();
            if (_inventory != null)
            {
                for (int i = 0; i < _inventory.Slots.Count; i++)
                {
                    _slotViews[i].OnItemClick -= ShowItemDetail;
                    _slotViews[i].OnRemoveItemFromInventoryClick -= RemoveFromInventory;
                    _slotViews[i].Clear();
                }
            }
        }
        
        private void RemoveFromInventory(ItemType itemType) => OnRemoveFromInventoryClick?.Invoke(itemType);
        private void ShowItemDetail(ItemType itemId) => OnItemClick?.Invoke(itemId);

        public bool IsEnable() => _canvas.enabled;
    }
}
