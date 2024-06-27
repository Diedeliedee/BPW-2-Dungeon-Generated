using UnityEngine;
using Joeri.Tools.Utilities;
using System.Collections.Generic;

namespace DungeonGeneration
{
    public class Spawner
    {
        private List<Room> m_rooms = new();

        public List<Room> rooms => m_rooms;

        public bool IterateSpawningRooms(GenerationSettings _settings)
        {
            var position    = Util.RandomCirclePoint() * _settings.circleRadius;
            var width       = Random.Range(_settings.minRoomWidth, _settings.maxRoomWidth);
            var height      = Random.Range(_settings.minRoomHeight, _settings.maxRoomHeight);

            m_rooms.Add(new Room(width, height, position));

            if (m_rooms.Count == _settings.rooms)   return true;
                                                    return false;
        }

        public void Draw(Color _color)
        {
            for (int i = 0; i < m_rooms.Count; i++)
            {
                m_rooms[i].Draw(_color);
            }
        }
    }
}
