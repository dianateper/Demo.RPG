using System;

namespace Game.CodeBase.PlayerLogic.PlayerData
{
    public class HealthData
    {
        private float _currentHealth;
        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                OnHealthChange?.Invoke();
            }
        }

        public event Action OnHealthChange;
    }
}