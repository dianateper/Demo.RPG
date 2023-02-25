using UnityEngine;

namespace Game.CodeBase.UI
{
    [RequireComponent(typeof(Canvas))]
    public class WindowBase : MonoBehaviour
    {
        protected Canvas _canvas;

        public virtual void Initialize()
        {
            _canvas = GetComponent<Canvas>();
        }
        
        public virtual void Hide() => _canvas.enabled = false;

        public virtual void Show() => _canvas.enabled = true;
    }
}