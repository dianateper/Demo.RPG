using System.Collections.Generic;
using Game.CodeBase.Core.States;
using UnityEngine;
using UnityEngine.AI;

namespace Game.CodeBase.EnemyLogic.States
{
    public class EnemyPatrolState : IEnemyState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly List<Transform> _waypoints;
        private readonly NavMeshAgent _agent;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly float _stopDistance;
        private readonly float _chaseDistance;
        private int _currentWaypoint;
        public Transform Target { get; set; }

        public EnemyPatrolState(IStateSwitcher stateSwitcher, NavMeshAgent agent, EnemyAnimator enemyAnimator,
            List<Transform> waypoints,
            EnemyAgentConfiguration agentConfiguration)
        {
            _stateSwitcher = stateSwitcher;
            _waypoints = waypoints;
            _currentWaypoint = Random.Range(0, _waypoints.Count);
            _agent = agent;
            _enemyAnimator = enemyAnimator;
            _stopDistance = agentConfiguration.StopDistance;
            _chaseDistance = agentConfiguration.ChaseDistance;
        }

        public void Enter()
        {
            GotoNextPoint();
        }

        public void Exit()
        {
            
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_agent.pathPending && _agent.remainingDistance < _stopDistance)
                GotoNextPoint();

            CheckTargetDistance();
            _enemyAnimator.SetVelocity(1);
        }

        private void CheckTargetDistance()
        {
            if (Target == null) return;
            
            var targetDistance = Target.position - _agent.transform.position;
            if (targetDistance.sqrMagnitude <= _chaseDistance * _chaseDistance && FaceSameDirection)
            {
                _stateSwitcher.SwitchState<EnemyChaseState>();
            }
        }

        private bool FaceSameDirection => Vector3.Dot(_agent.transform.forward, Target.transform.forward) > 0;


        private void GotoNextPoint() {
            if (_waypoints.Count == 0)
                return;

            SetAgentDestination(_waypoints[_currentWaypoint].position);
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Count;
        }

        private void SetAgentDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }
    }
}