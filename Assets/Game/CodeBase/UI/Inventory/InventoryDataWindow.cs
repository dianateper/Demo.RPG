using System.Collections.Generic;
using Game.CodeBase.Core.Services.InputService;
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

        public void Construct(ItemsData itemsData, IInventory inventory, IInventoryInput inventoryInput)
        {
            _itemsData = itemsData;
            _inventory = inventory;
            _inventoryWindow.Construct(inventoryInput);
            _inventoryWindow.OnItemClick += ShowItemOverviewWindow;
            _inventoryWindow.OnRemoveFromInventoryClick += Hide;
            _itemOverviewWindow.OnApplyClick += ClearInventoryWindowAndCloseOverviewWindow;
            _itemOverviewWindow.OnCloseButtonClick += _itemOverviewWindow.Hide;
            _itemDescriptionWindow.OnCloseButtonClick += Hide;
        }

        private void OnDestroy()
        {
            _inventoryWindow.OnRemoveFromInventoryClick -= Hide;
            _inventoryWindow.OnItemClick -= ShowItemOverviewWindow;
            _itemOverviewWindow.OnCloseButtonClick -= Hide;
            _itemDescriptionWindow.OnCloseButtonClick -= _itemOverviewWindow.Hide;
            _itemOverviewWindow.OnApplyClick -= ClearInventoryWindowAndCloseOverviewWindow;
        }

        private void ClearInventoryWindowAndCloseOverviewWindow(ItemType itemType)
        {
            _inventoryWindow.Hide();
            _inventoryWindow.Show();
            _itemOverviewWindow.Hide();
        }

        public void ShowInventory() => _inventoryWindow.Show(_inventory);

        public void ShowItemDescription(ItemType item, WorldItem worldItem)
        {
            _itemDescriptionWindow.SetWorldItem(worldItem);
            _itemDescriptionWindow.Show(_itemsData.GetItem(item));
        }

        public override void Hide() => _windows.ForEach(w => w.Hide());

        private void ShowItemOverviewWindow(ItemType item) => _itemOverviewWindow.Show(_itemsData.GetItem(item));

        private void Hide(ItemType _) => Hide();
    }
}