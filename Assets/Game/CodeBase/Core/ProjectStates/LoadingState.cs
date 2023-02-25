using Game.CodeBase.Core.Services;
using Game.CodeBase.Core.States;
using UnityEngine.SceneManagement;

namespace Game.CodeBase.Core.ProjectStates
{
    public class LoadingState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private ServiceInstaller _serviceInstaller;

        public LoadingState(IStateSwitcher stateSwitcher) 
        {
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
            _serviceInstaller = new ServiceInstaller();
            _serviceInstaller.InstallService();
            LoadLevelScene();
        }
        
        private void LoadLevelScene()
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(Constants.Level);
            loadSceneAsync.completed += _ => _stateSwitcher.SwitchState<LoadLevelState>();
        }

        public void Exit()
        {
            
        }
    }
}