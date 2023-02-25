namespace Game.CodeBase.Core.Updates
{
    public interface IUpdateable
    {
        void OnUpdate(float deltaTime);
    }
}