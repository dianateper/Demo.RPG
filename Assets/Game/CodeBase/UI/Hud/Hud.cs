using Game.CodeBase.PlayerLogic.PlayerData;
using TMPro;
using UnityEngine;

namespace Game.CodeBase.UI.Hud
{
    public class Hud : WindowBase
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _killsText;

        private HealthData _health;
        private KillData _killData;

        public void Construct(HealthData health, KillData killData)
        {
            _health = health;
            _killData = killData;
            _health.OnHealthChange += UpdateHealth;
            _killData.OnKillChange += UpdateKills;
            UpdateHealth();
            UpdateKills();
        }

        private void OnDestroy()
        {
            _health.OnHealthChange -= UpdateHealth;
            _killData.OnKillChange -= UpdateKills;
        }

        private void UpdateHealth()
        {
            _healthText.text = "Health: " + _health.CurrentHealth;
        }

        private void UpdateKills()
        {
            _killsText.text = "Kills: " + _killData.EnemiesKilled;
        }
    }
}