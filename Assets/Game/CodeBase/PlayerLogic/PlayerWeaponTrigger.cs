using System;
using Game.CodeBase.Common;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public class PlayerWeaponTrigger : MonoBehaviour
    {
        [SerializeField] private float _damage = 1;

        public event Action<Vector3> OnDamageHit;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealth damageable))
            {
                damageable.TakeDamage(_damage);
                OnDamageHit?.Invoke(transform.position);
            }
        }
    }
}
