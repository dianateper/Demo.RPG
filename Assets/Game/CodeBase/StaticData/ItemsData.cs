﻿using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.Inventory;
using UnityEngine;

namespace Game.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "ItemsData", menuName = "RPG/Inventory/ItemsData")]
    public class ItemsData : ScriptableObject
    {
        [SerializeField] private List<ItemData> _items;

        public IItem GetItem(ItemType itemType)
        {
            var item = _items.FirstOrDefault(t => t.Item.ItemId == itemType)?.Item;
            return item.Clone();
        } 
    }
}