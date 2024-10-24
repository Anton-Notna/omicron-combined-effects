using UnityEngine;

namespace OmicronCombinedEffects
{
    public class SoundEffects : EffectsRoot<SoundEffects, SoundEffect>
    {
        public void PlayFromPrefab(SoundEffect effect, Vector3 position) =>
            GetOrSpawn(effect)?.Play(position);
    }
}