using UnityEngine;
using UnityEngine.Events;

public class KeyPressEventRelay : MonoBehaviour
{
    [SerializeField] private KeyCode m_key = KeyCode.None;
    [Space]
    [SerializeField] private UnityEvent m_onPress;
    [SerializeField] private UnityEvent m_onHold;
    [SerializeField] private UnityEvent m_onRelease;

    private void Update()
    {
        if (Input.GetKeyDown(m_key))    m_onPress.Invoke();
        if (Input.GetKey(m_key))        m_onHold.Invoke();
        if (Input.GetKeyUp(m_key))      m_onRelease.Invoke();
    }
}