using System;
using Game.CodeBase.Core.Updates;
using Game.CodeBase.Inventory;
using Game.CodeBase.PlayerLogic.PlayerData;
using UnityEngine;

namespace Game.CodeBase.PlayerLogic
{
    public interface IPlayer : IUpdateable
    {
        IPlayerProgress Progress { get; }
        event Action OnDie;
        void ApplyInventoryItem(ItemType itemId);
        Transform Transform { get; }
    }
}