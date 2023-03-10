using System;
using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.Common;
using Game.CodeBase.Core;
using Game.CodeBase.Core.States;
using Game.CodeBase.EnemyLogic.States;
using Game.CodeBase.Level.ParticleSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Game.CodeBase.EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour, IStateSwitcher, IEnemy
    {
        private List<IEnemyState> _enemyStates;
        private List<Transform> _waypoints;
        private EnemyAgentConfiguration _agentConfiguration;
        private IHealth _enemyHealth;
        private IUpdateableState _currentState;
        private Transform _target;
        private NavMeshAgent _agent;
        private EnemyAnimator _enemyAnimator;
        private ParticleFactory _particleFactory;
        private float _currentHealth;
        private float _maxHealth;
        private bool _died;
        private float _damage;

        public event Action<IEnemy> OnDie;
        public float Damage => _damage;

        public void Construct(EnemyAgentConfiguration agentConfiguration,
            HealthSettings enemyDataHealthSettings,
            List<Transform> waypoints)
        {
            _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
            _agent = GetComponent<NavMeshAgent>();
            _enemyHealth = GetComponent<IHealth>();
            _waypoints = waypoints;
            _agentConfiguration = agentConfiguration;
            _particleFactory = ServiceLocator.ResolveService<ParticleFactory>();
            _damage = agentConfiguration.Damage;
            _enemyStates = new List<IEnemyState>
            {
                new EnemyIdleState(this, _agentConfiguration.IdleTime, _enemyAnimator),
                new EnemyPatrolState(this, _agent, _enemyAnimator, _waypoints, _agentConfiguration),
                new EnemyChaseState(this, _agent, _enemyAnimator, _agentConfiguration),
                new EnemyAttackState(this, _agent, _enemyAnimator),
                new EnemyDieState(this, _enemyAnimator, _agent, _particleFactory, agentConfiguration.DestroyDelay)
            };

            _enemyHealth.Construct(enemyDataHealthSettings);
            _enemyHealth.OnDie += Die;
            SwitchState<EnemyIdleState>();
        }

        public void OnUpdate(float deltaTime) => _currentState.OnUpdate(deltaTime);

        public void SetTarget(Transform target)
        {
            foreach (var state in _enemyStates) 
                state.SetTarget(target);
        }

        public void SwitchState<T>() where T : class, IState
        {
            _currentState?.Exit();
            _currentState = _enemyStates.FirstOrDefault(s => s.GetType() == typeof(T));
            _currentState?.Enter();
        }

        private void Die()
        {
            _enemyHealth.OnDie -= Die;
            SwitchState<EnemyDieState>();
            OnDie?.Invoke(this);
        }
    }
}
