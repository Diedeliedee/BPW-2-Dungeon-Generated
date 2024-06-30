using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Includer
    {
        public void IncludeIntersectingRooms(List<Corridor> _corridors, List<Room> _rooms, List<Room> _includedRooms)
        {
            var intersectingRooms = new Dictionary<int, Room>();

            foreach (var corridor in _corridors)
            {
                foreach (var room in _rooms)
                {
                    if (!corridor.OverlapsWith(room))                       continue;
                    if (intersectingRooms.ContainsKey(room.GetHashCode()))  continue;

                    intersectingRooms.Add(room.GetHashCode(), room);
                }
            }
            _includedRooms.AddRange(intersectingRooms.Values);
        }
    }
}