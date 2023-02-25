using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int VelocityX = Animator.StringToHash("VelocityX");
        private static readonly int VelocityY = Animator.StringToHash("VelocityY");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int PlayerHit = Animator.StringToHash("PlayerHit");

        public void Construct()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetVelocity(Vector3 movement)
        {
            float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
            float velocityX = Vector3.Dot(movement.normalized, transform.right);
            
            _animator.SetFloat(VelocityX, velocityX, 0.1f, Time.deltaTime);
            _animator.SetFloat(VelocityY,velocityZ, 0.1f, Time.deltaTime);
        }

        public void SetAttackTrigger() => _animator.SetTrigger(Attack);

        public void SetPlayerHitTrigger() => _animator.SetTrigger(PlayerHit);
    }
}