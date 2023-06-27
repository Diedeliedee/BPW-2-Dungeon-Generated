using UnityEngine;

namespace Joeri.Tools.Structure
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
                if (m_instance != null) Debug.LogWarning($"Singleton already has an instance set. Overriding..");
                m_instance = value;

            }
        }
    }
}
