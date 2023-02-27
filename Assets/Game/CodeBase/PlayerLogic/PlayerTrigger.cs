using System;
using Game.CodeBase.EnemyLogic;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public class PlayerTrigger : MonoBehaviour
    {
        public event Action<float> OnPlayerHit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEnemy _))
            {
                OnPlayerHit?.Invoke(1);
            }
        }
    }
}
