using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private UnityEvent m_event;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_event.Invoke();
    }
}
