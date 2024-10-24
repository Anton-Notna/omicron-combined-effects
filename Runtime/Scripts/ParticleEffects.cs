using UnityEngine;

namespace OmicronCombinedEffects
{
    public class ParticleEffects : EffectsRoot<ParticleEffects, ParticleEffect>
    {
        public void PlayFromPrefab(ParticleEffect effect, Vector3 position, Quaternion rotation) =>
            GetOrSpawn(effect)?.Emit(position, rotation);
    }
}