

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Tilemap m_groundTilemap;
    [SerializeField] private TileBase m_groundTile;
     
    private Dictionary<Vector2Int, Tile> m_tileMap;

    public void CreateFromComposite(Dictionary<Vector2Int, Tile> _composite)
    {
        m_tileMap = _composite;

        foreach (var pair in m_tileMap)
        {
            var position = new Vector3Int(pair.Key.x, pair.Key.y, 0);

            m_groundTilemap.SetTile(position, m_groundTile);
        }
    }

}
