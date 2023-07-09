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

    public Action onPlayerDeath = null;

    public Action<Character, Action> onTurnPrepare = null;

    public Action<Vector2> onPlayerClicked  = null;
    public Action<Vector2> onPlayerDragged  = null;
    public Action<Vector2> onPlayerReleased = null;
}
