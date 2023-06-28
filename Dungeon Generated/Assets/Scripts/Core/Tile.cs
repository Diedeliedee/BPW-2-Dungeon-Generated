using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Debugging;

public class Tile
{
    public readonly Room parent = null;
    public readonly Vector2Int localCoordinates;

    public Vector2Int coordinates
    {
        get => localCoordinates + parent.position;
    }

    public Vector2 position
    {
        get => new Vector2(coordinates.x + 0.5f, coordinates.y + 0.5f);
    }

    public Tile(Room parent, Vector2Int coords)
    {
        this.parent         = parent;
        localCoordinates    = coords;
    }

    public void Draw(Color color, float opacity, bool solid = false, float solidOpactiyMultiplier = 0.5f)
    {
        GizmoTools.DrawOutlinedBox(position, Vector2.one, color, opacity, solid, solidOpactiyMultiplier);
    }
}
