using System;
using Game.CodeBase.Common;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public class PlayerTrigger : MonoBehaviour
    {
        public event Action<float> OnPlayerHit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamager damager))
            {
                OnPlayerHit?.Invoke(damager.Damage);
            }
        }
    }
}
