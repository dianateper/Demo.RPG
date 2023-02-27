using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Game.CodeBase.EnemyLogic.States
{
    public class EnemyDieState : IEnemyState
    {
        private readonly NavMeshAgent _agent;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly WaitForSeconds _destroyDelay;
        public Transform Target { get; set; }


        public EnemyDieState(MonoBehaviour monoBehaviour, EnemyAnimator enemyAnimator, NavMeshAgent agent, float destroyDelay)
        {
            _agent = agent;
            _monoBehaviour = monoBehaviour;
            _enemyAnimator = enemyAnimator;
            _destroyDelay =  new WaitForSeconds(destroyDelay);
        }

        public void Enter()
        {
            _enemyAnimator.Die();
            _monoBehaviour.StartCoroutine(DestroyEnemy());
        }

        private IEnumerator DestroyEnemy()
        {
            yield return _destroyDelay;
            Object.Destroy(_agent.gameObject);
        }

        public void Exit()
        {
           
        }

        public void OnUpdate(float deltaTime)
        {
            
        }
    }
}