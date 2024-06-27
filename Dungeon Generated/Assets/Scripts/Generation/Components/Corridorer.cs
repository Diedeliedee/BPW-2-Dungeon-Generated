using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Corridorer
    {
        private List<Room> m_corridors = new();

        public List<Room> corridors => m_corridors;

        public void IterateCorridoring(List<Room> _rooms, GenerationSettings _settings)
        {
            foreach (var room in _rooms)
            {
                foreach (var connection in room.mainPath)
                {
                    var horizontalDimensions    = GetCorridorDimensions(0, connection, _settings.corridorExtents);
                    var horizontalCenter        = GetCenter(0, connection);

                    var verticalDimensions      = GetCorridorDimensions(1, connection, _settings.corridorExtents);
                    var verticalCenter          = GetCenter(1, connection);

                    var horizontalCorridor  = new Room(horizontalDimensions.x, horizontalDimensions.y, horizontalCenter);
                    var verticalCorridor    = new Room(verticalDimensions.x, verticalDimensions.y, verticalCenter);

                    horizontalCorridor.Snap();
                    verticalCorridor.Snap();

                    m_corridors.Add(horizontalCorridor);
                    m_corridors.Add(verticalCorridor);
                }
            }
        }

        public void Draw(Color _color)
        {
            for (int i = 0; i < m_corridors.Count; i++)
            {
                m_corridors[i].Draw(_color);
            }
        }

        public Vector2Int GetCorridorDimensions(int _type, Room.Connection _connection, int _extents)
        {
            var dimensions = Vector2Int.zero;

            switch (_type)
            {
                case 0:
                    dimensions.x = Mathf.RoundToInt(_extents * 2 + Mathf.Abs(_connection.offset.x));
                    dimensions.y = Mathf.RoundToInt(_extents * 2);
                    break;

                case 1:
                    dimensions.x = Mathf.RoundToInt(_extents * 2);
                    dimensions.y = Mathf.RoundToInt(_extents * 2 + Mathf.Abs(_connection.offset.y));
                    break;
            }
            return dimensions;
        }

        public Vector2 GetCenter(int _type, Room.Connection _connection)
        {
            var center = Vector2.zero;

            switch (_type)
            {
                case 0:
                    center.x = _connection.start.center.x + _connection.offset.x / 2;
                    center.y = _connection.start.center.y;
                    break;

                case 1:
                    center.x = _connection.start.center.x + _connection.offset.x;
                    center.y = _connection.start.center.y + _connection.offset.y / 2;
                    break;
            }
            return center;
        }
    }
}