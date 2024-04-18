using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Patterns
{
    /// <summary>
    /// A blackboard in which to store custom data containers or components, using their class type as a key.
    /// </summary>
    public class FittedBlackboard
    {
        private Dictionary<System.Type, object> m_pool = new();
        private bool m_replace = false;

        public FittedBlackboard() { }

        public FittedBlackboard(bool _replaceInstances)
        {
            m_replace = _replaceInstances;
        }

        public void Add(object _instance)
        {
            if (_instance == null)
            {
                Debug.LogWarning("Object passed in the Blackboard is null. This could be a problem for attempting to retrieve it in the future.");
                return;
            }

            var key = _instance.GetType();
            if (m_pool.ContainsKey(key))
            {
                if (m_replace)
                {
                    Debug.Log($"Replaced: {key} that was already present in the blackboard.");
                    m_pool.Remove(key);
                }
                else
                {
                    Debug.LogWarning($"Key: {key} already present in the blackboard.");
                    return;
                }
            }
            m_pool.Add(key, _instance);
        }

        public void Remove(System.Type _key)
        {
            if (!m_pool.ContainsKey(_key))
            {
                Debug.LogWarning($"Key: {_key} is not present in the blackboard.");
                return;
            }
            m_pool.Remove(_key);
        }

        public T Get<T>()
        {
            var key = typeof(T);

            if (m_pool.ContainsKey(key))
            {
                return (T)m_pool[key];
            }
            else
            {
                Debug.LogError($"Key: {key} did not return a valid instance.");
                return default;
            }
        }
    }
}
