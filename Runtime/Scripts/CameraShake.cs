using UnityEngine;

namespace OmicronCombinedEffects
{
    public class CameraShake : MonoBehaviour
    {
        private static CameraShake _instance;

        [SerializeField]
        private bool _setupSelfAsSingleton;
        [Space]
        [SerializeField]
        private float _powerSmooth = 0.1f;
        [SerializeField]
        private float _calmSmooth = 0.1f;
        [SerializeField]
        private float _directionSmooth = 0.1f;
        [SerializeField]
        private float _directionChangeTime = 0.1f;
        [SerializeField]
        private float _power = 1f;
        [SerializeField]
        private float _maxDistance = 30;
        [SerializeField]
        private float _distancePower = 0.2f;

        private readonly System.Random _random = new System.Random();

        private float _currentPower;
        private float _targetPower;
        private float _powerVelocity;
        private float _calmVelocity;

        private Vector2 _currentDirection;
        private Vector2 _targetDirection = Vector2.right;
        private Vector2 _directionVelocity;

        private float _lastDirectionChange = float.MinValue;

        public Vector2 Offset => _currentDirection * _currentPower * _power;

        public static Vector2 CurrentOffset => _instance == null ? Vector2.zero : _instance.Offset;

        public static void Shake(float power, Vector3 position)
        {
            if (_instance != null)
                _instance.ShakeCamera(power, position);
        }

        public void ShakeCamera(float power, Vector3 position)
        {
            position.z = transform.position.z;
            float distance = Vector3.Distance(position, transform.position);
            float distancePower = 1f - Mathf.Clamp01(distance / _maxDistance);
            distancePower = Mathf.Pow(distancePower, _distancePower);
            _targetPower += Mathf.Clamp(power * distancePower, 0f, float.MaxValue);
        }

        public void SetupAsSingleton()
        {
            if (_instance != null)
            {
                Destroy(_instance.gameObject);
                _instance = null;
            }

            _instance = this;
        }

        public void ClearSingleton()
        {
            if (_instance == this)
                _instance = null;
        }

        private void OnEnable()
        {
            if (_setupSelfAsSingleton)
                SetupAsSingleton();
        }

        private void OnDisable()
        {
            if (_setupSelfAsSingleton)
                ClearSingleton();
        }

        private void Update()
        {
            _targetPower = Mathf.SmoothDamp(_targetPower, 0, ref _calmVelocity, _calmSmooth);
            _currentPower = Mathf.SmoothDamp(_currentPower, _targetPower, ref _powerVelocity, _powerSmooth);

            if (_currentPower < Mathf.Epsilon)
                return;

            if (Time.time > _lastDirectionChange + _directionChangeTime)
            {
                _lastDirectionChange = Time.time;
                _targetDirection = Quaternion.Euler(0, 0, 180 + _random.Next(-30, 30)) * _targetDirection;
            }

            _currentDirection = Vector2.SmoothDamp(_currentDirection, _targetDirection, ref _directionVelocity, _directionSmooth);
        }
    }
}