using System;
using System.Collections.Generic;
using Game.CodeBase.CameraLogic;
using Game.CodeBase.Common;
using Game.CodeBase.Core.Services;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.EnemyLogic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.CodeBase.PlayerLogic
{
    [CreateAssetMenu(fileName = "PlayerFactory", menuName = "RPG/PlayerFactory")]
    public class PlayerFactory : ScriptableObject, IService
    {
        [SerializeField] private Player _prefab;
        [FormerlySerializedAs("_moveSettings")] [SerializeField] private PlayerSettings settings;
        [SerializeField] private PlayerSpawnSettings _spawnSettings;
        [SerializeField] private HealthSettings _healthSettings;

        public IPlayer CreatePlayer(IPlayerInput inputService, Vector3 at,
            ICameraRaycaster cameraRaycaster = null,
            List<IEnemy> enemies = null)
        {
            at += Vector3.up * _spawnSettings.OffsetY;
            var player = Instantiate(_prefab, at, quaternion.identity);
            player.Construct(settings,_healthSettings, inputService, cameraRaycaster);
            if (enemies != null)
                foreach (var enemy in enemies)
                    enemy.OnDie += _ => player.Kill();

            return player;
        }
    }

    [Serializable]
    public class PlayerSettings
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _damage;

        public float Speed => _speed;
        public float AttackDelay => _attackDelay;
        public float Damage => _damage;
    }
    
    [Serializable]
    public class PlayerSpawnSettings
    {
        [SerializeField] private float _offsetY;

        public float OffsetY => _offsetY;
    }
}