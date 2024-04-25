using Joeri.Tools.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Separator
    {
        public List<Room> rooms { get; set; }

        public void SteerRoomAway(Room _room)
        {
            var overlappingRooms = GetOverlapping(_room);
            if (overlappingRooms.Count == 0) return;

            var offset = (_room.center - GetAveragePosition(overlappingRooms)).normalized * 0.1f;

            if (offset == Vector2.zero)
            {
                offset = Random.Range(0f, 360f).ToDirection();
            }

            _room.center += offset;
        }

        public bool OverlapsRemain()
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (OverlapsWithAny(rooms[i])) return true;
            }
            return false;
        }

        public bool OverlapsWithAny(Room _room)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                //  If the overlap room is the same as the iteration room, skip it.
                if (rooms[i].GetHashCode() == _room.GetHashCode()) continue;
                //  If it overlaps with any room.
                if (_room.OverlapsWith(rooms[i])) return true;
            }
            return false;
        }

        private List<Room> GetOverlapping(Room _room)
        {
            var overlappingRooms = new List<Room>();

            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].GetHashCode() == _room.GetHashCode()) continue;
                if (!_room.OverlapsWith(rooms[i])) continue;
                overlappingRooms.Add(rooms[i]);
            }
            return overlappingRooms;
        }

        private Vector2 GetAveragePosition(List<Room> _rooms)
        {
            var averagePosition = new Vector2();

            for (int i = 0; i < _rooms.Count; i++) averagePosition += _rooms[i].center;
            return averagePosition /= _rooms.Count;
        }
    }
}