using System.Collections.Generic;
using Game.CodeBase.CameraLogic;
using Game.CodeBase.Core.Services.AssetProvider;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Core.States;
using Game.CodeBase.EnemyLogic;
using Game.CodeBase.Inventory;
using Game.CodeBase.Level;
using Game.CodeBase.Level.ParticleSystem;
using Game.CodeBase.PlayerLogic;
using Game.CodeBase.UI;
using Game.CodeBase.UI.Hud;
using Game.CodeBase.UI.Inventory;
using UnityEngine;

namespace Game.CodeBase.Core.ProjectStates
{
    public class LoadLevelState : IState
    {
        private readonly IPayloadDataStateSwitcher _stateSwitcher;
        private List<IEnemy> _enemies;
        
        private LevelInstaller _levelInstaller;
        private IPlayer _player;
        private Hud _hud;
        private IUpdateableHandler _updateableHandler;
        private UIFactory _uiFactory;
        private IPlayerInput _playerInput;
        private Camera _mainCamera;
        private IInventory _inventory;
        private InventoryDataWindow _inventoryWindow;
        private LevelData _levelData;
        private IAssetProvider _assetProvider;
        private ICameraRaycaster _raycaster;
        private IInventoryInput _inventoryInput;
        private ParticleFactory _particleFactory;

        public LoadLevelState(IPayloadDataStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
            ResolveServices();

            CreateUpdateableHandler();
            LoadLevelData();
            
            CreateLevelInstaller();
            
            RegisterInputs();
            
            CreateInventory();
            CreateEnemies();
            CreatePlayer();
            RegisterEnemies();
            
            CreateUI();
            RegisterCamera();
            EnterGameLoopState();
        }

        public void Exit()
        {
           
        }

        private void CreateLevelInstaller()
        {
            _levelInstaller = new LevelInstaller(_updateableHandler, _levelData, LevelType.Easy);
        }

        private void ResolveServices()
        {
            _playerInput = ServiceLocator.ResolveService<IPlayerInput>();
            _inventoryInput = ServiceLocator.ResolveService<IInventoryInput>();
            _assetProvider = ServiceLocator.ResolveService<IAssetProvider>();
        }

        private void RegisterInputs()
        {
            _levelInstaller.RegisterInputs(_playerInput, _inventoryInput);
            _raycaster = new CameraRaycaster(Camera.main, _playerInput);
        }

        private void LoadLevelData()
        {
            _levelData = _assetProvider.LoadAsset<LevelData>(Constants.LevelDataPath);
        }

        private void CreateInventory()
        {
            _inventory = _levelInstaller.CreateInventory(ServiceLocator.ResolveService<InventoryFactory>());
        }

        private void RegisterCamera()
        {
            _mainCamera = Camera.main;
            _levelInstaller.InitializeCamera(_player.Transform, _mainCamera);
        }

        private void CreateUpdateableHandler()
        {
            _updateableHandler = new GameObject("UpdateableHandler")
                .AddComponent<UpdateableHandler>();
        }

        private void CreateUI()
        {
            _levelInstaller.CreateHud(_player);
            _inventoryWindow = _levelInstaller.CreateInventoryDataWindow(_inventory);
        }

        private void RegisterEnemies() => 
            _enemies.ForEach(e => e.SetTarget(_player.Transform));

        private void CreateEnemies() =>
            _enemies = _levelInstaller.CreateEnemies(EnemyType.Base,
                ServiceLocator.ResolveService<EnemyFactory>());

        private void CreatePlayer()
        {
            _player = _levelInstaller.CreatePlayer(_playerInput, _enemies, _raycaster);
            _player.OnDie += () => _updateableHandler.RemoveFromUpdatable(_player);
        }

        private void EnterGameLoopState()
        {
            _stateSwitcher.SwitchState<GameLoopState>(new PayloadData()
            {
                Player = _player,
                Inventory = _inventory,
                InventoryDataWindow = _inventoryWindow,
                LevelData = _levelData
            });
        }
    }
}