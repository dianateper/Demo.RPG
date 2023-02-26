using System;
using UnityEngine;

namespace Game.CodeBase.Core.Services.InputService
{
    public class InventoryInput : IInventoryInput
    {
        public bool IsEnabled { get; set; }
        public event Action OnSpacePress;
        public event Action OnRightArrowPress;
        public event Action OnLeftArrowPress;
        public event Action OnHideInventory;

        public void OnUpdate(float deltaTime)
        {
            if (IsEnabled == false) return;
            
            if (Input.GetKeyDown(KeyCode.Q)) 
                OnHideInventory?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.Space)) 
                OnSpacePress?.Invoke();

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) 
                OnLeftArrowPress?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) 
                OnRightArrowPress?.Invoke();
        }
    }
}