using UnityEngine;
using UnityEngine.Events;

public class StateEventRelay : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onEnable;
    [SerializeField] private UnityEvent m_onDisable;

    public void OnEnable()
    {
        m_onEnable.Invoke();   
    }

    public void OnDisable()
    {
        m_onDisable.Invoke();
    }
}