using System.Collections.Generic;
using UnityEngine;

namespace OmicronCombinedEffects
{
    public class RigidBodyEffect : QueueEffect
    {
        private class TransformData
        {
            private Rigidbody _body;
            private Transform _transform;
            private Vector3 _position;
            private Quaternion _rotation;
            private Vector3 _scale;

            public TransformData(Rigidbody obj)
            {
                _body = obj;
                _transform = obj.transform;
                _position = obj.transform.localPosition;
                _rotation = obj.transform.localRotation;
                _scale = obj.transform.localScale;
            }

            public void MoveToDefault()
            {
                _transform.localPosition = _position;
                _transform.localRotation = _rotation;
                _transform.localScale = _scale;
                _body.velocity = Vector3.zero;
                _body.angularVelocity = Vector3.zero;
            }

            public void EvaluateScale(float t)
            {
                t = Mathf.Clamp01(t);
                Vector3 scale = _scale * t;
                _transform.localScale = scale;
            }

            public void Disable()
            {
                _body.gameObject.SetActive(false);
            }

            public void Enable()
            {
                _body.gameObject.SetActive(true);
            }

            internal void AddForce(Vector3 forceLocalPosition, float force, ForceMode forceMode)
            {
                Vector3 direction = (_position - forceLocalPosition).normalized;
                _body.AddForce(direction * force, forceMode);
            }
        }

        [SerializeField]
        private float _lifeTime = 7f;
        [SerializeField]
        private AnimationCurve _scaleOnLifeTime;
        [SerializeField]
        private ForceMode _forceMode;
        [SerializeField]
        private List<Rigidbody> _bodies;

        private List<TransformData> _data;
        private bool _free;
        private float _lastCallTime;

        public override bool Free => _free;

        public override void OnSpawned()
        {
            _data = new List<TransformData>();
            foreach (var item in _bodies)
                _data.Add(new TransformData(item));

            foreach (var item in _data)
                item.Disable();

            _free = true;
        }

        public void Emit(Vector3 position, Quaternion rotation, Vector3 forcePosition, float force)
        {
            _lastCallTime = Time.time;
            _free = false;
            transform.position = position;
            transform.rotation = rotation;

            Vector3 forceLocalPosition = transform.InverseTransformPoint(forcePosition);
            foreach (var item in _data)
            {
                item.MoveToDefault();
                item.Enable();
                item.AddForce(forceLocalPosition, force, _forceMode);
            }
        }

        private void Update()
        {
            if (_free)
                return;

            float t = (Time.time - _lastCallTime) / _lifeTime;
            t = Mathf.Clamp01(t);
            if (t == 1)
            {
                foreach (var item in _data)
                    item.Disable();

                _free = true;
            }
            else
            {
                foreach (var item in _data)
                    item.EvaluateScale(_scaleOnLifeTime.Evaluate(t));
            }
        }
    }
}