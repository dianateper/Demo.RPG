using System.Collections.Generic;
using Game.CodeBase.Core.Updates;
using UnityEngine;

namespace Game.CodeBase.Core
{
    public class UpdateableHandler : MonoBehaviour, IUpdateableHandler
    {
        private List<IUpdateable> _updateables = new();
        private List<ILateUpdateable> _lateUpdateables = new();
        
        private void Update()
        {
            foreach (var updatable in _updateables)
            {
                updatable.OnUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            foreach (var lateUpdatable in _lateUpdateables)
            {
                lateUpdatable.OnLateUpdate(Time.deltaTime);
            }
        }

        public void AddUpdatable(IUpdateable updateable) => 
            _updateables.Add(updateable);

        public void AddLateUpdatable(ILateUpdateable updateable) => 
            _lateUpdateables.Add(updateable);

        public void RemoveFromUpdatable(IUpdateable updateable) => 
            _updateables.Remove(updateable);

        public void RemoveFromLateUpdatable(ILateUpdateable updateable) => 
            _lateUpdateables.Remove(updateable);

        public void AddUpdatables(List<IUpdateable> updateables) => 
            _updateables.AddRange(updateables);
    }
}