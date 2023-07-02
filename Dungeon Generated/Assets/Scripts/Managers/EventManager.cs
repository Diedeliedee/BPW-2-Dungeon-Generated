using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EventManager
{
    public System.Action<Enemy> onPlayerSpotted = null;
    public System.Action<Enemy> onEnemyDespawn  = null;
}
