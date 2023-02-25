using Game.CodeBase.Common;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public class PlayerWeaponTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealth damageable))
            {
                damageable.TakeDamage(1);
            }
        }
    }
}
