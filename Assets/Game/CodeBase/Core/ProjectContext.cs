using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.Core.ProjectStates;
using Game.CodeBase.Core.Services;
using Game.CodeBase.Core.States;
using UnityEngine;

namespace Game.CodeBase.Core
{
    public class PayloadDataContext : MonoBehaviour, IPayloadDataStateSwitcher, IStateSwitcher
    {
        private ServiceInstaller _serviceInstaller;

        private List<IExitableState> _states;
        private IExitableState _currentState;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _states = new List<IExitableState>
            {
                new LoadingState(this),
                new LoadLevelState(this),
                new GameLoopState(this, this),
                new GameOverState(this),
                new GameWinState(this),
                new InventoryState(this)
            };
            var context = this as IStateSwitcher;
            context.SwitchState<LoadingState>();
        }

        void IStateSwitcher.SwitchState<T>() 
        {
            _currentState?.Exit();
            var currentState = _states.FirstOrDefault(s => s.GetType() == typeof(T)) as T;
            _currentState = currentState;
            currentState?.Enter();
        }

        void IPayloadDataStateSwitcher.SwitchState<T>(PayloadData payloadData)
        {
            _currentState?.Exit();
            var currentState = _states.FirstOrDefault(s => s.GetType() == typeof(T)) as T;
            _currentState = currentState;
            currentState?.Enter(payloadData);
        }
    }
}