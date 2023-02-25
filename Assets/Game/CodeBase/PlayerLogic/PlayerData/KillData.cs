using System;

namespace Game.CodeBase.PlayerLogic.PlayerData
{
    public class KillData
    {
        private float _enemiesKilled;
        public float EnemiesKilled
        {
            get => _enemiesKilled;
            set
            {
                _enemiesKilled = value;
                OnKillChange?.Invoke();
            }
        }
        
        public event Action OnKillChange;
    }
}