using Game.CodeBase.Inventory;
using Game.CodeBase.Level;
using UnityEngine;

namespace Game.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "RPG/Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private Item _item;
        [SerializeField] private WorldItem _worldItem;
        public Item Item => _item;
        public WorldItem WorldItem => _worldItem;
    }
}