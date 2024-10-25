using UnityEngine;

namespace OmicronCombinedEffects
{
    public class CombinedEffectCall : MonoBehaviour
    {
        [SerializeField]
        private CombinedEffect _effect;
        [Space]
        [SerializeField]
        private bool _callOnAwake;
        [SerializeField]
        private bool _callOnStart;
        [SerializeField]
        private bool _callOnEnable;
        [SerializeField]
        private bool _callOnDisable;
        [SerializeField]
        private bool _callOnDestroy;

        public void Call() => _effect?.Play(transform);

        private void Awake()
        {
            if (_callOnAwake)
                Call();
        }

        private void Start()
        {
            if (_callOnStart)
                Call();
        }

        private void OnEnable()
        {
            if (_callOnEnable)
                Call();
        }

        private void OnDisable()
        {
            if (_callOnDisable)
                Call();
        }

        private void OnDestroy()
        {
            if ( _callOnDestroy)
                Call();
        }
    }
}