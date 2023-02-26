using System;
using System.Collections.Generic;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Inventory;
using UnityEngine;

namespace Game.CodeBase.UI.Inventory
{
    public class InventoryWindow : WindowBase
    {
        [SerializeField] private List<SlotView> _slotViews;
        private IInventory _inventory;

        private IInventoryInput _inventoryInput;
        private int _activeSlotIndex;
        
        public event Action<ItemType> OnItemClick;
        public event Action<ItemType> OnRemoveFromInventoryClick;

        public void Construct(IInventoryInput inventoryInput)
        {
            _inventoryInput = inventoryInput;
        }

        public void Show(IInventory inventory)
        {
            _activeSlotIndex = 0;
            _inventory = inventory;
            RegisterSlotViews();
            SubscribeInput();
            ActivateFirstSlot();
            Show();
        }

        public override void Hide()
        {
            base.Hide();
            ClearSlotViews();
            UnsubscribeInput();
        }

        private void ActivateFirstSlot()
        {
            if (_inventory.Slots.Count > 0)
                _slotViews[_activeSlotIndex].SetActive();
        }

        private void ActivateRightSlot()
        {
            if (_inventory.Slots.Count == 0) return;
            _slotViews[_activeSlotIndex].Deactivate();
            if (_activeSlotIndex >= _inventory.Slots.Count - 1) 
                _activeSlotIndex = -1;
            _activeSlotIndex++;
            _slotViews[_activeSlotIndex].SetActive();
        }

        private void ActivateLeftSlot()
        {
            if (_inventory.Slots.Count == 0) return;
            _slotViews[_activeSlotIndex].Deactivate();
            if (_activeSlotIndex <= 0) 
                _activeSlotIndex = _inventory.Slots.Count;
            _activeSlotIndex--;
            _slotViews[_activeSlotIndex].SetActive();
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

                _slotViews[_activeSlotIndex]?.Deactivate();
            }
        }

        private void UnsubscribeInput()
        {
            if (_inventoryInput != null)
            {
                _inventoryInput.OnLeftArrowPress -= ActivateLeftSlot;
                _inventoryInput.OnRightArrowPress -= ActivateRightSlot;
            }
        }

        private void SubscribeInput()
        {
            _inventoryInput.OnLeftArrowPress += ActivateLeftSlot;
            _inventoryInput.OnRightArrowPress += ActivateRightSlot;
        }

        private void RemoveFromInventory(ItemType itemType) => OnRemoveFromInventoryClick?.Invoke(itemType);

        private void ShowItemDetail(ItemType itemId) => OnItemClick?.Invoke(itemId);
    }
}
