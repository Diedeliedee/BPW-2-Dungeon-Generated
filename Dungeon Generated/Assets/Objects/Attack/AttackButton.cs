using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private Attack m_attachedAttack;

    public void FireAttackEvent()
    {
        GameManager.instance.events.onAttackSelected?.Invoke(m_attachedAttack);
    }
}
