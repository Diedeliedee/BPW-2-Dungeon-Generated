using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dodelie.Tools
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        public readonly Transform root      = null;
        public readonly Transform parent    = null;
        public readonly GameObject item     = null;

        private int m_groupSize             = 0;
        private bool m_autoGrow             = false;

        private Stack<T> m_inactiveItems    = new Stack<T>();
        private List<T> m_activeItems       = new List<T>();

        public List<T> activeItems { get => m_activeItems; }

        public ObjectPool(GameObject itemToSpawn, int groupSize, bool autoGrow, Transform root)
        {
            //  Setting variables.
            this.root = root;

            item        = itemToSpawn;
            m_groupSize = groupSize;
            m_autoGrow  = autoGrow;

            //  Initializing.
            parent = new GameObject($"'{item.name}' Pool").transform;
            parent.parent = root;
        }

        /// <summary>
        /// Picks an item from the object pool, and 'spawns' it at the given location and rotation.
        /// </summary>
        public T Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (m_autoGrow)
            {
                if (m_inactiveItems.Count == 0) InstantiateItems();
            }
            else
            {
                Debug.LogWarning("Object pool ran out of available items to spawn..", this.parent);
            }
            if (parent == null) parent = this.parent;

            var itemSpawned = m_inactiveItems.Pop();

            itemSpawned.gameObject.SetActive(true);
            itemSpawned.transform.parent    = parent;
            itemSpawned.transform.position  = position;
            itemSpawned.transform.rotation  = rotation;

            m_activeItems.Add(itemSpawned);

            return itemSpawned;
        }

        /// <summary>
        /// Despawns the item, and places it back into the object pool.
        /// </summary>
        public T Despawn(T item)
        {
            m_activeItems.Remove(item);

            item.transform.parent   = parent;
            item.transform.position = Vector3.zero;
            item.transform.rotation = Quaternion.identity;
            item.gameObject.SetActive(false);

            m_inactiveItems.Push(item);

            return item;
        }

        /// <summary>
        /// Insatiates new instances of items, ready to be deployed.
        /// </summary>
        private void InstantiateItems()
        {
            for (int i = 0; i < m_groupSize; i++)
            {
                var component = Object.Instantiate(item, Vector3.zero, Quaternion.identity, parent).GetComponent<T>();

                //  Check whether the item's component matches the generic type.
                if (i == 0 && component == null)
                {
                    Debug.LogError("Assigned item for object pool does not match the class' generic type.", parent);
                    return;
                }

                //  Deactivate the game object, and add to the list.
                component.gameObject.SetActive(false);
                m_inactiveItems.Push(component);
            }
        }

        public List<T> GetItems(bool active, bool inactive)
        {
            if (!active && !inactive) return null;

            var itemList    = new List<T>();

            if (active)     itemList.AddRange(m_activeItems);
            if (inactive)   itemList.AddRange(m_inactiveItems);
            return          itemList;
        }
    }
}