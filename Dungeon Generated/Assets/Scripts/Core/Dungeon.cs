using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Joeri.Tools;
using Joeri.Tools.Debugging;

public class Dungeon : MonoBehaviour
{
    public int halfExtents = 10;
    [Space]
    public List<Room> rooms;

    [Header("Sub-components:")]
    [SerializeField] private DungeonGenerator m_generator;
    [SerializeField] private DungeonDresser m_dresser;

    //  References:
    private Tilemap m_tileMap = null;

    private void Awake()
    {
        m_tileMap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        rooms = m_generator.GetRawRooms();
        m_dresser.Dress(this);
    }

    private void Update()
    {
        //m_generator.Iterate(rooms);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying) Draw();
        else
        {
            var rooms = m_generator.GetRawRooms();

            void DrawTile(Vector2Int coords)
            {
                if (!HasTile(coords, rooms)) return;
                GizmoTools.DrawOutlinedBox(new Vector3(coords.x + 0.5f, coords.y + 0.5f), Vector2.one, Color.red, 0.5f, true);
            }

            Draw(rooms);
            LoopTiles(DrawTile);
        }
        GizmoTools.DrawOutlinedBox(Vector3.zero, new Vector2(halfExtents * 2, halfExtents * 2), Color.red);
    }

    public bool HasTile(Vector2Int coords)
    {
        return HasTile(coords, rooms);
    }

    public bool HasTile(Vector2Int coords, List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].HasTile(coords)) return true;
        }
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

    public void Draw()
    {

        if (rooms == null) return;
        for (int i = 0; i < rooms.Count; i++) rooms[i].Draw();
    }

    public static void Draw(List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count; i++) rooms[i].Draw();
    }
}
