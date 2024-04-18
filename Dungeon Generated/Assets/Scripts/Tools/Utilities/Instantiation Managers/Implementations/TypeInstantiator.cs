using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Utilities.SpawnManager
{
    internal class TypeInstantiator : MonoBehaviour, ISpawnManager
    {
        [SerializeField] private Association[] m_types;

        private Dictionary<string, GameObject> m_prefabs    = new();
        private Dictionary<string, Transform> m_parents     = new();

        private void Awake()
        {
            foreach (var _type in m_types)
            {
                var parentObject = new GameObject();

                parentObject.name               = $"{_type.type} Parent";
                parentObject.transform.parent   = transform;

                m_prefabs.Add(_type.type, _type.prefab);
                m_parents.Add(_type.type, parentObject.transform);
            }
        }

        public GameObject Spawn(string _type, Vector3 _position, Quaternion _rotation)
        {
            try
            {
                return Instantiate(m_prefabs[_type], _position, _rotation, m_parents[_type]);
            }
            catch
            {
                Debug.Log($"The instantiatable of type: {_type} does not seem to exist.", gameObject);
                return default;
            }
        }

        public T Spawn<T>(string _type, Vector3 _position, Quaternion _rotation)
        {
            try
            {
                var spawnedObject = Instantiate(m_prefabs[_type], _position, _rotation, m_parents[_type]);

                if (!spawnedObject.TryGetComponent(out T _component))
                {
                    Debug.Log("The requested components can't be found on the instantiated object");
                    return default;
                }
                return _component;
            }
            catch
            {
                Debug.Log($"The instantiatable of type: {_type} does not seem to exist.", gameObject);
                return default;
            }
        }
    }
}
