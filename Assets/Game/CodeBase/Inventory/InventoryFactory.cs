using System;
using Game.CodeBase.Core.Services;
using UnityEngine;

namespace Game.CodeBase.Inventory
{
    [CreateAssetMenu(fileName = "InventoryFactory", menuName = "RPG/Inventory/InventoryFactory")]
    public class InventoryFactory : ScriptableObject, IService
    {
        [SerializeField] private InventorySettings _inventorySettings;
        
        public Inventory CreateInventory()
        {
            return new Inventory(_inventorySettings.InventoryCapacity, _inventorySettings.InventorySlotCapacity);
        }
    }
    
    
    [Serializable]
    public class InventorySettings
    {
        [SerializeField] private int _inventorySlotCapacity;
        [SerializeField] private int _inventoryCapacity;
        public int InventorySlotCapacity => _inventorySlotCapacity;
        public int InventoryCapacity => _inventoryCapacity;
    }
}