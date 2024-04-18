using UnityEngine;

namespace Joeri.Tools.Patterns.ObjectPool
{
    public interface IObjectPool
    {
        /// <summary>
        /// Picks an item from the object pool, and 'spawns' it at the given location and rotation.
        /// </summary>
        /// <returns>The spawned object.</returns>
        public GameObject Spawn(Vector3 _position, Quaternion _rotation, Transform _parent = null);

        /// <summary>
        /// Picks an item from the object pool, and 'spawns' it at the given location and rotation.
        /// </summary>
        /// <typeparam name="T">The class type the spawned object will be casted to.</typeparam>
        /// <returns>The spawned object's casted class instance.</returns>
        public T Spawn<T>(Vector3 _position, Quaternion _rotation, Transform _parent = null) where T : IPoolItem;

        /// <summary>
        /// Despawns the item, and places it back into the object pool.
        /// </summary>
        public void Despawn(IPoolItem _item);
    }
}