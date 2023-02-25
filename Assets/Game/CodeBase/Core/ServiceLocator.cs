using System;
using System.Collections.Generic;
using Game.CodeBase.Core.Services;
using UnityEngine;

namespace Game.CodeBase.Core
{
    public class ServiceLocator
    {
        private static Dictionary<Type, IService> _services = new();

        public static void RegisterService<T>(IService service) where T : IService
        {
            if (_services.TryAdd(typeof(T), service) == false)
                Debug.LogError("Cannot add service of type : " + service.GetType());
        }

        public static T ResolveService<T>() where T : class
        {
            if (_services.ContainsKey(typeof(T)) == false) 
                Debug.LogError("Cannot resolve " + typeof(T));
            return _services[typeof(T)] as T;
        }
    }
}