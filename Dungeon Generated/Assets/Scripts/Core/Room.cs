﻿using System;
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

    public Dictionary<Vector2Int, Tile> tiles
    {
        get;
        private set;
    }

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
        void SetTile(Vector2Int coords)
        {
            tiles.Add(coords, new FloorTile(this, coords));
        }

        bounds = new Rectangle(pos, w, h);
        tiles = new Dictionary<Vector2Int, Tile>();
        LoopRoom(SetTile);
    }

    public bool HasTile(Vector2Int coords)
    {
        return HasTile(coords, out Tile tile);
    }

    public bool HasTile(Vector2Int coords, out Tile tile)
    {
        return tiles.TryGetValue(coords - position, out tile);
    }

    public void LoopRoom(System.Action<Vector2Int> onIterate)
    {
        for (int x = -width / 2; x < width / 2; x++)
        {
            for (int y = -height / 2; y < height / 2; y++)
            {
                onIterate.Invoke(new Vector2Int(x, y));
            }
        }
    }

    public void Draw(DrawStyle style)
    {
        var color = Color.white;
        var opacity = 0.5f;
        var solid = true;
        var solidOpacity = 0.25f;

        switch (style)
        {
            default: return;

            case DrawStyle.Entire:
                GizmoTools.DrawOutlinedBox(new Vector3(position.x, position.y), new Vector3(width, height), color, opacity, solid, solidOpacity);
                break;

            case DrawStyle.Tiles:
                foreach (var tile in tiles) tile.Value.Draw(color, opacity, solid, solidOpacity);
                break;
        }
    }

    public enum DrawStyle { None, Entire, Tiles }
}
