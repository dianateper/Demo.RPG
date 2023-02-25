using Game.CodeBase.Inventory;
using UnityEngine;

namespace Game.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "RPG/Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private Item _item;
        public Item Item => _item;
    }
}