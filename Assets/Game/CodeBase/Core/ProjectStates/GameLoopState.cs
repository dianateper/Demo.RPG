using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.CameraLogic;
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
        private WorldItemFactory _worldItemFactory;
        private List<WorldItem> _items;
        private CameraRaycaster _raycaster;
        private IUpdateableHandler _updateableHandler;

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
            _raycaster = new CameraRaycaster(Camera.main, _inputService);
            _worldItemFactory = ServiceLocator.ResolveService<WorldItemFactory>();
            _inputService.ToggleInventory += LoadInventory;

            _uiFactory = ServiceLocator.ResolveService<UIFactory>();

            SetupWorldItems();

            _inventoryDataWindow = payload.InventoryDataWindow;
            _inventoryDataWindow.ItemOverviewWindow.OnApplyClick += ApplyItem;
            _inventoryDataWindow.ItemDescriptionWindow.OnAddToInventoryClick += AddItemToInventory;
            _inventoryDataWindow.InventoryWindow.OnRemoveFromInventoryClick += SpawnWorldItem;
        }

        public void Exit()
        {
            _raycaster.DeInitialize();
            _player.OnDie -= EnterGameOverState;
            _inputService.ToggleInventory -= LoadInventory;
            _inventoryDataWindow.ItemOverviewWindow.OnApplyClick -= ApplyItem;
            _inventoryDataWindow.ItemDescriptionWindow.OnAddToInventoryClick -= AddItemToInventory;
            _inventoryDataWindow.InventoryWindow.OnRemoveFromInventoryClick -= SpawnWorldItem;
        }

        private void SpawnWorldItem(ItemType itemType)
        {
            _inventory.GetItemFromSlot(itemType);
            var item = _worldItemFactory.CreateWorldItem(itemType, _player.transform.position);
            item.OnWorldItemIteract += ShowItemDescription;
            _items.Add(item);
        }

        private void AddItemToInventory(WorldItem worldItem)
        {
            if (_inventory.TryAddItemToSlot(_itemsData.GetItem(worldItem.ItemType)))
            {
                _inventoryDataWindow.Hide();
                _items.Remove(worldItem);
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

        private void SetupWorldItems()
        {
            _items = Object.FindObjectsOfType<WorldItem>().ToList();
            foreach (var item in _items)
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