using Game.CodeBase.Core.Services;
using Game.CodeBase.Inventory;
using Game.CodeBase.StaticData;
using UnityEngine;

namespace Game.CodeBase.Level
{
    [CreateAssetMenu(fileName = "WorldItemFactory", menuName = "RPG/WorldItemFactory")]
    public class WorldItemFactory : ScriptableObject, IService
    {
        [SerializeField] private ItemsData _itemsData;
        public WorldItem CreateWorldItem(ItemType itemType, Vector3 transformPosition)
        {
            var item = _itemsData.GetItemPrefab(itemType);
            var result = Instantiate(item, transformPosition, Quaternion.identity);
            result.SetItemType(itemType);
            return result;
        }
    }
}