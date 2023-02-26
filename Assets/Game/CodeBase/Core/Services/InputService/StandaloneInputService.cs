using System;
using UnityEngine;

namespace Game.CodeBase.Core.Services.InputService
{
    public class StandaloneInputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public event Action ToggleInventory;
        public event Action<Vector2> OnScreenClick;
        public event Action OnAttack;
        public event Action <Vector3> OnMove;
        

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q)) 
                ToggleInventory?.Invoke();

            if (Input.GetMouseButtonDown(0)) 
                OnScreenClick?.Invoke(Input.mousePosition);
            
            if (Input.GetKeyDown(KeyCode.E)) 
                OnAttack?.Invoke();

            var moveDirection = new Vector3(Input.GetAxisRaw(Horizontal), 0, Input.GetAxisRaw(Vertical));
            OnMove?.Invoke(moveDirection);
        }
    }
}