using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Dodelie.Tools;

[System.Serializable]
public class DungeonGenerator
{
    public int mapSize = 10;
    public Vector2Int minRoomScale = new Vector2Int(3, 3);
    public Vector2Int maxRoomScale = new Vector2Int(5, 5);
    public int roomCount = 10;

    private Room[] SpawnRooms()
    {
        var unsortedRooms = new List<Room>();
        var sortedRooms = new List<Room>();

        Room Furthest(List<Room> roomList)
        {
            Room furthest = null;
            float distance = 0f;

            foreach (var room in roomList)
            {
                var roomDistance = room.position.sqrMagnitude;

                if (roomDistance <= distance) continue;
                furthest = room;
                distance = roomDistance;
            }
            roomList.Remove(furthest);
            return furthest;
        }

        for (int i = 0; i < roomCount; i++)
        {
            //  Initializing rooms in a tight circle.
            var randomCirclePosition = Calc.RandomCirclePoint(mapSize);
            var position = new Vector2Int(Mathf.RoundToInt(randomCirclePosition.x), Mathf.RoundToInt(randomCirclePosition.y));
            var scale = new Vector2Int(Random.Range(minRoomScale.x, maxRoomScale.x + 1), Random.Range(minRoomScale.x, maxRoomScale.y + 1));

            unsortedRooms.Add(new Room(position, scale));
        }
        while (unsortedRooms.Count > 0)
        {
            sortedRooms.Add(Furthest(unsortedRooms));
        }
        foreach (var item in sortedRooms)
        {

        }
    }

    public void DrawGizmos()
    {
        foreach (var room in SpawnRooms())
        {


            GizmoTools.DrawOutlinedBox(Calc.FlatToVector(room.position), Calc.FlatToVector(room.size), Color.white, 1f, true, 0.25f);
        }
        GizmoTools.DrawCircle(Vector3.zero, mapSize, Color.red, 0.75f);
    }

    private class Room
    {
        public readonly Tile[] tiles = null;

        public Vector2Int position = Vector2Int.zero;
        public Vector2Int size = Vector2Int.zero;

        public Room(Vector2Int position, Vector2Int size)
        {
            this.position = position;
            this.size = size;
        }
    }

    private class Tile
    {
        public readonly Room parent = null;

        public Vector2Int position = Vector2Int.zero;

        public Tile(Vector2Int position, Room parent)
        {
            this.position = position;
            this.parent = parent;
        }
    }
}
