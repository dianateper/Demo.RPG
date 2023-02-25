using Game.CodeBase.Core.States;
using UnityEngine;
using UnityEngine.AI;

namespace Game.CodeBase.EnemyLogic.States
{
    public class EnemyAttackState : IEnemyState
    {
        private readonly NavMeshAgent _agent;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly float _attackDistance;
        private readonly IStateSwitcher _stateSwitcher;
        public Transform Target { get; set; }


        public EnemyAttackState(IStateSwitcher stateSwitcher,
            NavMeshAgent agent,
            EnemyAnimator enemyAnimator) 
        {
            _stateSwitcher = stateSwitcher;
            _agent = agent;
            _enemyAnimator = enemyAnimator;
            _attackDistance = agent.radius + 0.3f;
        }

        public void Enter()
        {
            _enemyAnimator.Attack();
        }

        public void Exit()
        {

        }

        public void OnUpdate(float deltaTime)
        {
            if(Target == null) return;
            var targetDistance = Target.position - _agent.transform.position;
            if (targetDistance.sqrMagnitude >= _attackDistance * _attackDistance)
            {
                _stateSwitcher.SwitchState<EnemyChaseState>();
            }
        }
    }
}