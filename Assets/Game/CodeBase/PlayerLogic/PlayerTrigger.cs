using System;
using System.Collections;
using Game.CodeBase.EnemyLogic;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public class PlayerTrigger : MonoBehaviour
    {
        private float _hitDelay = 0.3f;
        private bool _isHit;
        public event Action<float> OnPlayerHit;

        private IEnumerator HitPlayerCoroutine()
        {
            OnPlayerHit?.Invoke(1);
            yield return new WaitForSeconds(_hitDelay);
            _isHit = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isHit == false && other.TryGetComponent(out IEnemy _))
            {
                _isHit = true;
                StartCoroutine(HitPlayerCoroutine());
            }
        }
    }
}
