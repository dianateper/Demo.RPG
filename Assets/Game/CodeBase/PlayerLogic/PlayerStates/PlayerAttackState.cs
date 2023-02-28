using System.Collections;
using Game.CodeBase.Core.States;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic.PlayerStates
{
    public class PlayerAttackState : IState
    {
        private readonly PlayerAnimator _playerAnimator;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly WaitForSeconds _attackDelay; 

        public PlayerAttackState(PlayerAnimator playerAnimator, MonoBehaviour monoBehaviour, IStateSwitcher stateSwitcher, float attackDelay)
        {
            _playerAnimator = playerAnimator;
            _monoBehaviour = monoBehaviour;
            _stateSwitcher = stateSwitcher;
            _attackDelay = new WaitForSeconds(attackDelay);
        }

        public void Enter()
        {
            _playerAnimator.SetAttackTrigger();
            _monoBehaviour.StartCoroutine(EnterIdleState());
        }

        private IEnumerator EnterIdleState()
        {
            yield return _attackDelay;
            _stateSwitcher.SwitchState<PlayerIdleState>();
        }

        public void Exit()
        {
            
        }
    }
}