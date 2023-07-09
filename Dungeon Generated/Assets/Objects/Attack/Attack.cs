using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObjects/Attack", order = 0)]
public class Attack : ScriptableObject
{
    public int damage   = 5;
    public Sprite icon  = null;
    public Color color  = Color.white;

    public GameObject instance = null;
}
