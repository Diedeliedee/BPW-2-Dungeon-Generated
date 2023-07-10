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
    [Space]
    public Tilemap walls;
    public TileBase wallTile;

    public void Dress(Dungeon dungeon)
    {
        void DressGround(Vector2Int coords)
        {
            if (!dungeon.HasTile(coords)) return;
            ground.SetTile(To3DCoords(coords), groundTile);
        }

        void DressWalls(Vector2Int coords)
        {
            var coords3 = To3DCoords(coords);

            foreach (var direction in Dungeon.allDirections)
            {
                var offsetCoords = coords3 + To3DCoords(direction);

                if (!ground.HasTile(offsetCoords) || ground.HasTile(coords3)) continue;

                walls.SetTile(coords3, wallTile);
            }
        }

        dungeon.LoopTiles(DressGround);
        dungeon.LoopTiles(DressWalls);
    }

    private Vector3Int To3DCoords(Vector2Int coords)
    {
        return new Vector3Int(coords.x, coords.y);
    }
}
