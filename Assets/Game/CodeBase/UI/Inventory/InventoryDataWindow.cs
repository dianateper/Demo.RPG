using System.Collections.Generic;
using Game.CodeBase.Inventory;
using Game.CodeBase.Level;
using Game.CodeBase.StaticData;
using UnityEngine;

namespace Game.CodeBase.UI.Inventory
{
    public class InventoryDataWindow : WindowBase
    {
        [SerializeField] private InventoryWindow _inventoryWindow;
        [SerializeField] private ItemOverviewWindow _itemOverviewWindow;
        [SerializeField] private ItemDescriptionWindow _itemDescriptionWindow;

        private List<WindowBase> _windows;
        
        private ItemsData _itemsData;
        private IInventory _inventory;
        public InventoryWindow InventoryWindow => _inventoryWindow;
        public ItemOverviewWindow ItemOverviewWindow => _itemOverviewWindow;
        public ItemDescriptionWindow ItemDescriptionWindow => _itemDescriptionWindow;
        
        public override void Initialize()
        {
            _windows = new List<WindowBase> { _inventoryWindow, _itemOverviewWindow, _itemDescriptionWindow };
            base.Initialize();
            _windows.ForEach(w => w.Initialize());
        }

        public void Construct(ItemsData itemsData, IInventory inventory)
        {
            _itemsData = itemsData;
            _inventory = inventory;
            _inventoryWindow.OnItemClick += ShowItemOverviewWindow;
            _inventoryWindow.OnRemoveFromInventoryClick += RefreshInventoryWindow;
            _itemOverviewWindow.OnApplyClick += ClearInventoryWindowAndCloseOverviewWindow;
            _itemOverviewWindow.OnCloseButtonClick += _itemOverviewWindow.Hide;
            _itemOverviewWindow.OnCloseButtonClick += _inventoryWindow.ActivateFirstSlot;
            _itemDescriptionWindow.OnCloseButtonClick += Hide;
        }

        private void OnDestroy()
        {
            _inventoryWindow.OnRemoveFromInventoryClick -= RefreshInventoryWindow;
            _inventoryWindow.OnItemClick -= ShowItemOverviewWindow;
            _itemOverviewWindow.OnCloseButtonClick -= Hide;
            _itemDescriptionWindow.OnCloseButtonClick -= _itemOverviewWindow.Hide;
            _itemOverviewWindow.OnCloseButtonClick -= _inventoryWindow.ActivateFirstSlot;
            _itemOverviewWindow.OnApplyClick -= ClearInventoryWindowAndCloseOverviewWindow;
        }

        public void ShowInventory() => _inventoryWindow.Show(_inventory);

        public void ShowItemDescription(ItemType item, WorldItem worldItem)
        {
            _itemDescriptionWindow.SetWorldItem(worldItem);
            _itemDescriptionWindow.AnimateShow(_itemsData.GetItem(item));
        }

        public override void Hide() => _windows.ForEach(w => w.Hide());

        private void RefreshInventoryWindow(ItemType itemType)
        {
            _inventoryWindow.Hide();
            _inventoryWindow.Show();
        }

        private void ClearInventoryWindowAndCloseOverviewWindow(ItemType itemType)
        {
            RefreshInventoryWindow(itemType);
            _itemOverviewWindow.Hide();
        }

        private void ShowItemOverviewWindow(ItemType item)
        {
            if (_itemOverviewWindow.IsEnabled && _itemOverviewWindow.ItemType != item)
                _itemOverviewWindow.AnimateHide(() => _itemOverviewWindow.AnimateShow(_itemsData.GetItem(item)));
            else if(_itemOverviewWindow.IsEnabled == false)
                _itemOverviewWindow.AnimateShow(_itemsData.GetItem(item));
        }
    }
}