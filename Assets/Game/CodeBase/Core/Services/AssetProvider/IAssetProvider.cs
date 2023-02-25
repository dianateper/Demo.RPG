using UnityEngine;

namespace Game.CodeBase.Core.Services.AssetProvider
{
    public interface IAssetProvider : IService
    {
        T LoadAsset<T>(string path) where T : Object;
    }
}