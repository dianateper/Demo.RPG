using System;
using Game.CodeBase.EnemyLogic;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public class PlayerTrigger : MonoBehaviour
    {
        public event Action<float> OnPlayerHit;
     
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IEnemy _))
            {
                OnPlayerHit?.Invoke(1);
            }
        }
    }
}
