using Game.CodeBase.Common;
using Game.CodeBase.Core.Updates;
using Game.CodeBase.Inventory;
using Game.CodeBase.PlayerLogic.PlayerData;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public interface IPlayer : IUpdateable
    {
        IPlayerProgress Progress { get; }
        IHealth PlayerHealth { get; }
        void ApplyInventoryItem(ItemType itemId);
        Transform Transform { get; }
    }
}