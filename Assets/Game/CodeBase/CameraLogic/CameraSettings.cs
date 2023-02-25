using System;
using UnityEngine;

namespace Game.CodeBase.CameraLogic
{
    [Serializable]
    public class CameraSettings
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _distance;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _rotationX;
        
        public float Speed => _speed;
        public float Distance => _distance;
        public float OffsetY => _offsetY;
        public float RotationX => _rotationX;
    }
}