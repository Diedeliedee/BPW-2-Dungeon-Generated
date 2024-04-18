using Joeri.Tools.Patterns.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Utilities.SpawnManager
{
    public class PoolCollection : MonoBehaviour, ISpawnManager
    {
        [SerializeField] private Association[] m_poolTypes;
        [Space]
        [SerializeField] private int m_groupSize = 5;

        private Dictionary<string, ObjectPool> m_pools = new();

        private void Awake()
        {
            for (int i = 0; i < m_poolTypes.Length; i++)
            {
                var pool = new ObjectPool(m_poolTypes[i].prefab, m_groupSize, transform, transform);
                var type = m_poolTypes[i].type;

                m_pools.Add(type, pool);
            }
        }

        public GameObject Spawn(string _type, Vector3 _position, Quaternion _rotation)
        {
            try
            {
                return m_pools[_type].Spawn(_position, _rotation, null);
            }
            catch
            {
                Debug.Log($"The poolable object of type: {_type} does not seem to exist.", gameObject);
                return default;
            }
        }

        public T Spawn<T>(string _type, Vector3 _position, Quaternion _rotation)
        {
            try
            {
                if (!m_pools[_type].Spawn(_position, _rotation, null).TryGetComponent(out T _component))
                {
                    Debug.Log("Requested object to be spawned is not of the the given casting type");
                    return default;
                }
                return _component;
            }
            catch
            {
                Debug.Log($"The poolable object of type: {_type} does not seem to exist.", gameObject);
                return default;
            }
        }
    }
}