using System;
using UnityEngine;

namespace Game.CodeBase.Level
{
    [Serializable]
    public class LevelSettings 
    {
        [SerializeField] private LevelType _levelType;
        [SerializeField] private int _spawnOuterRadius;
        [SerializeField] private int _numberOfEnemies;
        [SerializeField] private float _spawnInnerRadius;

        public LevelType LevelType => _levelType;
        public float SpawnOuterRadius => _spawnOuterRadius;
        public float SpawnInnerRadius => _spawnInnerRadius;
        public float NumberOfEnemies => _numberOfEnemies;
    }

    public enum LevelType
    {
        Easy,
        Normal,
        Hard
    }
}