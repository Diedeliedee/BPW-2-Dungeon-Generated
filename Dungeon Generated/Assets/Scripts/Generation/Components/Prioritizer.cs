using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Prioritizer
    {
        public List<Room> GetMainRooms(List<Room> _rooms, float _multiplier)
        {
            var averageSize = new Vector2();
            var mainRooms   = new List<Room>();

            //  Getting the average size of all rooms
            for (int i = 0; i < _rooms.Count; i++)
            {
                averageSize += new Vector2(_rooms[i].width, _rooms[i].height);
            }
            averageSize /= _rooms.Count;

            //  Finding the rooms who are greater than this size.
            for (int i = 0; i < _rooms.Count; i++)
            {
                if (_rooms[i].width     < averageSize.x * _multiplier) continue;
                if (_rooms[i].height    < averageSize.y * _multiplier) continue;
                mainRooms.Add(_rooms[i]);
            }

            return mainRooms;
        }
    }
}