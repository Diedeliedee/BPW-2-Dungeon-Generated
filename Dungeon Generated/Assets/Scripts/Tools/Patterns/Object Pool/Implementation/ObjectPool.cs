using Joeri.Tools.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Joeri.Tools.Patterns.ObjectPool
{
    public class ObjectPool : IObjectPool
    {
        public readonly Transform root      = null;
        public readonly Transform parent    = null;
        public readonly GameObject item     = null;

        private int m_groupSize = 0;
        private bool m_autoGrow = false;

        private Stack<IPoolItem> m_inactiveItems    = new();
        private List<IPoolItem> m_activeItems       = new();

        public List<IPoolItem> activeItems { get => m_activeItems; }

        /// <summary>
        /// Create a new object pool.
        /// </summary>
        /// <param name="_itemToSpawn">The game object which will be stored in the pool.</param>
        /// <param name="_groupSize">The amount of items which will be stored in the pool, and will be the amount to increment with.</param>
        /// <param name="_autoGrow">Whether the pool should grow when the available items run out.</param>
        /// <param name="_root">The transform in which the inactive objects will be stored.</param>
        public ObjectPool(GameObject _itemToSpawn, int _groupSize, bool _autoGrow, Transform _root)
        {
            //  Setting variables.
            item        = _itemToSpawn;
            m_groupSize = _groupSize;
            m_autoGrow  = _autoGrow;
            root        = _root;

            //  Initializing.
            parent = new GameObject($"'{item.name}' Pool").transform;
            parent.parent = _root;
        }

        public GameObject Spawn(Vector3 _position, Quaternion _rotation, Transform _parent = null)
        {
            //  Autogrow if enabled.
            if (m_autoGrow && m_inactiveItems.Count == 0)   InstantiateItems();
            else                                            Debug.LogWarning("Object pool ran out of available items to spawn..", parent);

            //  If no parent is given, keep the item in the pool transform.
            if (_parent == null) _parent = parent;

            //  Pop an inactive item.
            var itemSpawned = m_inactiveItems.Pop();

            //  Configure it..
            itemSpawned.Spawn(_position, _rotation, _parent);
            m_activeItems.Add(itemSpawned);

            return itemSpawned.link;
        }

        public T Spawn<T>(Vector3 _position, Quaternion _rotation, Transform _parent = null) where T : IPoolItem
        {
            //  Autogrow if enabled.
            if (m_autoGrow && m_inactiveItems.Count == 0)   InstantiateItems();
            else                                            Debug.LogWarning("Object pool ran out of available items to spawn..", parent);

            //  If no parent is given, keep the item in the pool transform.
            if (_parent == null) _parent = parent;

            //  Pop an inactive item.
            var itemSpawned = m_inactiveItems.Pop();

            //  Configure it..
            itemSpawned.Spawn(_position, _rotation, _parent);
            m_activeItems.Add(itemSpawned);

            //  Guard against invalid casting type.
            if (itemSpawned is not T _type)
            {
                Debug.LogError("Spawned poolitem is not of the requested casting type! >:(");
                return default;
            }

            return _type;
        }

        public void Despawn(IPoolItem _item)
        {
            m_activeItems.Remove(_item);

            _item.Despawn();
            m_inactiveItems.Push(_item);
        }

        /// <summary>
        /// Warning. This function is relatively heavy, casting an IEnumerable multiple times. Use with caution.
        /// </summary>
        /// <returns>The items in either the active, or inactive list.</returns>
        public List<T> GetItems<T>(bool active, bool inactive) where T : MonoBehaviour, IPoolItem
        {
            if (!active && !inactive) return null;

            var itemList = new List<T>();

            if (active)     itemList.AddRange(Util.CastList<T, IPoolItem>(m_activeItems));
            if (inactive)   itemList.AddRange(Util.CastList<T, IPoolItem>(m_inactiveItems.ToList()));
            return          itemList;
        }

        /// <summary>
        /// Insatiates new instances of items, ready to be deployed.
        /// </summary>
        private void InstantiateItems()
        {
            for (int i = 0; i < m_groupSize; i++)
            {
                var component = Object.Instantiate(item, Vector3.zero, Quaternion.identity, parent).GetComponent<IPoolItem>();

                //  Check whether the item's component matches the generic type.
                if (i == 0 && component == null)
                {
                    Debug.LogError("Assigned item for object pool does not match the class' generic type.", parent);
                    return;
                }

                //  Call the OnCreate() function, and push it in the inactive list.
                component.Create(this, parent);
                m_inactiveItems.Push(component);
            }
        }
    }
}