namespace Game.CodeBase.Core.Updates
{
    public interface ILateUpdateable
    {
        void OnLateUpdate(float deltaTime);
    }
}