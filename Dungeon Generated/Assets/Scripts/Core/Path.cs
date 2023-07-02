using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct Path
{
    public readonly Vector2Int first;
    public readonly Vector2Int last;
    public readonly Vector2Int[] coordinates;

    public Path(Vector2Int from, Vector2Int to, Dungeon dungeon)
    {
        var offset          = new LockedVector(to - from);
        var stepAmount      = Mathf.Abs(offset.value) + 1;

        var coordinatesList = new List<Vector2Int>();

        first   = from;
        last    = from + offset.ToVector();

        for (int i = 0; i < stepAmount; i++)
        {
            var coordinate = from + (offset.normalized * i).ToVector();

            if (i > 0 && !dungeon.HasTile(coordinate))
            {
                last = coordinatesList[i -1];
                break;
            }
            coordinatesList.Add(coordinate);
        }
        coordinates = coordinatesList.ToArray();
    }
}
