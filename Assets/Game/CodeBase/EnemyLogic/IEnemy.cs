using System;
using Game.CodeBase.Common;
using Game.CodeBase.Core.Updates;
using UnityEngine;

namespace Game.CodeBase.EnemyLogic
{
    public interface IEnemy : IUpdateable, IDamager
    {
        event Action<IEnemy> OnDie;
        void SetTarget(Transform target);
    }
}