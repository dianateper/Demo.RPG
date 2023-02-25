using System;
using Game.CodeBase.PlayerLogic;

namespace Game.CodeBase.Common
{
    public interface IHealth
    {
        event Action HealthChanged;
        float Current { get; set; }
        void TakeDamage(float damageTaken);
        void Construct(IHealthSettings playerHealthSettings);
    }
}