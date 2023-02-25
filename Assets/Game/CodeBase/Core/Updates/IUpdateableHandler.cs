using System.Collections.Generic;
using Game.CodeBase.Core.Updates;

namespace Game.CodeBase.Core
{
    public interface IUpdateableHandler
    {
        public void AddUpdatable(IUpdateable updateable);
        public void AddLateUpdatable(ILateUpdateable updateable);
        void RemoveFromUpdatable(IUpdateable updateable);
        void RemoveFromLateUpdatable(ILateUpdateable updateable);
        void AddUpdatables(List<IUpdateable> updateables);
    }
}