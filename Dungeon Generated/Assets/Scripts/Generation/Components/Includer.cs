using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Includer
    {
        private Dictionary<int, Room> m_intersectingRooms = new();

        public IEnumerable<Room> intersectingRooms => m_intersectingRooms.Values;

        public void IncludeIntersectingRooms(List<Room> corridors, List<Room> rooms)
        {
            foreach (var corridor in corridors)
            {
                foreach (var room in rooms)
                {
                    if (!corridor.OverlapsWith(room))                           continue;
                    if (m_intersectingRooms.ContainsKey(room.GetHashCode()))    continue;

                    m_intersectingRooms.Add(room.GetHashCode(), room);
                }
            }
        }

        public void Draw(Color _color)
        {
            foreach (var room in m_intersectingRooms.Values)
            {
                room.Draw(_color);
            }
        }
    }
}