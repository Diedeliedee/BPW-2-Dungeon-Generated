using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Prioritizer
    {
        private List<Room> m_mainRooms = new();

        public List<Room> mainRooms => m_mainRooms;

        public void CacheMainRooms(List<Room> _rooms, float _multiplier)
        {
            var averageSize = new Vector2();

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
                m_mainRooms.Add(_rooms[i]);
            }
        }

        public void Draw(Color _color)
        {
            _color.r = 1f;

            for (int i = 0; i < m_mainRooms.Count; i++)
            {
                m_mainRooms[i].Draw(_color);
            }
        }
    }
}