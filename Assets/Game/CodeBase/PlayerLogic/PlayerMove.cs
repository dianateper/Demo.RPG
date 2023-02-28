using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private LayerMask _walkable;
        private CharacterController _character;
        private PlayerAnimator _playerAnimator;
        private float _speed;
        private Vector3 _previousPosition;
        private Vector3 _direction;
        private Vector3 _velocity;
        private Collider[] _walkableColliders;
        private const float GroundOffset = 0.2f;
        private const int MaxColliders = 1;

        public void Construct(PlayerSettings settings, PlayerAnimator playerAnimator)
        {
            _walkableColliders = new Collider[MaxColliders];
            _character = GetComponent<CharacterController>();
            _speed = settings.Speed;
            _playerAnimator = playerAnimator;
        }

        public void Move(Vector3 direction)
        {
            if (IsGrounded) _velocity.y = 0f;

            _direction = direction;
            _character.Move(_direction * (_speed * Time.deltaTime));
            _playerAnimator.SetVelocity(_character.velocity);
            
            _velocity.y +=  Physics.gravity.y * Time.deltaTime;
            _character.Move(_velocity * Time.deltaTime);
        }

        private bool IsGrounded =>
            Physics.OverlapSphereNonAlloc(transform.position, _character.height / 2 + GroundOffset, _walkableColliders, _walkable) > 0;
    }
}
