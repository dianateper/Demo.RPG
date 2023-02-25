using Game.CodeBase.Core.States;
using UnityEngine;

namespace Game.CodeBase.EnemyLogic.States
{
    public interface IEnemyState : IUpdateableState
    {
        Transform Target { set; }
        
        public void SetTarget(Transform target)
        {
            Target = target;
        }
    }
}