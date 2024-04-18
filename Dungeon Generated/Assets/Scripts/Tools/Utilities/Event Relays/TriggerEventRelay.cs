using UnityEngine;
using UnityEngine.Events;

public class TriggerEventRelay : MonoBehaviour
{
    public UnityEvent<Collider> onTriggerEnter;
    public UnityEvent<Collider> onTriggerExit;
    public UnityEvent<Collider> onTriggerStay;

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        onTriggerStay.Invoke(other);
    }
}
