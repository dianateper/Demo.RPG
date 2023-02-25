namespace Game.CodeBase.Core.States
{
    public interface IPayloadDataStateSwitcher 
    {
        void SwitchState<T>(PayloadData payloadData) where T : class, IGamePlayState<PayloadData>;
    }
}