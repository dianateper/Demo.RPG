using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.CameraLogic;
using Game.CodeBase.Common;
using Game.CodeBase.Core;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Core.States;
using Game.CodeBase.Inventory;
using Game.CodeBase.Level.ParticleSystem;
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
        private List<IState> _states;
        private IState _currentState;
        private PlayerSettings _playerSettings;
        private ParticleFactory _particleFactory;

        public IPlayerProgress Progress => _progress;
        public IHealth PlayerHealth => _playerHealth;
        public Transform Transform => transform;
        
        public void Construct(PlayerSettings settings, HealthSettings playerHealthSettings,
            IPlayerInput inputService, ICameraRaycaster cameraRaycaster)
        {
            _particleFactory = ServiceLocator.ResolveService<ParticleFactory>();
            _playerHealth = GetComponent<IHealth>();
            _progress = new PlayerProgress();
            _playerSettings = settings;
            _inputService = inputService;
            _cameraRaycaster = cameraRaycaster;

            _states = new List<IState>
            {
                new PlayerAttackState(_playerAnimator, this, this, _playerSettings, _particleFactory, _weaponTrigger),
                new PlayerDieState(this),
                new PlayerHitState(_playerHealth, Progress, this),
                new PlayerIdleState()
            };

            _playerAnimator.Construct();
            _playerMove.Construct(settings, _playerAnimator);
            _progress.HealthData.CurrentHealth = _playerHealth.Current;
            _progress.KillData.EnemiesKilled = 0;
            _playerHealth.Current = playerHealthSettings.MaxHealth;
            
            _playerHealth.OnDie += SwitchState<PlayerDieState>;
            _playerTrigger.OnPlayerHit += _ => SwitchState<PlayerHitState>();
            
            EnableInput();
            SwitchState<PlayerIdleState>();
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= SwitchState<PlayerDieState>;
            _playerTrigger.OnPlayerHit -= _ => SwitchState<PlayerHitState>();
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