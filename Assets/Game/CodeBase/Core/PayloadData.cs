﻿using Game.CodeBase.Inventory;
using Game.CodeBase.Level;
using Game.CodeBase.PlayerLogic;
using Game.CodeBase.UI.Inventory;

namespace Game.CodeBase.Core
{
    public class PayloadData
    {
        public IPlayer Player;
        public IInventory Inventory;
        public InventoryDataWindow InventoryDataWindow;
        public LevelData LevelData;
    }
}