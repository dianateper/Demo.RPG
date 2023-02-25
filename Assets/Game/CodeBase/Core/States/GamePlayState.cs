namespace Game.CodeBase.Core.States
{
    public interface IGamePlayState<T> : IExitableState
    {
        void Enter(T payload);
    }
}