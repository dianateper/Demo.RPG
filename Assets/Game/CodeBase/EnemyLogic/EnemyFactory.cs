using System;
using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.Core;
using Game.CodeBase.Core.Services;
using Game.CodeBase.Level;
using Game.CodeBase.PlayerLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.CodeBase.EnemyLogic
{
    [CreateAssetMenu(fileName = "EnemyFactory", menuName = "RPG/EnemyFactory")]
    public class EnemyFactory : ScriptableObject, IService
    {
        [SerializeField] private List<EnemySettings> enemySettings;

        public List<IEnemy> CreateEnemies(EnemyType enemyType, LevelSettings levelSettings,
            List<Transform> waypoints,
            Transform target = null)
        {
            var enemies = new List<IEnemy>();
            var enemyData = enemySettings.FirstOrDefault(e => e.EnemyType == enemyType);
            if (enemyData == null)
            {
                Debug.LogError("Cannot find an enemy with type of " + enemyType);
                return null;
            }

            for (int i = 0; i < levelSettings.NumberOfEnemies; i++)
            {
                var spawnPosition =
                    GetRandomPosition(levelSettings.SpawnOuterRadius, levelSettings.SpawnInnerRadius,
                        enemyData.OffsetY);
                var enemy = Instantiate(enemyData.EnemyPrefab, spawnPosition, Quaternion.identity);

                enemy.Construct(enemyData.AgentConfiguration, enemyData.HealthSettings, waypoints);
                enemies.Add(enemy);
            }
            

            return enemies;
        }

        private Vector3 GetRandomPosition(float outerRadius, float innerRadius, float offsetY)
        {
            var randomOnUnitSphere = Random.onUnitSphere;
            var outerPosition = randomOnUnitSphere * outerRadius;
            var innerPosition = randomOnUnitSphere * innerRadius;
            
            Vector3 spawnPosition = Vector3.zero;
            
            spawnPosition.y = offsetY;
            spawnPosition.z = Random.Range(innerPosition.z, outerPosition.z);
            spawnPosition.x = Random.Range(innerPosition.x, outerPosition.x);
            return spawnPosition;
        }
    }

    public enum EnemyType
    {
        Base
    }

    [Serializable]
    public class EnemySettings
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private EnemyAgentConfiguration _agentConfiguration;
        [SerializeField] private float _offsetY;
        [SerializeField] private HealthSettings _healthSettings;
        
        public EnemyType EnemyType => _enemyType;
        public HealthSettings HealthSettings => _healthSettings;
        public Enemy EnemyPrefab => _enemyPrefab;
        public EnemyAgentConfiguration AgentConfiguration => _agentConfiguration;
        public float OffsetY => _offsetY;
    }

    [Serializable]
    public class EnemyAgentConfiguration
    {
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _chaseDistance;
        [SerializeField] private float _idleTime;
        [SerializeField] private float _stopDistance;

        public float AttackDistance => _attackDistance;
        public float ChaseDistance => _chaseDistance;
        public float IdleTime => _idleTime;
        public float StopDistance => _stopDistance;
    }
}