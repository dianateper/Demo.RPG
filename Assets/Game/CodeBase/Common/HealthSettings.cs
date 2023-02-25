using System;
using Game.CodeBase.PlayerLogic;
using UnityEngine;

namespace Game.CodeBase.Common
{
    [Serializable]
    public class HealthSettings : IHealthSettings
    {
        [SerializeField] private float _maxHealth;

        public float MaxHealth => _maxHealth;
    }
}