using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMarker : MonoBehaviour
{
    //  Cache:
    private SpriteRenderer m_sprite = null;
    private Animator m_animator     = null;

    public void Setup()
    {
        m_sprite    = GetComponent<SpriteRenderer>();
        m_animator  = GetComponent<Animator>();
    }

    public void Tick(Vector2 worldPos)
    {
        transform.position = worldPos;
    }

    public void Activate(Attack attack, Vector2 worldPos)
    {
        gameObject.SetActive(true);
        m_animator.Play("Loop");

        transform.position  = worldPos;
        m_sprite.color      = attack.color;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);

        transform.position  = Vector2.zero;
        m_sprite.color      = Color.white;
    }
}
