using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public class EntityManager : MonoBehaviour
{
    public Player player    = null;
    public Enemy[] enemies  = null;

    public void Setup()
    {
        player  = GetComponentInChildren<Player>();
        enemies = GetComponentsInChildren<Enemy>();
    }
}
