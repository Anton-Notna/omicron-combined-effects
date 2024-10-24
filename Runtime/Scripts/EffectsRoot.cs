using System.Collections.Generic;
using UnityEngine;

namespace OmicronCombinedEffects
{
    public abstract class EffectsRoot<TEffects, TEffect> : Effects<TEffects, TEffect> where TEffects : EffectsRoot<TEffects, TEffect> where TEffect : QueueEffect
    {
        private readonly Dictionary<TEffect, List<TEffect>> _effects = new Dictionary<TEffect, List<TEffect>>();

        protected TEffect GetOrSpawn(TEffect prefab)
        {
            if (prefab == null)
            {
                Debug.LogWarning($"There is null prefab of {typeof(TEffect).Name}");
                return null;
            }

            if (_effects.TryGetValue(prefab, out var list) == false)
            {
                list = new List<TEffect>();
                _effects.Add(prefab, list);
            }

            foreach (var item in list)
            {
                if (item.Free)
                    return item;
            }

            var instance = GameObject.Instantiate(prefab, Instance.transform);
            list.Add(instance);
            instance.OnSpawned();

            return instance;
        }
    }
}