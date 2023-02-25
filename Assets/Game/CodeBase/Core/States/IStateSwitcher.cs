namespace Game.CodeBase.Core.States
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : class, IState;
    }
}