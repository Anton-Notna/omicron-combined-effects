using UnityEngine;

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

        public void Play(Vector3 position, Quaternion rotation) =>
            Play(position, rotation, position, _defaultForcePower, _defaultCameraShake);

        public void Play(Vector3 position, Quaternion rotation, Vector3 forceOrigin) =>
            Play(position, rotation, forceOrigin, _defaultForcePower, _defaultCameraShake);

        public void Play(Vector3 position, Quaternion rotation, Vector3 forceOrigin, float forcePower) =>
            Play(position, rotation, forceOrigin, forcePower, _defaultCameraShake);

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