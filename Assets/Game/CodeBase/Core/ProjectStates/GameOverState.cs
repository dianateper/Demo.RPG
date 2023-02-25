using Game.CodeBase.Core.Services.AssetProvider;
using Game.CodeBase.Core.States;
using Game.CodeBase.UI;
using Game.CodeBase.UI.Windows;
using UnityEngine.SceneManagement;

namespace Game.CodeBase.Core.ProjectStates
{
    public class GameOverState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private GameOverWindow _gameOverWindow;
        private IAssetProvider _assetProvider;
        private UIFactory _uiFactory;

        public GameOverState(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
            _uiFactory = ServiceLocator.ResolveService<UIFactory>();
            _gameOverWindow = _uiFactory.CreateWindow(WindowId.GameOver) as GameOverWindow;
            _gameOverWindow.OnReloadClick += ReloadGame;
        }
        
        public void Exit()
        {
            
        }

        private void ReloadGame()
        {
            var handler = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            handler.completed += _ => _stateSwitcher.SwitchState<LoadLevelState>();
        }
    }
}