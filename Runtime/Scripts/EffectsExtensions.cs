using UnityEngine;

namespace OmicronCombinedEffects
{
    public static class EffectsExtensions
    {
        public static void PlayFromPrefab(this RigidBodyEffect effect, Vector3 position, float force) =>
            effect.PlayFromPrefab(position, Quaternion.identity, position, force);

        public static void PlayFromPrefab(this RigidBodyEffect effect, Vector3 position, Quaternion rotation, float force) =>
            effect.PlayFromPrefab(position, rotation, position, force);

        public static void PlayFromPrefab(this RigidBodyEffect effect, Vector3 position, Quaternion rotation, Vector3 forceSource, float force) =>
            RigidBodyEffects.Instance.PlayFromPrefab(effect, position, rotation, forceSource, force);

        public static void PlayFromPrefab(this SoundEffect effect) =>
            SoundEffects.Instance.PlayFromPrefab(effect, Vector3.zero);

        public static void PlayFromPrefab(this SoundEffect effect, Vector3 position) =>
            SoundEffects.Instance.PlayFromPrefab(effect, position);

        public static void PlayFromPrefab(this ParticleEffect effect, Transform origin) =>
            PlayFromPrefab(effect, origin.position, origin.rotation);

        public static void PlayFromPrefab(this ParticleEffect effect, Vector3 position) =>
            PlayFromPrefab(effect, position, Quaternion.identity);

        public static void PlayFromPrefab(this ParticleEffect effect, Ray ray) =>
            PlayFromPrefab(effect, ray.origin, ToRotation(ray.direction));

        public static void PlayFromPrefab(this ParticleEffect effect, Vector3 position, Vector3 forward) =>
            PlayFromPrefab(effect, position, ToRotation(forward));

        public static void PlayFromPrefab(this ParticleEffect effect, Vector3 position, Quaternion rotation) =>
            ParticleEffects.Instance.PlayFromPrefab(effect, position, rotation);

        private static Quaternion ToRotation(Vector3 forward)
        {
            if (forward == Vector3.zero)
                return Quaternion.identity;

            forward = forward.normalized;

            Vector3 up = forward == Vector3.up ? Vector3.forward : Vector3.up;
            return Quaternion.LookRotation(forward, up);
        }
    }
}