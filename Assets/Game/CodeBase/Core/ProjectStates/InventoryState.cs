using System.Collections;
using System.Threading.Tasks;
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
    public class InventoryState : IGamePlayState<PayloadData>
    {
        private readonly IPayloadDataStateSwitcher _stateSwitcher;
        private InventoryDataWindow _inventoryDataWindow;
        private ItemsData _itemsData;
        private UIFactory _uiFactory;
        private WorldItemFactory _worldItemFactory;
        private IInventory _inventory;
        private LevelData _levelData;
        private IPlayer _player;
        private PayloadData _payloadData;
        private IInventoryInput _inventoryInput;

        public InventoryState(IPayloadDataStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void Enter(PayloadData payload)
        {
            _payloadData = payload;
            _player = payload.Player;
            _inventory = payload.Inventory;
            _levelData = payload.LevelData;
            _itemsData = _levelData.GetItemsData();
            _worldItemFactory = ServiceLocator.ResolveService<WorldItemFactory>();
            _uiFactory = ServiceLocator.ResolveService<UIFactory>();
            _inventoryInput = ServiceLocator.ResolveService<IInventoryInput>();
            _inventoryInput.OnHideInventory += HideInventoryAndEnterGameLoopState;

            _inventoryDataWindow = payload.InventoryDataWindow;
            _inventoryDataWindow.ItemOverviewWindow.OnApplyClick += ApplyItem;
            _inventoryDataWindow.ItemDescriptionWindow.OnAddToInventoryClick += AddItemToInventory;
            _inventoryDataWindow.InventoryWindow.OnRemoveFromInventoryClick += SpawnWorldItem;
            _inventoryDataWindow.ItemDescriptionWindow.OnCloseButtonClick += HideInventoryAndEnterGameLoopState;

            if (_payloadData.WorldPayloadData.WordItem != null)
                ShowWorldItemDetail(_payloadData.WorldPayloadData.WordItem);
            else
                LoadInventory();

            RegisterInput();
        }

        public void Exit()
        {
            _payloadData.WorldPayloadData.WordItem = null;
            _inventoryDataWindow.ItemOverviewWindow.OnApplyClick -= ApplyItem;
            _inventoryDataWindow.ItemDescriptionWindow.OnAddToInventoryClick -= AddItemToInventory;
            _inventoryDataWindow.InventoryWindow.OnRemoveFromInventoryClick -= SpawnWorldItem;
            _inventoryDataWindow.ItemDescriptionWindow.OnCloseButtonClick -= HideInventoryAndEnterGameLoopState;
            _inventoryInput.OnHideInventory -= HideInventoryAndEnterGameLoopState;
            _inventoryInput.IsEnabled = false;
        }

        private async Task RegisterInput()
        {
            await Task.Delay(200);
            _inventoryInput.IsEnabled = true;
        }

        private void HideInventoryAndEnterGameLoopState()
        {
            _inventoryDataWindow.Hide();
            EnterGameLoopState();
        }

        private void SpawnWorldItem(ItemType itemType)
        {
            _inventory.GetItemFromSlot(itemType);
            _worldItemFactory.CreateWorldItem(itemType, _player.Transform.position);
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

        private void EnterGameLoopState() => _stateSwitcher.SwitchState<GameLoopState>(_payloadData);

        private void ShowWorldItemDetail(WorldItem worldItem) => _inventoryDataWindow.ShowItemDescription(worldItem.ItemType, worldItem);

        private void LoadInventory() => _inventoryDataWindow.ShowInventory();
    }
}