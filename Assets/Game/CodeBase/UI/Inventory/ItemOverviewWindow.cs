using System;
using Game.CodeBase.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Game.CodeBase.UI.Inventory
{
    public class ItemOverviewWindow : ItemDescriptionBaseWindow
    {
        [SerializeField] private Button _applyButton;
        public event Action<ItemType> OnApplyClick;

        public override void Initialize()
        {
            base.Initialize();
            _applyButton.onClick.AddListener(ApplyItem);
        }

        private void ApplyItem() => OnApplyClick?.Invoke(_itemType);
    }
}