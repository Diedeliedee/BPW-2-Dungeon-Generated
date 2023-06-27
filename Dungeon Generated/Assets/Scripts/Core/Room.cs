using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools;
using Joeri.Tools.Debugging;

public class Room
{
    public Rectangle bounds;

    public Vector2Int position 
    { 
        get => new Vector2Int(Mathf.RoundToInt(bounds.position.x), Mathf.RoundToInt(bounds.position.y));
        set => bounds.position = value; 
    }

    public int width
    {
        get => Mathf.RoundToInt(bounds.width);
        set => bounds.width = value;
    }

    public int height
    {
        get => Mathf.RoundToInt(bounds.height);
        set => bounds.height = value;
    }

    public Room(Vector2Int pos, int w, int h)
    {
        bounds = new Rectangle(pos, w, h);
    }

    public void Draw()
    {
        GizmoTools.DrawOutlinedBox(new Vector3(position.x, position.y), new Vector3(width, height), Color.white, 1f, true, 0.25f);
    }
}