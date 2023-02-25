using System;
using UnityEngine;

namespace Game.CodeBase.Core.Services.InputService
{
    public class StandaloneInputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector3 GetMoveInput() =>
            _isEnabled ? new(Input.GetAxisRaw(Horizontal), 0, Input.GetAxisRaw(Vertical)) : Vector3.zero;
        
        public event Action ToggleInventory;
        public event Action<Vector2> OnScreenClick;
        public event Action OnAttack;

        private bool _isEnabled;
       
        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q)) 
                ToggleInventory?.Invoke();
           
            if (_isEnabled == false) return;

            if (Input.GetMouseButtonDown(0)) 
                OnScreenClick?.Invoke(Input.mousePosition);
            
            if (Input.GetKeyDown(KeyCode.E)) 
                OnAttack?.Invoke();
        }
    }
}