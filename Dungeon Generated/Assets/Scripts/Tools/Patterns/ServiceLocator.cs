using UnityEngine;

namespace Joeri.Tools.Patterns
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        [SerializeField] private MonoBehaviour[] m_serviceItems;

        private Blackboard m_blackboard = new();

        private void Awake()
        {
            //  Setting singleton instance.
            instance = this;

            //  Registering servicable items.
            foreach (var serviceItem in m_serviceItems)
            {
                //  At the moment, service items are accessed through their game object's name. Be mindful of this.
                m_blackboard.Add(serviceItem, serviceItem.gameObject.name);
            }
        }

        public T Get<T>(string _name)
        {
            return m_blackboard.Get<T>(_name);
        }
    }
}
