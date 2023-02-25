using System;
using UnityEngine;

namespace Game.CodeBase.EnemyLogic
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int DieHash= Animator.StringToHash("Die");
        private static readonly int AttackHash = Animator.StringToHash("Attack");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetVelocity(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void Die()
        {
            _animator.SetTrigger(DieHash);
        }

        public void Attack()
        {
            _animator.SetTrigger(AttackHash);
        }
    }
}