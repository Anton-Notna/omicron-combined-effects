using UnityEngine;

namespace OmicronCombinedEffects
{
    public class RigidBodyEffects : EffectsRoot<RigidBodyEffects, RigidBodyEffect>
    {
        public void PlayFromPrefab(RigidBodyEffect effect, Vector3 position, Quaternion rotation, Vector3 forcePosition, float force) =>
            GetOrSpawn(effect)?.Emit(position, rotation, forcePosition, force);
    }
}