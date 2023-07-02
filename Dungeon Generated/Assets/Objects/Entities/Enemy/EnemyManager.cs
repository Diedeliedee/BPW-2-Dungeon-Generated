using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<Enemy> m_enemies = null;

    public List<Enemy> enemies { get => m_enemies; }

    public void Setup()
    {
        m_enemies = GetComponentsInChildren<Enemy>().ToList();
    }
}