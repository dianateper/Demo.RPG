using System;
using Game.CodeBase.Core.Updates;
using UnityEngine;

namespace Game.CodeBase.Core.Services.InputService
{
    public interface IInputService : IService, IUpdateable
    {
        event Action ToggleInventory;
        event Action<Vector2> OnScreenClick;
        event Action OnAttack;
        event Action <Vector3> OnMove;
    }
}