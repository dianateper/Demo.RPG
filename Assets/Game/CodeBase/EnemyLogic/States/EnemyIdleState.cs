using Game.CodeBase.Core.States;
using UnityEngine;

namespace Game.CodeBase.EnemyLogic.States
{
    public class EnemyIdleState : IEnemyState
    {
        private float _timeToMove;
        private readonly float _idleTime;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly IStateSwitcher _stateSwitcher;

        public Transform Target { get; set; }

        public EnemyIdleState(IStateSwitcher stateSwitcher, float idleTime, EnemyAnimator enemyAnimator)
        {
            _idleTime = idleTime;
            _enemyAnimator = enemyAnimator;
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
            _enemyAnimator.SetVelocity(0);
            _timeToMove = _idleTime;
        }

        public void Exit()
        {
            
        }

        public void OnUpdate(float deltaTime)
        {
            if (_timeToMove <= 0)
            {
                _stateSwitcher.SwitchState<EnemyPatrolState>();
            }
            _timeToMove -= deltaTime;
        }
    }
}