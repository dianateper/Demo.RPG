using System;
using Game.CodeBase.Common;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public class PlayerWeaponTrigger : MonoBehaviour
    {
        public event Action<IDamageable, Vector3> OnDamageHit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                OnDamageHit?.Invoke(damageable, transform.position);
            }
        }
    }
}
