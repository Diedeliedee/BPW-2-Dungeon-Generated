using Joeri.Tools.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Separator
    {
        public void SteerRoomAway(Room _room, List<Room> _rooms)
        {
            var overlappingRooms = GetOverlapping(_room, _rooms);
            if (overlappingRooms.Count == 0) return;

            var offset = (_room.center - GetAveragePosition(overlappingRooms)).normalized * 0.1f;

            if (offset == Vector2.zero)
            {
                offset = Random.Range(0f, 360f).ToDirection();
            }

            _room.center += offset;
        }

        public bool OverlapsRemain(List<Room> _rooms)
        {
            for (int i = 0; i < _rooms.Count; i++)
            {
                if (OverlapsWithAny(_rooms[i], _rooms)) return true;
            }
            return false;
        }

        public bool OverlapsWithAny(Room _room, List<Room> _rooms)
        {
            for (int i = 0; i < _rooms.Count; i++)
            {
                //  If the overlap room is the same as the iteration room, skip it.
                if (_rooms[i].GetHashCode() == _room.GetHashCode()) continue;
                //  If it overlaps with any room.
                if (_room.OverlapsWith(_rooms[i])) return true;
            }
            return false;
        }

        private List<Room> GetOverlapping(Room _room, List<Room> _rooms)
        {
            var overlappingRooms = new List<Room>();

            for (int i = 0; i < _rooms.Count; i++)
            {
                if (_rooms[i].GetHashCode() == _room.GetHashCode()) continue;
                if (!_room.OverlapsWith(_rooms[i])) continue;
                overlappingRooms.Add(_rooms[i]);
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