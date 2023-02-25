using System;
using Game.CodeBase.Core.Updates;
using UnityEngine;

namespace Game.CodeBase.Core.Services.InputService
{
    public interface IInputService : IService, IUpdateable
    {
        Vector3 GetMoveInput();
        event Action ToggleInventory;
        event Action<Vector2> OnScreenClick;

        void Enable();
        void Disable();
    }
}