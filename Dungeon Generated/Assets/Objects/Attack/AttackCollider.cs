using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private AttackInstance m_attackInstance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Entity entity)) return;
        m_attackInstance.OnEntityCaught(entity);
    }
}
