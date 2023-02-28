using System;
using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.CameraLogic;
using Game.CodeBase.Common;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Core.States;
using Game.CodeBase.Inventory;
using Game.CodeBase.PlayerLogic.PlayerData;
using Game.CodeBase.PlayerLogic.PlayerStates;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    [RequireComponent(typeof(PlayerMove))]
    [RequireComponent(typeof(PlayerTrigger))]
    public class Player : MonoBehaviour, IPlayer, IStateSwitcher
    {
        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private PlayerTrigger _playerTrigger;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private PlayerWeaponRig _playerWeaponRig;
        [SerializeField] private PlayerWeaponTrigger _weaponTrigger;

        private IPlayerProgress _progress;
        private IHealth _playerHealth;
        private IInventory _inventory;
        private IPlayerInput _inputService;
        private ICameraRaycaster _cameraRaycaster;

        public IPlayerProgress Progress => _progress;
        public IHealth PlayerHealth => _playerHealth;
        public Transform Transform => transform;
        public event Action<Vector3> OnDamageHit;

        private List<IState> _states;
        private IState _currentState;
        private PlayerMoveSettings _playerMoveSettings;

        public void Construct(PlayerMoveSettings moveSettings, HealthSettings playerHealthSettings,
            IPlayerInput inputService, ICameraRaycaster cameraRaycaster)
        {
            _playerHealth = GetComponent<IHealth>();
            _progress = new PlayerProgress();
            _playerMoveSettings = moveSettings;
            
            _states = new List<IState>
            {
                new PlayerAttackState(_playerAnimator, this, this, moveSettings.AttackDelay),
                new PlayerDieState(this),
                new PlayerHitState(_playerHealth, Progress, this),
                new PlayerIdleState()
            };

            _playerAnimator.Construct();
            _playerMove.Construct(moveSettings, _playerAnimator);
            _progress.HealthData.CurrentHealth = _playerHealth.Current;
            _progress.KillData.EnemiesKilled = 0;
            _inputService = inputService;
            _cameraRaycaster = cameraRaycaster;
            _playerHealth.Current = playerHealthSettings.MaxHealth;
            _playerHealth.OnDie += SwitchState<PlayerDieState>;
            _playerTrigger.OnPlayerHit += _ => SwitchState<PlayerHitState>();

            _weaponTrigger.OnDamageHit += HitDamage;
            EnableInput();
            SwitchState<PlayerIdleState>();
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= SwitchState<PlayerDieState>;
            _playerTrigger.OnPlayerHit -= _ => SwitchState<PlayerHitState>();
            _weaponTrigger.OnDamageHit -= HitDamage;
            DisableInput();
        }

        public void SwitchState<T>() where T : class, IState
        {
            _currentState?.Exit();
            _currentState = _states.FirstOrDefault(s => s.GetType() == typeof(T));
            _currentState?.Enter();
        }

        public void OnUpdate(float deltaTime) => _playerWeaponRig.OnUpdate();

        public void Kill() => Progress.KillData.EnemiesKilled++;

        public void ApplyInventoryItem(ItemType itemId)
        {
        }

        private void HitDamage(IDamageable damageable, Vector3 position)
        {
            if (_currentState.GetType() == typeof(PlayerAttackState))
            {
                OnDamageHit?.Invoke(position);
                damageable.TakeDamage(_playerMoveSettings.Damage);
            }
        }
        
        private void EnableInput()
        {
            _inputService.OnMove += _playerMove.Move;
            _inputService.OnAttack += SwitchState<PlayerAttackState>;
            _cameraRaycaster?.Initialize();
        }

        private void DisableInput()
        {
            _inputService.OnMove -= _playerMove.Move;
            _inputService.OnAttack -=  SwitchState<PlayerAttackState>;
            _cameraRaycaster?.DeInitialize();
        }
    }
}