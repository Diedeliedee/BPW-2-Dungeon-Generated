using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class DungeonGenerator
{
    public int roomCount;
    public Vector2Int minSize, maxSize;

    private int roomIteration = 0;
    private int collissionIteration = 0;

    public List<Room> GetRawRooms()
    {
        var rooms = new List<Room>();

        for (int i = 0; i < roomCount; i++)
        {
            var width = Random.Range(minSize.x / 2, maxSize.x / 2) * 2;
            var height = Random.Range(minSize.y / 2, maxSize.y / 2) * 2;

            rooms.Add(new Room(Vector2Int.zero, width, height));
        }
        return rooms;
    }

    public void Iterate(List<Room> rooms)
    {
        var room = rooms[roomIteration];

        int GetShiftOnAxis(int axisPos, int otherAxisPos)
        {
            if (axisPos == otherAxisPos) return Random.Range(0f, 1f) > 0.5f ? 1 : -1;
            return Mathf.Clamp(axisPos - otherAxisPos, -1, 1);
        }

        foreach (var other in rooms)
        {
            if (other == room) continue;                                                            //  Skip collision with itself.
            if (!room.bounds.CollidesWith(other.bounds, out bool xCol, out bool yCol)) continue;    //  Continue if no collision is found.

            if (xCol) room.bounds.position.x += GetShiftOnAxis(room.position.x, other.position.x);
            if (yCol) room.bounds.position.y += GetShiftOnAxis(room.position.y, other.position.y);
        }
        if (++roomIteration >= rooms.Count) roomIteration = 0;
    }
}