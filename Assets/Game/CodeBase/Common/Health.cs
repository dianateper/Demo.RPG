using System;
using Game.CodeBase.PlayerLogic;
using UnityEngine;

namespace Game.CodeBase.Common
{
    public class Health : MonoBehaviour, IHealth
    {
        private float _currentHealth;
        private float _maxHealth;

        public Health(float current)
        {
            Current = current;
            HealthChanged += CheckForDie;
        }

        public event Action OnDie;

        public float Current
        {
            get => _currentHealth;
            set
            {
                if (value <= 0)
                    value = 0;
                
                _currentHealth = value;
            }
        }

        public event Action HealthChanged;

        public void TakeDamage(float damageTaken)
        {
            Current -= damageTaken;
            HealthChanged?.Invoke();
        }

        public void Construct(IHealthSettings healthSettings)
        {
            _maxHealth = healthSettings.MaxHealth;
            _currentHealth = _maxHealth;
        }
        
        private void CheckForDie()
        {
            if (_currentHealth <= 0) 
                OnDie?.Invoke();
        }
    }
}