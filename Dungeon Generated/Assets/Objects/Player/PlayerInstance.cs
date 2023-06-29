using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    private Player      m_player;
    private PlayerInput m_input;

    public void Setup()
    {
        m_player    = GetComponentInChildren<Player>();
        m_input     = GetComponentInChildren<PlayerInput>();

        m_player.Setup();
        m_input.Setup(m_player);
    }
}
