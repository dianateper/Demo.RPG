using Game.CodeBase.Common;
using Game.CodeBase.Core.States;
using Game.CodeBase.PlayerLogic.PlayerData;

namespace Game.CodeBase.PlayerLogic.PlayerStates
{
    public class PlayerHitState : IState
    {
        private readonly IHealth _playerHealth;
        private readonly IPlayerProgress _progress;
        private readonly IStateSwitcher _stateSwitcher;

        public PlayerHitState(IHealth playerHealth, IPlayerProgress progress, IStateSwitcher stateSwitcher)
        {
            _playerHealth = playerHealth;
            _progress = progress;
            _stateSwitcher = stateSwitcher;
        }
        
        public void Enter()
        {
            TakeDamage(1);
            UpdateHealthData();
            _stateSwitcher.SwitchState<PlayerIdleState>();
        }

        public void Exit()
        {
            
        }

        private void TakeDamage(float damage) => _playerHealth.TakeDamage(damage);
        private void UpdateHealthData() => 
            _progress.HealthData.CurrentHealth = _playerHealth.Current;

    }
}