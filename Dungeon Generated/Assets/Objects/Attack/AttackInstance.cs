using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInstance : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    //  Properties:
    private int m_damage = 0;

    //  Events:
    private System.Action m_onFinish = null;

    public void Activate(int damage, System.Action onFinish)
    {
        m_damage    = damage;
        m_onFinish  = onFinish;
        m_animator.Play("Start");
    }

    public void OnEntityCaught(Entity entity)
    {
        entity.Damage(m_damage);
    }

    public void Recall()
    {
        m_onFinish?.Invoke();
        Destroy(gameObject);
    }
}
