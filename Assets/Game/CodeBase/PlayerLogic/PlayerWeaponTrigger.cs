using Game.CodeBase.Common;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public class PlayerWeaponTrigger : MonoBehaviour
    {
        [SerializeField] private float _damage = 1;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealth damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}
