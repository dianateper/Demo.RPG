using System;
using UnityEngine;

namespace Game.CodeBase.Level.ParticleSystem
{
    [Serializable]
    public class Particle
    {
        [SerializeField] private ParticleId _particleId;
        [SerializeField] private GameObject _particle;
        [SerializeField] private float  _lifeTime;

        public ParticleId ParticleId => _particleId;
        public GameObject ParticlePrefab => _particle;
        public float LifeTime => _lifeTime;
    }
}