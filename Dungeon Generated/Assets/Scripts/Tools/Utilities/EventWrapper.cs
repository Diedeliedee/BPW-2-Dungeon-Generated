using UnityEngine;

namespace Joeri.Tools.Utilities
{
    public class EventWrapper
    {
        private readonly string m_eventName     = default;
        private System.Action m_event           = null;

        public int subscribers
        {
            get
            {
                if (m_event == null) return 0;
                return m_event.GetInvocationList().Length;
            }
        }

        public EventWrapper()
        {
            m_eventName = "Unnamed Event";
        }

        public EventWrapper(string _eventName)
        {
            m_eventName = _eventName;
        }

        public void Invoke()
        {
            if (m_event == null)
            {
                Debug.LogWarning($"Warning, event: {m_eventName} called without any subscribers. Please heed caution!");
                return;
            }

            m_event.Invoke();
        }

        public void Subscribe(System.Action _action)
        {
            m_event += _action;
        }

        public void Unsubscribe(System.Action _action)
        {
            m_event -= _action;
        }

        public void Clear()
        {
            m_event = null;
        }
    }
}
