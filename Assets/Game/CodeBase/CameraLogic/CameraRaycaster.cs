using Game.CodeBase.Common;
using Game.CodeBase.Core.Services.InputService;
using UnityEngine;

namespace Game.CodeBase.CameraLogic
{
    public class CameraRaycaster : ICameraRaycaster
    {
        private readonly IPlayerInput _playerInput;
        private readonly Camera _camera;

        public CameraRaycaster(Camera camera, IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _camera = camera;
        }

        public void DeInitialize()
        {
            _playerInput.OnScreenClick -= Raycast;
        }

        public void Initialize()
        {
            _playerInput.OnScreenClick += Raycast;
        } 

        private void Raycast(Vector2 screenPosition)
        {
            if (_camera == null) return;
            var ray = _camera.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }
}