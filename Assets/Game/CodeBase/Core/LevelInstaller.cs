using System;
using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.CameraLogic;
using Game.CodeBase.Core.Services.AssetProvider;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Core.Updates;
using Game.CodeBase.EnemyLogic;
using Game.CodeBase.Inventory;
using Game.CodeBase.Level;
using Game.CodeBase.PlayerLogic;
using Game.CodeBase.StaticData;
using Game.CodeBase.UI;
using Game.CodeBase.UI.Hud;
using Game.CodeBase.UI.Inventory;
using Game.CodeBase.UI.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.CodeBase.Core
{
    public class LevelInstaller 
    {
        private readonly IUpdateableHandler _updateableHandler;
        private readonly PlayerFactory _playerFactory;
        private readonly UIFactory _uiFactory;
        private readonly LevelSettings _levelSettings;

        private IPlayer _player;
        private Camera _mainCamera;
        
        private IInventory _inventory;
        private InventoryFactory _inventoryFactory;
        private Hud _hud;
        private List<IEnemy> _enemies;
        private IInputService _inputService;
        private readonly LevelData _levelData;

        public LevelInstaller(IUpdateableHandler updateableHandler, LevelData levelData, LevelType levelType)
        {
            _updateableHandler = updateableHandler;

            _playerFactory = ServiceLocator.ResolveService<PlayerFactory>();
            _uiFactory = ServiceLocator.ResolveService<UIFactory>();
            _levelData = levelData;
            _levelSettings = _levelData.GetLevelSettings(levelType);
        }

        public void RegisterInput(IInputService inputService)
        {
            _inputService = inputService;
            _updateableHandler.AddUpdatable(inputService);
        }

        public IPlayer CreatePlayer(IInputService inputService, List<IEnemy> enemies, ICameraRaycaster raycaster)
        {
            var position = GameObject.FindGameObjectWithTag(Constants.PayerSpawnPointTag).transform.position;
            _player = _playerFactory.CreatePlayer(inputService, position, raycaster, enemies);
            _updateableHandler.AddUpdatable(_player);
            return _player;
        }

        public List<IEnemy> CreateEnemies(EnemyType enemyType, EnemyFactory enemyFactory)
        {
            var waypoints = Object.FindObjectsOfType<Waypoint>()
                .Select(x => x.transform).ToList();
            _enemies = enemyFactory.CreateEnemies(enemyType, _levelSettings, waypoints);
            _updateableHandler.AddUpdatables(_enemies.Select(x => x as IUpdateable).ToList());
            _enemies.ForEach(e => e.OnDie += _ => _enemies.Remove(e));
            return _enemies;
        }

        public void CreateHud(IPlayer player)
        {
            var hud = _uiFactory.CreateWindow(WindowId.HUD);
            _hud = hud.GetComponent<Hud>();
            _hud.Construct(player.Progress.HealthData, player.Progress.KillData);
        }

        public InventoryDataWindow CreateInventoryDataWindow(IInventory inventory)
        {
            var window = _uiFactory.CreateWindow(WindowId.Inventory) as InventoryDataWindow;
            window.Initialize();
            window.Construct(_levelData.GetItemsData(), inventory, _player);
            window.Hide();
            return window;
        }

        public Follower InitializeCamera(Transform player, Camera mainCamera)
        {
            var follower = mainCamera.GetComponent<Follower>();
            follower.Follow(player);
            _updateableHandler.AddLateUpdatable(follower);
            return follower;
        }

        public IInventory CreateInventory(InventoryFactory inventoryFactory)
        {
            _inventory = inventoryFactory.CreateInventory();
            return _inventory;
        }
    }
}