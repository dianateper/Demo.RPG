using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.CodeBase.UI.Windows
{
    public class GameOverWindow : WindowBase
    {
        public event Action OnReloadClick;

        [SerializeField] private Button _reloadButton;

        private void Start() => 
            _reloadButton.onClick.AddListener(() => OnReloadClick?.Invoke());
    }

    public enum WindowId
    {
        HUD,
        GameOver,
        Inventory,
        Message
    }
}