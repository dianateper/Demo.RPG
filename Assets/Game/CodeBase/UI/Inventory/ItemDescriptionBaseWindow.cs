using System;
using DG.Tweening;
using Game.CodeBase.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.CodeBase.UI.Inventory
{
    public class ItemDescriptionBaseWindow : WindowBase
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _closeButton;
        [SerializeField] private RectTransform _container;
        
        private const float SlideDuration = 0.3f;
        protected ItemType _itemType;
        
        public event Action OnCloseButtonClick;

        public override void Initialize()
        {
            base.Initialize();
            _closeButton.onClick.AddListener(() =>
            {
                OnCloseButtonClick?.Invoke();
                Hide();
            });
        }

        public void Show(IItem item)
        {
            _itemImage.sprite = item.Sprite;
            _title.text = item.Name;
            _description.text = item.Description;
            _itemType = item.ItemId;
            _container.anchoredPosition = Vector2.up * _container.sizeDelta.y;            
            
            base.Show();
            Animate();
        }

        private void Animate()
        {
           _container.DOAnchorPos(Vector2.down * _container.sizeDelta.y, SlideDuration)
                .SetEase(Ease.Flash);
        }
    }
}
