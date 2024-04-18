using UnityEngine;

namespace Joeri.Tools.Patterns.ObjectPool
{
    public interface IPoolItem
    {
        public GameObject link { get; }

        public void Create(IObjectPool _pool, Transform _origin);

        public void Spawn(Vector3 _position, Quaternion _rotation, Transform _parent);

        public void Despawn();
    }
}