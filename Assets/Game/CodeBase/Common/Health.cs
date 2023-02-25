using System;
using Game.CodeBase.PlayerLogic;
using UnityEngine;

namespace Game.CodeBase.Common
{
    public class Health : MonoBehaviour, IHealth
    {
        private float _currentHealth;
        private float _maxHealth;

        public Health()
        {
        }

        public Health(float current)
        {
            Current = current;
        }

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

        public void Construct(HealthSettings playerHealthSettings)
        {
            _maxHealth = playerHealthSettings.MaxHealth;
            _currentHealth = _maxHealth;
        }
    }
}