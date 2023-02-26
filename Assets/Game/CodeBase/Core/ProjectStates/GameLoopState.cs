using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.CameraLogic;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Core.States;
using Game.CodeBase.Level;
using Game.CodeBase.PlayerLogic;
using UnityEngine;

namespace Game.CodeBase.Core.ProjectStates
{
    public class GameLoopState : IGamePlayState<PayloadData>
    {
        private readonly IPayloadDataStateSwitcher _payloadStateSwitcher;
        private readonly IStateSwitcher _stateSwitcher;
        private IInputService _inputService;
        private IPlayer _player;
      
        private LevelData _levelData;
        private List<WorldItem> _items;
        private CameraRaycaster _raycaster;
        private IUpdateableHandler _updateableHandler;
        private PayloadData _payloadData;
    
        public GameLoopState(IPayloadDataStateSwitcher payloadStateSwitcher, IStateSwitcher stateSwitcher)
        {
            _payloadStateSwitcher = payloadStateSwitcher;
            _stateSwitcher = stateSwitcher;
        }
        
        public void Enter(PayloadData payload)
        {
            _payloadData = payload;
            _player = payload.Player;
            _player.OnDie += EnterGameOverState;
            _inputService = ServiceLocator.ResolveService<IInputService>();
            _inputService.ToggleInventory += LoadInventoryState;
            SetupWorldItems();
            _player.EnableInput();
        }

        public void Exit()
        {
            _player.DisableInput();
            _player.OnDie -= EnterGameOverState;
            _inputService.ToggleInventory -= LoadInventoryState;
            foreach (var item in _items)
                item.OnWorldItemIteract -= ShowItemDescription;
        }

        private void ShowItemDescription(WorldItem worldItem)
        {
            _payloadData.WorldPayloadData.WordItem = worldItem;
            LoadInventoryState();
        }

        private void LoadInventoryState() => _payloadStateSwitcher.SwitchState<InventoryState>(_payloadData);
        
        private void SetupWorldItems()
        {
            _items = Object.FindObjectsOfType<WorldItem>().ToList();
            foreach (var item in _items)
                item.OnWorldItemIteract += ShowItemDescription;
        }

        private void EnterGameOverState() => 
            _stateSwitcher.SwitchState<GameOverState>();
    }
}