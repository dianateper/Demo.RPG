using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int VelocityX = Animator.StringToHash("VelocityX");
        private static readonly int VelocityY = Animator.StringToHash("VelocityY");
      
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetVelocity(Vector3 velocity)
        {
            _animator.SetFloat(VelocityX, velocity.x);
            _animator.SetFloat(VelocityY, velocity.z);
        }
    }
}