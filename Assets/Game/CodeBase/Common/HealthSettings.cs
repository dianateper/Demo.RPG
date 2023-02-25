using System;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    [Serializable]
    public class HealthSettings
    {
        [SerializeField] private float _maxHealth;

        public float MaxHealth => _maxHealth;
    }
}