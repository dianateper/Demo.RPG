using DG.Tweening;
using Game.CodeBase.Core.Services.AssetProvider;
using Game.CodeBase.Core.Services.InputService;
using Game.CodeBase.EnemyLogic;
using Game.CodeBase.Inventory;
using Game.CodeBase.Level;
using Game.CodeBase.PlayerLogic;
using Game.CodeBase.UI;

namespace Game.CodeBase.Core.Services
{
    public class ServiceInstaller
    {
        private IAssetProvider _assetProvider;

        public void InstallService()
        {
            RegisterAssetProvider();
            RegisterInputService();
            RegisterFactories();
            RegisterDotWeen();
        }

        private static void RegisterDotWeen()
        {
            DOTween.Init();
        }

        private void RegisterFactories()
        {
            ServiceLocator.RegisterService<UIFactory>(new UIFactory());
            ServiceLocator.RegisterService<InventoryFactory>(_assetProvider.LoadAsset<InventoryFactory>(Constants.InventoryFactoryPath));
            ServiceLocator.RegisterService<PlayerFactory>(_assetProvider.LoadAsset<PlayerFactory>(Constants.PlayerFactoryPath));
            ServiceLocator.RegisterService<EnemyFactory>(_assetProvider.LoadAsset<EnemyFactory>(Constants.EnemyFactoryPath));
            ServiceLocator.RegisterService<WorldItemFactory>(_assetProvider.LoadAsset<WorldItemFactory>(Constants.WorldItemFactoryPath));
        }

        private void RegisterAssetProvider()
        {
            _assetProvider = new ResourcesProvider();
            ServiceLocator.RegisterService<IAssetProvider>(_assetProvider);
        }

        private void RegisterInputService()
        {
            ServiceLocator.RegisterService<IInputService>(new StandaloneInputService());
        }
    }
}