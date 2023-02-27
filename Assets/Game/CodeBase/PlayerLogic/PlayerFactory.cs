using System;
using System.Collections.Generic;
using Game.CodeBase.CameraLogic;
using Game.CodeBase.Common;
using Game.CodeBase.Core.Services;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.EnemyLogic;
using Unity.Mathematics;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    [CreateAssetMenu(fileName = "PlayerFactory", menuName = "RPG/PlayerFactory")]
    public class PlayerFactory : ScriptableObject, IService
    {
        [SerializeField] private Player _prefab;
        [SerializeField] private PlayerMoveSettings _moveSettings;
        [SerializeField] private PlayerSpawnSettings _spawnSettings;
        [SerializeField] private HealthSettings _healthSettings;
        [SerializeField] private GameObject _spawnRing;
       
        public IPlayer CreatePlayer(IPlayerInput inputService, Vector3 at,
            ICameraRaycaster cameraRaycaster = null,
            List<IEnemy> enemies = null)
        {
            at += Vector3.up * _spawnSettings.OffsetY;
            var player = Instantiate(_prefab, at, quaternion.identity);
            player.Construct(_moveSettings,_healthSettings, inputService, cameraRaycaster);
            if (enemies != null)
                foreach (var enemy in enemies)
                    enemy.OnDie += _ => player.Kill();

            return player;
        }
    }

    [Serializable]
    public class PlayerMoveSettings
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;
    }
    
    [Serializable]
    public class PlayerSpawnSettings
    {
        [SerializeField] private float _offsetY;

        public float OffsetY => _offsetY;
    }
}