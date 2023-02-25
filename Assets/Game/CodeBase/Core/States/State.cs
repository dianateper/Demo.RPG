namespace Game.CodeBase.Core.States
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}