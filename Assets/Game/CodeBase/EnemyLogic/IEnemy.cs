using System;
using Game.CodeBase.Core.Updates;
using UnityEngine;

namespace Game.CodeBase.EnemyLogic
{
    public interface IEnemy : IUpdateable
    {
        event Action<IEnemy> OnDie;
        void SetTarget(Transform target);
    }
}