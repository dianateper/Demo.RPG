using System;
using UnityEngine;

namespace Game.CodeBase.Core.Services.InputService
{
    public class PlayerInput : IPlayerInput
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public bool IsEnabled { get; set; }
        public event Action OnShowInventory;
        public event Action<Vector2> OnScreenClick;
        public event Action OnAttack;
        public event Action <Vector3> OnMove;

        public void OnUpdate(float deltaTime)
        {
            if (IsEnabled == false) return;

            if (Input.GetMouseButtonDown(0)) 
                OnScreenClick?.Invoke(Input.mousePosition);
            
            if (Input.GetKeyDown(KeyCode.E)) 
                OnAttack?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.Q)) 
                OnShowInventory?.Invoke();

            var moveDirection = new Vector3(Input.GetAxisRaw(Horizontal), 0, Input.GetAxisRaw(Vertical));
            OnMove?.Invoke(moveDirection);
        }
    }
}