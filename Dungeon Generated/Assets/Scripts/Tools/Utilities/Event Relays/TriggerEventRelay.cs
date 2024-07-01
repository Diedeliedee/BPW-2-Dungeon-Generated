using UnityEngine;
using UnityEngine.Events;

public class TriggerEventRelay : MonoBehaviour
{
    public UnityEvent<Collider> onTriggerEnter;
    public UnityEvent<Collider> onTriggerExit;
    public UnityEvent<Collider> onTriggerStay;

    private void OnTriggerEnter2D(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }

    private void OnTriggerExit2D(Collider other)
    {
        onTriggerExit.Invoke(other);
    }

    private void OnTriggerStay2D(Collider other)
    {
        onTriggerStay.Invoke(other);
    }
}
