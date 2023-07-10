using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Joeri.Tools;
using Joeri.Tools.Structure;
using Joeri.Tools.Debugging;

public class Dungeon : MonoBehaviour
{
    public int halfExtents = 10;
    [Space]
    public List<Room> rooms;

    [Header("Sub-components:")]
    [SerializeField] private DungeonGenerator m_generator;
    [SerializeField] private DungeonDresser m_dresser;

    [Header("Debug:")]
    [SerializeField] Room.DrawStyle m_roomsDrawSyle = Room.DrawStyle.Entire;

    //  Properties:
    public Vector2Int[] allowedDirections = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public static Vector2Int[] allDirections = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
        new Vector2Int(1, -1),
        new Vector2Int(1, 1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1),
    };

    //  References:
    private Tilemap m_tileMap = null;

    public void Setup()
    {
        m_tileMap   = GetComponent<Tilemap>();
        rooms       = m_generator.Generate();

        m_dresser.Dress(this);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)  Draw();
        else                        Draw(m_generator.GetRawRooms());
        GizmoTools.DrawOutlinedBox(Vector3.zero, new Vector2(halfExtents * 2, halfExtents * 2), Color.red);
    }

    public bool HasTile(Vector2Int coords)
    {
        return HasTile(coords, rooms, out Tile tile);
    }

    public bool HasTile(Vector2Int coords, out Tile tile)
    {
        return HasTile(coords, rooms, out tile);
    }

    public bool HasTile(Vector2Int coords, List<Room> rooms, out Tile tile)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].HasTile(coords, out tile)) return true;
        }
        tile = null;
        return false;
    }

    public void LoopTiles(System.Action<Vector2Int> onIterate)
    {
        for (int x = -halfExtents; x < halfExtents; x++)
        {
            for (int y = -halfExtents; y < halfExtents; y++)
            {
                onIterate.Invoke(new Vector2Int(x, y));
            }
        }
    }

    /// <returns>The coordinates of the "tile" the passed in position resides in.</returns>
    public static Vector2Int PosToCoords(Vector2 pos)
    {
        return new Vector2Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }

    /// <returns>The position in the center of the passed in tile's coordinates.</returns>
    public static Vector2 CoordsToPos(Vector2Int coords)
    {
        return new Vector2(coords.x + 0.5f, coords.y + 0.5f);
    }

    /// <returns>The position in the center of the passed in tile's coordinates, but using a Vector2 instead.</returns>
    public static Vector2 CoordsToPos(Vector2 coords)
    {
        return new Vector2(coords.x + 0.5f, coords.y + 0.5f);
    }

    public static Vector2Int GetGeneralDirection(Vector2Int offset)
    {
        if (Mathf.Abs(offset.x) < Mathf.Abs(offset.y))
        {
            offset.x = 0;
            offset.y = Mathf.Clamp(offset.y, -1, 1);
        }
        else
        {
            offset.y = 0;
            offset.x = Mathf.Clamp(offset.x, -1, 1);
        }
        return offset;
    }

    public void Draw()
    {
        if (rooms == null) return;
        for (int i = 0; i < rooms.Count; i++) rooms[i].Draw(m_roomsDrawSyle);
    }

    public void Draw(List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count; i++) rooms[i].Draw(m_roomsDrawSyle);
    }

    
}
