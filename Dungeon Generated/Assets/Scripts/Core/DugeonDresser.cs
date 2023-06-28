using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class DungeonDresser
{
    [Header("Layers:")]
    public Tilemap ground;
    public TileBase groundTile;

    public void Test(Dungeon dungeon)
    {
        ground.SetTile(new Vector3Int(0, 0), groundTile);
        ground.SetTile(new Vector3Int(-1, 1), groundTile);
    }

    public void Dress(Dungeon dungeon)
    {
        void DressTile(Vector2Int coords)
        {
            if (!dungeon.HasTile(coords)) return;
            ground.SetTile(new Vector3Int(coords.x, coords.y), groundTile);
        }

        dungeon.LoopTiles(DressTile);
    }
}
