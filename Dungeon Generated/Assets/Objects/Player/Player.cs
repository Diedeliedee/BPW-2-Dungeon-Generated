using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

public partial class Player : MonoBehaviour
{
    [SerializeField] private State<Player>[] m_states = new State<Player>[1];

    private FSM<Player> m_fsm = null;

    public void Initialize()
    {
        m_fsm = new FSM<Player>(this, m_states[0].GetType(), m_states);
    }

    public void Tick(float deltaTime)
    {
        m_fsm.Tick(deltaTime);
    }
}
