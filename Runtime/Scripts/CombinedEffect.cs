using UnityEngine;
using UnityEngine.UIElements;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;

namespace OmicronCombinedEffects
{
    [CreateAssetMenu(menuName = "New Combined Effect")]
    public class CombinedEffect : ScriptableObject
    {
        [SerializeField]
        private ParticleEffect _particles;
        [SerializeField]
        private SoundEffect _sound;
        [SerializeField]
        private RigidBodyEffect _rigidBodies;
        [SerializeField]
        private float _defaultForcePower;
        [SerializeField]
        private float _defaultCameraShake;

        public void Play(Vector3 position) =>
            Play(position, Quaternion.identity, position, _defaultForcePower, _defaultCameraShake);

        public void Play(Transform origin) => Play(origin.position, origin.rotation);

        public void Play(Ray ray) => Play(ray.origin, ray.direction);

        public void Play(Vector3 position, Vector3 direction) =>
            Play(position, EffectsExtensions.ToRotation(direction));

        public void Play(Vector3 position, Quaternion rotation) =>
            Play(position, rotation, position, _defaultForcePower, _defaultCameraShake);

        public void Play(Transform origin, Vector3 forceOrigin) =>
            Play(origin.position, origin.rotation, forceOrigin, _defaultForcePower, _defaultCameraShake);

        public void Play(Ray ray, Vector3 forceOrigin) =>
            Play(ray.origin, EffectsExtensions.ToRotation(ray.direction), forceOrigin, _defaultForcePower, _defaultCameraShake);

        public void Play(Vector3 position, Vector3 direction, Vector3 forceOrigin) =>
            Play(position, EffectsExtensions.ToRotation(direction), forceOrigin, _defaultForcePower, _defaultCameraShake);

        public void Play(Vector3 position, Quaternion rotation, Vector3 forceOrigin) =>
            Play(position, rotation, forceOrigin, _defaultForcePower, _defaultCameraShake);

        public void Play(Transform origin, Vector3 forceOrigin, float forcePower) =>
            Play(origin.position, origin.rotation, forceOrigin, forcePower, _defaultCameraShake);

        public void Play(Vector3 position, Quaternion rotation, Vector3 forceOrigin, float forcePower) =>
            Play(position, rotation, forceOrigin, forcePower, _defaultCameraShake);

        public void Play(Ray ray, Vector3 forceOrigin, float forcePower) =>
            Play(ray.origin, EffectsExtensions.ToRotation(ray.direction), forceOrigin, forcePower, _defaultCameraShake);

        public void Play(Vector3 position, Vector3 direction, Vector3 forceOrigin, float forcePower) =>
            Play(position, EffectsExtensions.ToRotation(direction), forceOrigin, forcePower, _defaultCameraShake);

        public void Play(Transform origin, Vector3 forceOrigin, float forcePower, float cameraShake) =>
            Play(origin.position, origin.rotation, forceOrigin, forcePower, cameraShake);

        public void Play(Ray ray, Vector3 forceOrigin, float forcePower, float cameraShake) =>
            Play(ray.origin, EffectsExtensions.ToRotation(ray.direction), forceOrigin, forcePower, cameraShake);

        public void Play(Vector3 position, Quaternion rotation, Vector3 forceOrigin, float forcePower, float cameraShake)
        {
            if (_particles != null)
                _particles.PlayFromPrefab(position, rotation);

            if (_sound != null)
                _sound.PlayFromPrefab(position);

            if (_rigidBodies != null)
                _rigidBodies.PlayFromPrefab(position, rotation, forceOrigin, forcePower);

            if (cameraShake > 0)
                CameraShake.Shake(cameraShake, position);
        }
    }
}