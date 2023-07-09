using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInstance : MonoBehaviour
{
    private int m_damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Entity entity)) return;
        entity.Damage(m_damage);
    }

    public void Recall()
    {
        Destroy(gameObject);
    }
}
