using System;
using Game.CodeBase.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.CodeBase.UI.Inventory
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _activeBackground;
        [SerializeField] private TMP_Text _itemCount;
        [SerializeField] private Button _itemDescriptionButton;
        [SerializeField] private Button _removeItemButton;
        private ItemType _itemType;

        public event Action<ItemType> OnItemClick;
        public event Action<ItemType> OnRemoveItemFromInventoryClick;

        public void SetSlot(IInventorySlot slot)
        {
            _itemType = slot.Item.ItemId;
            _itemImage.sprite = slot.Item.Sprite;
            _itemImage.color = new Color(_itemImage.color.r, _itemImage.color.g, _itemImage.color.b, 1);
            if (slot.Count > 1)
            {
                _itemCount.gameObject.SetActive(true);
                _itemCount.text = $"x{slot.Count}";
            }
            _itemDescriptionButton.onClick.AddListener(ShowItemDescription);
            _itemDescriptionButton.interactable = true;
            
            _removeItemButton.gameObject.SetActive(true);
            _removeItemButton.interactable = true;
            _removeItemButton.onClick.AddListener(RemoveItemFromInventory);
        }

        public void Clear()
        {
            _itemImage.sprite = null;
            _itemImage.color = new Color(_itemImage.color.r, _itemImage.color.g, _itemImage.color.b, 0);
            _itemCount.gameObject.SetActive(false);
            _itemCount.text = string.Empty;
            _itemDescriptionButton.interactable = false;
            _removeItemButton.gameObject.SetActive(false);
            _removeItemButton.interactable = false;
        }

        public void SetActive()
        {
            _itemCount.color = Color.white;
            _activeBackground.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _itemCount.color = Color.black;
            _activeBackground.gameObject.SetActive(false);
        }

        private void RemoveItemFromInventory() => OnRemoveItemFromInventoryClick?.Invoke(_itemType);

        private void ShowItemDescription() => OnItemClick?.Invoke(_itemType);
    }
}
