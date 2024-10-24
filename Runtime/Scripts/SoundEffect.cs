using System.Collections.Generic;
using UnityEngine;

namespace OmicronCombinedEffects
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffect : QueueEffect
    {
        [SerializeField]
        private List<AudioClip> _clips;
        [SerializeField, Range(0f, 1f)]
        private float _randomPitch;

        private System.Random _random;
        private float _defaultPitch;
        private AudioSource _audioSource;
        private float _releaseTime = float.MinValue;

        public override bool Free => Time.time > _releaseTime;

        public override void OnSpawned()
        {
            _random = new System.Random();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = false;
            _defaultPitch = _audioSource.pitch;
        }

        public void Play(Vector3 position)
        {
            transform.position = position;

            AudioClip clip = _clips[_random.Next(_clips.Count - 1)];
            _audioSource.clip = clip;

            float pitch = _defaultPitch + ((float)_random.Next(Mathf.RoundToInt(-_randomPitch * 1000f), Mathf.RoundToInt(_randomPitch * 1000f)) / 1000f);
            _audioSource.pitch = pitch;

            _audioSource.Play();
            _releaseTime = Time.time + _audioSource.clip.length;
        }
    }
}