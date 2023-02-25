using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Core.States;
using Game.CodeBase.Inventory;
using Game.CodeBase.Level;
using Game.CodeBase.PlayerLogic;
using Game.CodeBase.StaticData;
using Game.CodeBase.UI;
using Game.CodeBase.UI.Inventory;
using Game.CodeBase.UI.Windows;
using UnityEngine;

namespace Game.CodeBase.Core.ProjectStates
{
    public class GameLoopState : IGamePlayState<PayloadData>
    {
        private readonly IStateSwitcher _stateSwitcher;
        private IInputService _inputService;
        private IInventory _inventory;
        private PlayerBase _player;
        private InventoryDataWindow _inventoryDataWindow;
        private LevelData _levelData;
        private ItemsData _itemsData;
        private UIFactory _uiFactory;

        public GameLoopState(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }
        
        public void Enter(PayloadData payload)
        {
            _player = payload.PlayerBase;
            _inventory = payload.Inventory;
            _levelData = payload.LevelData;
            _itemsData = _levelData.GetItemsData();
            _player.OnDie += EnterGameOverState;
            
            _inputService = ServiceLocator.ResolveService<IInputService>();
            _inputService.ToggleInventory += LoadInventory;

            _uiFactory = ServiceLocator.ResolveService<UIFactory>();
            
            SetupWorldItems();

            _inventoryDataWindow = payload.InventoryDataWindow;
            _inventoryDataWindow.ItemOverviewWindow.OnApplyClick += ApplyItem;
            _inventoryDataWindow.ItemDescriptionWindow.OnAddToInventoryClick += AddItemToInventory;
        }

        private void AddItemToInventory(WorldItem worldItem)
        {
            if (_inventory.TryAddItemToSlot(_itemsData.GetItem(worldItem.ItemType)))
            {
                _inventoryDataWindow.Hide();
                Object.Destroy(worldItem.gameObject);
            }
            else
            {
                ShowMessage(Constants.MessageInventoryIsFull);
            }
        }

        private void ShowMessage(string message)
        {
            var window = _uiFactory.CreateWindow(WindowId.Message) as MessageWindow;
            window.ShowMessage(message);
        }

        private void ApplyItem(ItemType itemType)
        {
            _inventory.GetItemFromSlot(itemType);
            _player.ApplyInventoryItem(itemType);
        }

        public void Exit()
        {
            _player.OnDie -= EnterGameOverState;
            _inputService.ToggleInventory -= LoadInventory;
            _inventoryDataWindow.ItemOverviewWindow.OnApplyClick -= ApplyItem;
            _inventoryDataWindow.ItemDescriptionWindow.OnAddToInventoryClick -= AddItemToInventory;
        }

        private void SetupWorldItems()
        {
            var items = Object.FindObjectsOfType<WorldItem>();
            foreach (var item in items)
                item.OnWorldItemIteract += ShowItemDescription;
        }

        private void LoadInventory()
        {
            if (_inventoryDataWindow.InventoryWindow.IsEnabled())
                _inventoryDataWindow.Hide();
            else
                _inventoryDataWindow.ShowInventory();
        }

        private void ShowItemDescription(WorldItem item) =>
            _inventoryDataWindow.ShowItemDescription(item.ItemType, item);

        private void EnterGameOverState() => 
            _stateSwitcher.SwitchState<GameOverState>();
    }
}