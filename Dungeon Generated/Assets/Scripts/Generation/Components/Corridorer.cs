using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Corridorer
    {
        public void IterateCorridoring(List<MainRoom> _rooms, List<Corridor> _corridors, GenerationSettings _settings)
        {
            foreach (var room in _rooms)
            {
                foreach (var connection in room.mainPath)
                {
                    var horizontalDimensions    = GetCorridorDimensions(0, connection, _settings.corridorExtents);
                    var horizontalCenter        = GetCenter(0, connection);

                    var verticalDimensions      = GetCorridorDimensions(1, connection, _settings.corridorExtents);
                    var verticalCenter          = GetCenter(1, connection);

                    var horizontalCorridor  = new Corridor(horizontalDimensions.x, horizontalDimensions.y, horizontalCenter);
                    var verticalCorridor    = new Corridor(verticalDimensions.x, verticalDimensions.y, verticalCenter);

                    horizontalCorridor.Snap();
                    verticalCorridor.Snap();

                    _corridors.Add(horizontalCorridor);
                    _corridors.Add(verticalCorridor);
                }
            }
        }

        public Vector2Int GetCorridorDimensions(int _type, MainRoom.Connection _connection, int _extents)
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

        public Vector2 GetCenter(int _type, MainRoom.Connection _connection)
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