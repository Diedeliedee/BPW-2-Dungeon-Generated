using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Debugging;

public class Tile
{
    public Vector2Int coordinates;

    public Vector2 position
    {
        get => new Vector2(coordinates.x + 0.5f, coordinates.y + 0.5f);
        set => coordinates = new Vector2Int(Mathf.FloorToInt(value.x), Mathf.FloorToInt(value.y));
    }

    public Tile(Vector2Int coords)
    {
        coordinates = coords;
    }

    public void Draw()
    {
        GizmoTools.DrawOutlinedBox(position, Vector2.one, Color.green, 0.5f, true);
    }
}
