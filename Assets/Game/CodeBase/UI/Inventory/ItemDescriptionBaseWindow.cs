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
        public ItemType ItemType;
        
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
            ItemType = item.ItemId;
            _container.anchoredPosition = Vector2.up * _container.sizeDelta.y;            
            
            base.Show();
        }

        private void AnimateSlideDown()
        {
           _container.DOAnchorPos(Vector2.down * _container.sizeDelta.y, SlideDuration)
                .SetEase(Ease.Flash);
        }
        
        private void AnimateSlideUp(TweenCallback onComplete)
        {
            _container
                .DOAnchorPos(Vector2.up * _container.sizeDelta.y, SlideDuration)
                .SetEase(Ease.Flash).OnComplete(onComplete);
        }

        public void AnimateShow(IItem item)
        {
            AnimateSlideDown();
            Show(item);
        }
        
        public void AnimateHide(TweenCallback onComplete) => AnimateSlideUp(onComplete);
    }
}
