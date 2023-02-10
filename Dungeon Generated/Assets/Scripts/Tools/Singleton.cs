using UnityEngine;

namespace Dodelie.Tools
{
    public class Singleton<T> : MonoBehaviour
    {
        private static T m_instance;

        public static T instance
        {
            get
            {
                return m_instance;
            }
            set
            {
                if (m_instance == null)
                {
                    m_instance = value;
                    return;
                }

                Debug.LogError($"Singleton already has an instance set.");
            }
        }
    }
}
