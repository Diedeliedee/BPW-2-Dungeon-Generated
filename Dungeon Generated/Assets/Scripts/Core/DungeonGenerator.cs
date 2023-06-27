using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;

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
        void CheckForOverlap(Room anchor)
        {
            //  Iterate through all other rooms, and cache the ones colliding with the current room.
            foreach (var other in rooms)
            {
                if (other == anchor) continue;                              //  Skip collision with itself.
                if (!anchor.bounds.CollidesWith(other.bounds)) continue;    //  Continue if no collision is found.
                IterateOverlap(anchor, other);
            }
        }

        void IterateOverlap(Room anchor, Room other)
        {
            var offset = other.position - anchor.position;

            //  If the rooms are in the exact same spot, create a random direction to move it towards, otherwise, clamp the offset.
            if (offset == Vector2Int.zero)
            {
                var randX = Random.Range(-1, 1);
                var randY = Random.Range(-1, 1);

                //  In the case that both values are still zero, assign a random direction to either.
                if (randX == 0 & randY == 0)
                {
                    if (Util.RandomChance(0.5f))    randX = Util.RandomChance(0.5f) ? 1 : -1;
                    else                            randY = Util.RandomChance(0.5f) ? 1 : -1;
                }

                offset = new Vector2Int(randX, randY);
            }
            else
            {
                offset.x = Mathf.Clamp(offset.x, -1, 1);
                offset.y = Mathf.Clamp(offset.y, -1, 1);
            }

            //  Assign offset to other room's position.
            other.position += offset;

            //  If there is still collision between the other room and the anchor, iterate again.
            if (anchor.bounds.CollidesWith(other.bounds)) IterateOverlap(anchor, other);

            //  Check if the moved room has any new overlaps to be fixed.
            CheckForOverlap(other);

        }

        //  Iterate the current designated room.
        CheckForOverlap(rooms[0]);
    }
}