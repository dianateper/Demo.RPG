namespace Game.CodeBase.PlayerLogic.PlayerData
{
    public interface IPlayerProgress
    {
        HealthData HealthData { get; set; }
        KillData KillData { get; set; }
    }
}