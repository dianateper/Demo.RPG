namespace Game.CodeBase.PlayerLogic.PlayerData
{
    public class PlayerProgress : IPlayerProgress
    {
        public PlayerProgress()
        {
            HealthData = new HealthData();
            KillData = new KillData();
        }
        public HealthData HealthData { get; set; }
        public KillData KillData { get; set; }
    }
}