using System;
using UnityEngine;

namespace Game.CodeBase.Core.Services.InputService
{
    public interface IPlayerInput : IInputService
    {
        event Action<Vector2> OnScreenClick;
        event Action OnAttack;
        event Action <Vector3> OnMove;
        event Action OnShowInventory;
    }
}