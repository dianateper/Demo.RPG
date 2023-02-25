using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.CodeBase.UI.Windows
{
    public class MessageWindow : WindowBase
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private RectTransform _container;
        [SerializeField] private CanvasGroup _canvasGroup;

        private const float MaxScale = 1.1f;
        private const float MinScale = 1f;
        private const float MaxScaleDuration = 1;
        private const float MinScaleDuration = 1;
        private const float MaxAlpha = 1;
        private const float FadeDuration = 1;
        private const float SlideDuration = 0.3f;

        public void ShowMessage(string message)
        {
            messageText.text = message;
            Animate();
        }

        private void Animate()
        {
            _container.DOScale(MaxScale, MaxScaleDuration)
                .OnComplete(() => _container.DOScale(MinScale, MinScaleDuration));

            _canvasGroup.DOFade(MaxAlpha, FadeDuration)
                .OnComplete(() => _container.DOAnchorPos(Vector2.up * _container.sizeDelta.y, SlideDuration))
                .SetEase(Ease.Flash);
        }

        public override void Hide() => Destroy(gameObject);
    }
}
