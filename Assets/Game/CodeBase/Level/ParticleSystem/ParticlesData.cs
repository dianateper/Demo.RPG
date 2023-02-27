using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.CodeBase.Level.ParticleSystem
{
    [Serializable]
    public class ParticlesData
    {
        [SerializeField] private List<Particle> _particles;

        public Particle GetParticleData(ParticleId particleId) => _particles.FirstOrDefault(p => p.ParticleId == particleId);
    }
}