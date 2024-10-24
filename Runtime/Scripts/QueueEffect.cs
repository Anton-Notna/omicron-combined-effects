using UnityEngine;

namespace OmicronCombinedEffects
{
    public abstract class QueueEffect : MonoBehaviour
    {
        public abstract bool Free { get; }

        public virtual void OnSpawned() { }
    }
}