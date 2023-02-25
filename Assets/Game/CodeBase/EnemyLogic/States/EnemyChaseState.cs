using Game.CodeBase.Core.States;
using UnityEngine;
using UnityEngine.AI;

namespace Game.CodeBase.EnemyLogic.States
{
    public class EnemyChaseState : IEnemyState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly NavMeshAgent _agent;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly float _chaseDistance;
        private readonly float _attackDistance;
        public Transform Target { get; set; }

        public EnemyChaseState(IStateSwitcher stateSwitcher, NavMeshAgent agent,
            EnemyAnimator enemyAnimator,
            EnemyAgentConfiguration agentConfiguration) 
        {
            _stateSwitcher = stateSwitcher;
            _agent = agent;
            _enemyAnimator = enemyAnimator;
            _chaseDistance = agentConfiguration.ChaseDistance;
            _attackDistance = _agent.radius + 0.3f;
        }

        public void Enter()
        {
           
        }

        public void Exit()
        {
            ResetAgentTarget();
        }

        public void OnUpdate(float deltaTime)
        {
            _enemyAnimator.SetVelocity(1);
            CheckTargetDistance();
        }

        private void CheckTargetDistance()
        {
            if (Target == null) return;
            
            var targetDistance = Target.position - _agent.transform.position;
            if (targetDistance.sqrMagnitude >= _chaseDistance * _chaseDistance)
            {
                _stateSwitcher.SwitchState<EnemyIdleState>();
            }
            else if(targetDistance.sqrMagnitude >= _attackDistance * _attackDistance)
            {
                _agent.SetDestination(Target.position);
            }
            else
            {
                _stateSwitcher.SwitchState<EnemyAttackState>();
            }
        }

        private void ResetAgentTarget()
        {
            _agent.SetDestination(_agent.transform.position);
        }
    }
}