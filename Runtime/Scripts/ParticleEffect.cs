using UnityEngine;

namespace OmicronCombinedEffects
{
    public class ParticleEffect : QueueEffect
    {
        [SerializeField]
        private ParticleSystem[] _particleSystems;

        public override bool Free => true;

        public void Emit(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;

            for (int i = 0; i < _particleSystems.Length; i++)
            {
                var particleSystem = _particleSystems[i];
                if (particleSystem == null)
                {
                    Debug.LogWarning($"Null particle effect in {gameObject.name}");
                    continue;
                }

                if (particleSystem.emission.burstCount == 0)
                {
                    Debug.LogWarning($"There is no bursts in {particleSystem.name}");
                    continue;
                }

                int amount = Mathf.RoundToInt(particleSystem.emission.GetBurst(0).count.constant);
                if (amount > 0)
                    particleSystem.Emit(amount);
            }
        }
    }
}