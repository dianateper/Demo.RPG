using System.Collections;
using Game.CodeBase.Common;
using Game.CodeBase.Core.States;
using Game.CodeBase.Level.ParticleSystem;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic.PlayerStates
{
    public class PlayerAttackState : IState
    {
        private readonly PlayerAnimator _playerAnimator;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly WaitForSeconds _attackDelay;
        private readonly PlayerWeaponTrigger _weaponTrigger;
        private readonly PlayerSettings _playerSettings;
        private readonly ParticleFactory _particleFactory;

        public PlayerAttackState(PlayerAnimator playerAnimator, MonoBehaviour monoBehaviour,
            IStateSwitcher stateSwitcher, PlayerSettings playerSettings, ParticleFactory particleFactory, PlayerWeaponTrigger playerWeaponTrigger)
        {
            _playerAnimator = playerAnimator;
            _monoBehaviour = monoBehaviour;
            _weaponTrigger = playerWeaponTrigger;
            _stateSwitcher = stateSwitcher;
            _playerSettings = playerSettings;
            _particleFactory = particleFactory;
            _attackDelay = new WaitForSeconds(_playerSettings.AttackDelay);
        }

        public void Enter()
        {
            _weaponTrigger.OnDamageHit += HitDamage;
            _playerAnimator.SetAttackTrigger();
            _monoBehaviour.StartCoroutine(EnterIdleState());
        }

        public void Exit()
        {
            _weaponTrigger.OnDamageHit -= HitDamage;
        }

        private IEnumerator EnterIdleState()
        {
            yield return _attackDelay;
            _stateSwitcher.SwitchState<PlayerIdleState>();
        }

        private void HitDamage(IDamageable damageable, Vector3 position)
        {
            _particleFactory.CreateParticle(ParticleId.Hit, position, true);
            damageable.TakeDamage(_playerSettings.Damage);
        }
    }
}