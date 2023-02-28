using System;
using Game.CodeBase.PlayerLogic;

namespace Game.CodeBase.Common
{
    public interface IHealth : IDamageable
    {
        event Action HealthChanged;
        event Action OnDie;
        float Current { get; set; }
        void Construct(IHealthSettings healthSettings);
    }
}