using Game.CodeBase.Core.Updates;
using UnityEngine;

namespace Game.CodeBase.CameraLogic
{
    public class Follower : MonoBehaviour, ILateUpdateable
    {
        [SerializeField] private CameraSettings _cameraSettings;
        private Transform _target;
        private Vector3 _targetPosition;

        public void Follow(Transform target)
        {
            _target = target;
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (_target == null)
                return;

            _targetPosition = _target.position + new Vector3(0, _cameraSettings.OffsetY, -_cameraSettings.Distance);
            transform.position = Vector3.Lerp(transform.position, _targetPosition, deltaTime * _cameraSettings.Speed);

            transform.rotation =
                Quaternion.Euler(_cameraSettings.RotationX, 0, 0);
        }
    }
}