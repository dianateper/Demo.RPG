using Game.CodeBase.Inventory;
using Game.CodeBase.Level;
using Game.CodeBase.PlayerLogic;
using Game.CodeBase.StaticData;
using UnityEngine;

namespace Game.CodeBase.UI.Inventory
{
    public class InventoryDataWindow : WindowBase
    {
        [SerializeField] private InventoryWindow _inventoryWindow;
        [SerializeField] private ItemOverviewWindow _itemOverviewWindow;
        [SerializeField] private ItemDescriptionWindow _itemDescriptionWindow;

        private ItemsData _itemsData;
        private IInventory _inventory;
        public InventoryWindow InventoryWindow => _inventoryWindow;
        public ItemOverviewWindow ItemOverviewWindow => _itemOverviewWindow;
        public ItemDescriptionWindow ItemDescriptionWindow => _itemDescriptionWindow;

        private IPlayerInput _playerInput;

        public override void Initialize()
        {
            base.Initialize();
            _inventoryWindow.Initialize();
            _itemDescriptionWindow.Initialize();
            _itemOverviewWindow.Initialize();
        }

        public void Construct(ItemsData itemsData, IInventory inventory, IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _itemsData = itemsData;
            _inventory = inventory;
            _inventoryWindow.OnItemClick += ShowItemOverviewWindow;
            _inventoryWindow.OnRemoveFromInventoryClick += Hide;
            _itemOverviewWindow.OnApplyClick += _ => Hide();
            _itemOverviewWindow.OnCloseButtonClick += Hide;
            _itemDescriptionWindow.OnCloseButtonClick += Hide;
        }

        private void OnDestroy()
        {
            _inventoryWindow.OnRemoveFromInventoryClick -= Hide;
            _inventoryWindow.OnItemClick -= ShowItemOverviewWindow;
            _itemOverviewWindow.OnCloseButtonClick -= Hide;
            _itemDescriptionWindow.OnCloseButtonClick -= Hide;
            _itemOverviewWindow.OnApplyClick -= Hide;
        }

        public void ShowInventory()
        {
            _playerInput.DisableInput();
            _inventoryWindow.Show(_inventory);
        }

        public override void Hide()
        {
            _inventoryWindow.Hide();
            _itemOverviewWindow.Hide();
            _itemDescriptionWindow.Hide();
            _playerInput.EnableInput();
        }

        public void ShowItemDescription(ItemType item, WorldItem worldItem)
        {
            _playerInput.DisableInput();
            _itemDescriptionWindow.SetWorldItem(worldItem);
            _itemDescriptionWindow.Show(_itemsData.GetItem(item));
        }

        private void ShowItemOverviewWindow(ItemType item)
        {
            _playerInput.DisableInput();
            _itemOverviewWindow.Show(_itemsData.GetItem(item));
        }

        private void Hide(ItemType _) => Hide();
    }
}