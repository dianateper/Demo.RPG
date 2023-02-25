using Game.CodeBase.Common;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.Level;
using UnityEngine;

namespace Game.CodeBase.CameraLogic
{
    public class CameraRaycaster
    {
        private IInputService _inputService;
        private readonly Camera _camera;

        public CameraRaycaster(Camera camera, IInputService inputService)
        {
            _inputService = inputService;
            _inputService.OnScreenClick += Raycast;
            _camera = camera;
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

        public void DeInitialize() => _inputService.OnScreenClick -= Raycast;
    }
}