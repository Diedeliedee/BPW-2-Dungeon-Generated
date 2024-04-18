using UnityEngine;

namespace Joeri.Tools.Patterns.ObjectPool
{
    public class PoolItem : MonoBehaviour, IPoolItem
    {
        private IObjectPool m_pool = null;
        private Transform m_origin = null;

        public GameObject link => gameObject;

        public void Create(IObjectPool _pool, Transform _origin)
        {
            m_pool = _pool;
            m_origin = _origin;
            gameObject.SetActive(false);
        }

        public void Despawn()
        {
            transform.parent = m_origin;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            gameObject.SetActive(false);
            OnDespawn();
        }

        public void Spawn(Vector3 _position, Quaternion _rotation, Transform _parent)
        {
            gameObject.SetActive(true);
            transform.parent    = _parent;
            transform.position  = _position;
            transform.rotation  = _rotation;
            OnSpawn();
        }

        public void RequestDespawn()
        {
            m_pool.Despawn(this);
        }

        protected virtual void OnSpawn() { }

        protected virtual void OnDespawn() { }
    }
}