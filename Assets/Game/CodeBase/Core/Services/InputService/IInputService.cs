using System;
using Game.CodeBase.Core.Updates;

namespace Game.CodeBase.Core.Services.InputService
{
    public interface IInputService : IService, IUpdateable
    {
        bool IsEnabled { get; set; }
    }
}