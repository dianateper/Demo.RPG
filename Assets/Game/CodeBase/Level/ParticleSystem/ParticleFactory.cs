using Game.CodeBase.Core.Services;
using UnityEngine;

namespace Game.CodeBase.Level.ParticleSystem
{
    [CreateAssetMenu(fileName = "ParticleFactory", menuName = "RPG/ParticleFactory")]
    public class ParticleFactory : ScriptableObject, IService
    {
        [SerializeField] private ParticlesData _particles;

        public void CreateParticle(ParticleId particleId, Vector3 at, bool destroyable = false)
        {
            var particleData = _particles.GetParticleData(particleId);
            var particle =  Instantiate(particleData.ParticlePrefab, at, Quaternion.identity);
            
            if (destroyable) 
                Destroy(particle, particleData.LifeTime);
        }
    }
}