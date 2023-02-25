using System.Linq;
using Game.CodeBase.Core;
using Game.CodeBase.Core.Services;
using Game.CodeBase.Core.Services.AssetProvider;
using Game.CodeBase.UI.Windows;
using Object = UnityEngine.Object;

namespace Game.CodeBase.UI
{
    public class UIFactory : IService
    {
        private const string UIDataPath = "UI/UIData";
        private IAssetProvider _assetProvider;

        public UIFactory()
        {
            _assetProvider = ServiceLocator.ResolveService<IAssetProvider>();
        }

        public WindowBase CreateWindow(WindowId id)
        {
            UIDataList data = _assetProvider.LoadAsset<UIDataList>(UIDataPath);
            return Object.Instantiate(data.UIData.FirstOrDefault(x => x.WindowId == id)?.Window);
        }
    }
}