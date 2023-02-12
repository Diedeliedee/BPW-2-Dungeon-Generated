using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform m_player = null;

    public void Initialize(Transform player)
    {
        m_player = player;
    }

    public void Tick(float deltaTime)
    {
        transform.LookAt(m_player);
    }
}
