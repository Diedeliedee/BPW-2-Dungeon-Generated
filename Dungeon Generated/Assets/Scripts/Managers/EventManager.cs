using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EventManager
{
    public Action<Enemy> onPlayerSpotted = null;
    public Action<Enemy> onEnemyDespawn  = null;

    public Action<UnityEngine.Object, Vector2> onObjectClicked  = null;
    public Action<UnityEngine.Object, Vector2> onObjectDrag     = null;
    public Action<UnityEngine.Object, Vector2> onObjectReleased = null;
}
