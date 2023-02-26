using Game.CodeBase.Common;
using Game.CodeBase.Core.Services.InputService;
using UnityEngine;

namespace Game.CodeBase.CameraLogic
{
    public interface ICameraRaycaster
    {
        void DeInitialize();
        void Initialize();
    }

    public class CameraRaycaster : ICameraRaycaster
    {
        private readonly IInputService _inputService;
        private readonly Camera _camera;

        public CameraRaycaster(Camera camera, IInputService inputService)
        {
            _inputService = inputService;
            _camera = camera;
        }

        public void Raycast(Vector2 screenPosition)
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
        public void Initialize() => _inputService.OnScreenClick += Raycast;
    }
}