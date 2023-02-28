using Game.CodeBase.Core.States;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic.PlayerStates
{
    public class PlayerDieState : IState
    {
        private MonoBehaviour _player;

        public PlayerDieState(MonoBehaviour player)
        {
            _player = player;
        }
        
        public void Enter()
        {
            Object.Destroy(_player.gameObject);
        }

        public void Exit()
        {
           
        }
    }
}