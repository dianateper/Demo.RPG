using System.Collections.Generic;
using System.Linq;
using Game.CodeBase.StaticData;
using UnityEngine;

namespace Game.CodeBase.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "RPG/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<LevelSettings> _levelSettings;
        [SerializeField] private ItemsData _itemsData;
   
        public LevelSettings GetLevelSettings(LevelType levelType) => 
            _levelSettings.FirstOrDefault(l => l.LevelType == levelType);

        public ItemsData GetItemsData() => Instantiate(_itemsData);
    }
}