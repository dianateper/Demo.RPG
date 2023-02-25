using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float _speed;
        private PlayerAnimator _playerAnimator;
        private Vector3 _previousPosition;
        private Vector3 _direction;

        public void Construct(PlayerMoveSettings moveSettings, PlayerAnimator playerAnimator)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _speed = moveSettings.Speed;
            _playerAnimator = playerAnimator;
        }

        public void Move(Vector3 direction)
        {
            _direction = direction;
            _rigidbody.transform.position += direction * (_speed * Time.deltaTime);
            var velocity = (_previousPosition - transform.position) / Time.deltaTime;
            _playerAnimator.SetVelocity(velocity);
            _previousPosition = transform.position;
        }
    }
}
