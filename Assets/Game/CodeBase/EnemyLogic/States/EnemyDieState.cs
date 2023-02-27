using System.Collections;
using Game.CodeBase.Level.ParticleSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Game.CodeBase.EnemyLogic.States
{
    public class EnemyDieState : IEnemyState
    {
        private readonly NavMeshAgent _agent;
        private readonly ParticleFactory _particleFactory;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly WaitForSeconds _destroyDelay;
        public Transform Target { get; set; }

        public EnemyDieState(MonoBehaviour monoBehaviour, EnemyAnimator enemyAnimator, NavMeshAgent agent,
            ParticleFactory particleFactory, float destroyDelay)
        {
            _agent = agent;
            _particleFactory = particleFactory;
            _monoBehaviour = monoBehaviour;
            _enemyAnimator = enemyAnimator;
            _destroyDelay = new WaitForSeconds(destroyDelay);
        }

        public void Enter()
        {
            _enemyAnimator.Die();
            _monoBehaviour.StartCoroutine(DestroyEnemy());
        }

        public void Exit()
        {
           
        }

        public void OnUpdate(float deltaTime)
        {
            
        }

        private IEnumerator DestroyEnemy()
        {
            yield return _destroyDelay;
            _particleFactory.CreateParticle(ParticleId.Die, _agent.transform.position);
            Object.Destroy(_agent.gameObject);
        }
    }
}