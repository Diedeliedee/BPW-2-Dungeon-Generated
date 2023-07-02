using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public class EntityManager : MonoBehaviour
{
    private PlayerInstance m_playerInstance = null;
    private EnemyManager m_enemyManager     = null;

    public Player player        { get => m_playerInstance.player; }
    public List<Enemy> enemies  { get => m_enemyManager.enemies; }

    public void Setup()
    {
        m_playerInstance    = GetComponentInChildren<PlayerInstance>();
        m_enemyManager      = GetComponentInChildren<EnemyManager>();

        m_playerInstance    .Setup();
        m_enemyManager      .Setup();
    }
}
