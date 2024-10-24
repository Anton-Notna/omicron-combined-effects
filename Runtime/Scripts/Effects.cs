using UnityEngine;

namespace OmicronCombinedEffects
{
    public abstract class Effects<TEffects, TEffect> : MonoBehaviour where TEffects : Effects<TEffects, TEffect> where TEffect : MonoBehaviour
    {
        private static TEffects _instance;

        public static TEffects Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject($"{typeof(TEffects).Name}Root").AddComponent<TEffects>();
                    GameObject.DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }
    }
}