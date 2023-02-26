using System;

namespace Game.CodeBase.Core.Services.InputService
{
    public interface IInventoryInput : IInputService
    {
        event Action OnSpacePress;
        event Action OnRightArrowPress;
        event Action OnLeftArrowPress;
        event Action OnHideInventory;
    }
}