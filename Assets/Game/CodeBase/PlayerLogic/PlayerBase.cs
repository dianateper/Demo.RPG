using System;
using Game.CodeBase.Common;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Core.Updates;
using Game.CodeBase.Inventory;
using Game.CodeBase.PlayerLogic.PlayerData;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    [RequireComponent(typeof(PlayerMove))]
    [RequireComponent(typeof(PlayerTrigger))]
    public class PlayerBase : MonoBehaviour, IUpdateable
    {
        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private PlayerTrigger _playerTrigger;
        [SerializeField] private PlayerAnimator _playerAnimator;
        private IHealth _playerHealth;
        private IInventory _inventory;
        public IPlayerProgress Progress;
        private IInputService _inputService;
        public event Action OnDie;
   
        public void Construct(PlayerMoveSettings moveSettings, HealthSettings playerHealthSettings,
            IInputService inputService)
        {
            _playerHealth = GetComponent<IHealth>();
            _playerMove.Construct(moveSettings, _playerAnimator);
            _playerHealth.Current = playerHealthSettings.MaxHealth;
            Progress = new PlayerProgress();
            Progress.HealthData.CurrentHealth = _playerHealth.Current;
            Progress.KillData.EnemiesKilled = 0;
            _inputService = inputService;
            _playerHealth.HealthChanged += CheckForDie;
            _playerHealth.HealthChanged += UpdateHealthData;
            _playerTrigger.OnPlayerHit += TakeDamage;
        }

        private void UpdateHealthData() => 
            Progress.HealthData.CurrentHealth = _playerHealth.Current;

        private void TakeDamage(float damage) => 
            _playerHealth.TakeDamage(damage);

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= CheckForDie;
            _playerTrigger.OnPlayerHit -= TakeDamage;
            _playerHealth.HealthChanged -= UpdateHealthData;
        }

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

        public void OnUpdate(float deltaTime) => 
            _playerMove.Move(_inputService.GetMoveInput());

        public void Kill()
        {
            Progress.KillData.EnemiesKilled++;
        }

        public void ApplyInventoryItem(ItemType itemId)
        {
        }
    }
}