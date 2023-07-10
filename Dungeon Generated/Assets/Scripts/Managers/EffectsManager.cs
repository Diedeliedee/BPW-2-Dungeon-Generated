using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Utilities;

public class EffectsManager : MonoBehaviour
{
    public T Instantiate<T>(GameObject gameObject, Vector2Int coords, Vector2Int direction) where T : Object
    {
        var pos         = Dungeon.CoordsToPos(coords);
        var angle       = -Vectors.VectorToAngle(direction);
        var rotation    = Quaternion.Euler(0f, 0f, angle);

        if ( Instantiate(gameObject, pos, rotation, transform).TryGetComponent(out T t))
        {
            return t;
        }
        else
        {
            Debug.LogError($"Warning! Component not found on {gameObject.name}!", this.gameObject);
            return null;
        }
    }
}
