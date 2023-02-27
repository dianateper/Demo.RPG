using System;
using System.Collections.Generic;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.CodeBase.UI.Inventory
{
    public class InventoryWindow : WindowBase
    {
        [SerializeField] private List<SlotView> _slotViews;
        private IInventory _inventory;

        private int _activeSlotIndex;
        
        public event Action<ItemType> OnItemClick;
        public event Action<ItemType> OnRemoveFromInventoryClick;

        public void Show(IInventory inventory)
        {
            _activeSlotIndex = 0;
            _inventory = inventory;
            RegisterSlotViews();
            ActivateFirstSlot();
            Show();
        }

        public override void Hide()
        {
            base.Hide();
            ClearSlotViews();
        }

        public void ActivateFirstSlot()
        {
            if (_inventory.Slots.Count > 0) 
                EventSystem.current.SetSelectedGameObject(_slotViews[0].gameObject);
        }
        
        private void RegisterSlotViews()
        {
            for (int i = 0; i < _inventory.Slots.Count; i++)
            {
                var slot = _inventory.Slots[i];
                _slotViews[i].SetSlot(slot);
                _slotViews[i].OnItemClick += ShowItemDetail;
                _slotViews[i].OnRemoveItemFromInventoryClick += RemoveFromInventory;
            }
        }

        private void ClearSlotViews()
        {
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
    }
}
