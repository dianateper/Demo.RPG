using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.CameraLogic;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Core.States;
using Game.CodeBase.Level;
using Game.CodeBase.Level.ParticleSystem;
using Game.CodeBase.PlayerLogic;
using UnityEngine;

namespace Game.CodeBase.Core.ProjectStates
{
    public class GameLoopState : IGamePlayState<PayloadData>
    {
        private readonly IPayloadDataStateSwitcher _payloadStateSwitcher;
        private readonly IStateSwitcher _stateSwitcher;
        private IPlayerInput _inputService;
        private IPlayer _player;
      
        private LevelData _levelData;
        private List<WorldItem> _items;
        private CameraRaycaster _raycaster;
        private IUpdateableHandler _updateableHandler;
        private PayloadData _payloadData;
        private ParticleFactory _particleFactory;
     
        public GameLoopState(IPayloadDataStateSwitcher payloadStateSwitcher, IStateSwitcher stateSwitcher)
        {
            _payloadStateSwitcher = payloadStateSwitcher;
            _stateSwitcher = stateSwitcher;
        }
        
        public void Enter(PayloadData payload)
        {
            _particleFactory = ServiceLocator.ResolveService<ParticleFactory>();
            _payloadData = payload;
            _player = payload.Player;
            _player.PlayerHealth.OnDie += EnterGameOverState;
            _inputService = ServiceLocator.ResolveService<IPlayerInput>();
            _inputService.OnShowInventory += LoadInventoryState;
            _inputService.IsEnabled = true;
            SetupWorldItems();
        }

        public void Exit()
        {
            _player.PlayerHealth.OnDie -= EnterGameOverState;
            _inputService.OnShowInventory -= LoadInventoryState;
            foreach (var item in _items)
                item.OnWorldItemIteract -= ShowItemDescription;
            _inputService.IsEnabled = false;
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
        
        private void CreateHitParticle(Vector3 at) => 
            _particleFactory.CreateParticle(ParticleId.Hit, at, true);
    }
}