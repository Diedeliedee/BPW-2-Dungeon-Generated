using DelaunatorSharp;
using Joeri.Tools.Debugging;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonGeneration
{
    public class Room : IPoint
    {
        private Vector2 m_center                                    = Vector2.zero;
        private Dictionary<int, Connection> m_connections           = new();
        private Dictionary<int, Connection> m_mainPathConnections   = new();

        public int width    { get; set; }
        public int height   { get; set; }

        public int xPos
        {
            get => Mathf.RoundToInt(center.x - (float)width / 2);
            set => m_center.x = value + (float)width / 2;
        }
        public int yPos
        {
            get => Mathf.RoundToInt(center.y - (float)height / 2);
            set => m_center.y = value + (float)height / 2;
        }

        public Vector2 center
        {
            get => m_center;
            set => m_center = value;
        }

        public Vector2 topLeft      => new(m_center.x - (float)width / 2, m_center.y + (float)height / 2);
        public Vector2 bottomRight  => new(m_center.x + (float)width / 2, m_center.y - (float)height / 2);

        public IEnumerable<Connection> connections => m_connections.Values;
        public IEnumerable<Connection> mainPath => m_mainPathConnections.Values;

        public double X { get => m_center.x; set => m_center.x = (float)value; }
        public double Y { get => m_center.y; set => m_center.y = (float)value; }

        public Room(int _width, int _height, int _xPos, int _yPos)
        {
            width   = _width;
            height  = _height;
            xPos    = _xPos;
            yPos    = _yPos;
        }

        public Room(int _width, int _height, Vector2 _center)
        {
            width   = _width;
            height  = _height;
            center  = _center;
        }

        public void Snap()
        {
            xPos = xPos;
            yPos = yPos;
        }

        public bool OverlapsWith(Room _other)
        {
            // if (right > other left) && ( left < other right)
            var overlapHorizontal   = bottomRight.x > _other.topLeft.x      && topLeft.x        < _other.bottomRight.x;

            // if (up > other bottom) && (down < other up)
            var overlapVertical     = topLeft.y     > _other.bottomRight.y  && bottomRight.y    < _other.topLeft.y;

            return overlapHorizontal && overlapVertical;
        }

        public void LinkRoom(Room _room)
        {
            if (m_connections.ContainsKey(_room.GetHashCode()))
            {
                Debug.Log($"Room: {GetHashCode()} is already linked with room: {_room.GetHashCode()}. Skipping..");
                return;
            }

            var connection = new Connection
            {
                offset  = _room.center - center,
                length  = Vector2.Distance(center, _room.center),
                start   = this,
                end     = _room
            };

            m_connections.Add(_room.GetHashCode(), connection);
        }

        public void CreatePath(Room _room)
        {
            var connection = m_connections[_room.GetHashCode()];

            m_mainPathConnections.Add(connection.end.GetHashCode(), connection);
        }

        public void Draw(Color _color)
        {
            GizmoTools.DrawOutlinedBox(new Vector3(center.x, center.y), new Vector3(width, height), _color, _color.a, true, 0.5f);
        }

        public static void Draw(Color _color, int _width, int _height)
        {
            GizmoTools.DrawOutlinedBox(new Vector3(0, 0), new Vector3(_width, _height), _color, _color.a, true, 0.5f);
        }

        public class Connection
        {
            public Vector2 offset;
            public float length;
            public Room start;
            public Room end;
        }
    }
}