using System.Collections.Generic;

namespace DungeonGeneration
{
    public static class RoomExtensions
    {
        public static Room GetClosest(this Room _room, List<Room> _other)
        {
            var closest         = _other[0];
            var cachedDistance  = 0f;

            for (int i = 0; i < _other.Count; i++)
            {
                if (_other.GetHashCode() == _room.GetHashCode()) continue;

                var distance = (_other[i].center - _room.center).sqrMagnitude;
                if (distance < cachedDistance)
                {
                    closest = _other[i];
                    cachedDistance = distance;
                }
            }
            return closest;
        }

        public static List<Room> FilterToNear(this Room _room, float _distance, List<Room> _other)
        {
            var list                = new List<Room>(_other.Count);
            var requiredDistance    = _distance;

            for (int i = 0; i < _other.Count; i++)
            {
                if (_other.GetHashCode() == _room.GetHashCode()) continue;
                var distance = (_other[i].center - _room.center).sqrMagnitude;
                if (distance <= requiredDistance) list.Add(_other[i]);
            }
            return list;
        }
    }
}