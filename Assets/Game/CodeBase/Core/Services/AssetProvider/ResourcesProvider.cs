using UnityEngine;

namespace Game.CodeBase.Core.Services.AssetProvider
{
    public class ResourcesProvider : IAssetProvider
    {
        public T LoadAsset<T>(string path) where T : Object
        {
            var asset = Resources.Load<T>(path);
            if (asset == null)
            {
                Debug.LogError("Cannot load an asset by path : " + path);
            }
            return asset;
        }
    }
}