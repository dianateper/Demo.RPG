using System.Collections.Generic;
using System.Linq;
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
            foreach (var updatable in _updateables.ToList())
            {
                updatable?.OnUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            foreach (var lateUpdatable in _lateUpdateables.ToList())
            {
                lateUpdatable?.OnLateUpdate(Time.deltaTime);
            }
        }

        public void AddUpdatable(IUpdateable updateable)
        {
            var newList = _updateables;
            newList.Add(updateable);
            _updateables = newList;
        }

        public void AddLateUpdatable(ILateUpdateable updateable)
        {
            var newList = _lateUpdateables;
            newList.Add(updateable);
            _lateUpdateables = newList;
        }

        public void RemoveFromUpdatable(IUpdateable updateable)
        {
            var newList = _updateables;
            newList.Remove(updateable);
            _updateables = newList;
        }

        public void RemoveFromLateUpdatable(ILateUpdateable updateable)
        {
            var newList = _lateUpdateables;
            newList.Remove(updateable);
            _lateUpdateables = newList;
        }

        public void AddUpdatables(List<IUpdateable> updateables)
        {
            var newList = _updateables;
            newList.AddRange(updateables);
            _updateables = newList;
        }
    }
}