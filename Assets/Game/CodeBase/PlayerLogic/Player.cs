using System;
using Game.CodeBase.CameraLogic;
using Game.CodeBase.Common;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Inventory;
using Game.CodeBase.PlayerLogic.PlayerData;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    [RequireComponent(typeof(PlayerMove))]
    [RequireComponent(typeof(PlayerTrigger))]
    public class Player : MonoBehaviour, IPlayer
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
        public Transform Transform => transform;
        public event Action OnDie;
        public event Action<Vector3> OnDamageHit;

        public void Construct(PlayerMoveSettings moveSettings, HealthSettings playerHealthSettings,
            IPlayerInput inputService, ICameraRaycaster cameraRaycaster)
        {
            _playerHealth = GetComponent<IHealth>();
            _playerAnimator.Construct();
            _playerMove.Construct(moveSettings, _playerAnimator);
            _playerHealth.Current = playerHealthSettings.MaxHealth;
            _progress = new PlayerProgress();
            _progress.HealthData.CurrentHealth = _playerHealth.Current;
            _progress.KillData.EnemiesKilled = 0;
            _inputService = inputService;
            _cameraRaycaster = cameraRaycaster;
            _playerHealth.HealthChanged += CheckForDie;
            _playerHealth.HealthChanged += UpdateHealthData;
            _playerTrigger.OnPlayerHit += TakeDamage;
            _weaponTrigger.OnDamageHit += pos =>  OnDamageHit?.Invoke(pos);
            EnableInput();
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= CheckForDie;
            _playerTrigger.OnPlayerHit -= TakeDamage;
            _playerHealth.HealthChanged -= UpdateHealthData;
            _weaponTrigger.OnDamageHit -= pos =>  OnDamageHit?.Invoke(pos);
            DisableInput();
        }

        private void UpdateHealthData() => 
            Progress.HealthData.CurrentHealth = _playerHealth.Current;

        private void TakeDamage(float damage) => _playerHealth.TakeDamage(damage);

        private void CheckForDie()
        {
            if (_playerHealth.Current <= 0) 
                DestroyPlayer();
        }

        private void DestroyPlayer()
        {
            OnDie?.Invoke();
            Destroy(gameObject);
        }

        public void OnUpdate(float deltaTime) => _playerWeaponRig.OnUpdate();

        public void Kill() => Progress.KillData.EnemiesKilled++;

        public void ApplyInventoryItem(ItemType itemId)
        {
        }

        private void EnableInput()
        {
            _inputService.OnMove += _playerMove.Move;
            _inputService.OnAttack += _playerAnimator.SetAttackTrigger;
            _cameraRaycaster?.Initialize();
        }

        private void DisableInput()
        {
            _inputService.OnMove -= _playerMove.Move;
            _inputService.OnAttack -= _playerAnimator.SetAttackTrigger;
            _cameraRaycaster?.DeInitialize();
        }
    }
}