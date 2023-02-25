using System;
using System.Collections.Generic;
using Game.CodeBase.UI.Windows;
using UnityEngine;

namespace Game.CodeBase.UI
{
    [CreateAssetMenu(fileName = "UIData", menuName = "RPG/UIData")]
    public class UIDataList : ScriptableObject
    {
        [SerializeField] private List<UIData> _uiDataList;
        public List<UIData> UIData => _uiDataList;
    }
    
    [Serializable]
    public class UIData
    {
        [SerializeField] private WindowBase _window;
        [SerializeField] private WindowId _windowId;
        public WindowId WindowId => _windowId;
        public WindowBase Window => _window;
    }
}