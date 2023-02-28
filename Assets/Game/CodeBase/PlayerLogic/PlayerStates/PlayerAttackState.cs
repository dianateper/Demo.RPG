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

        public PlayerAttackState(PlayerAnimator playerAnimator, MonoBehaviour monoBehaviour, IStateSwitcher stateSwitcher)
        {
            _playerAnimator = playerAnimator;
            _monoBehaviour = monoBehaviour;
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
            _playerAnimator.SetAttackTrigger();
            _monoBehaviour.StartCoroutine(EnterIdleState());
        }

        private IEnumerator EnterIdleState()
        {
            yield return new WaitForSeconds(3);
            _stateSwitcher.SwitchState<PlayerIdleState>();
        }

        public void Exit()
        {
            
        }
    }
}