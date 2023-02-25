using System;

namespace Game.CodeBase.Common
{
    public interface IHealth
    {
        event Action HealthChanged;
        float Current { get; set; }
        void TakeDamage(float damageTaken);
    }
}