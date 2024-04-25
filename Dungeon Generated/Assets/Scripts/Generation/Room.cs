using Joeri.Tools.Debugging;
using UnityEngine;

namespace DungeonGeneration
{
    public class Room : IRoom
    {
        private Vector2 m_center = Vector2.zero;

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

        public bool OverlapsWith(IRoom _other)
        {
            // if (right > other left) && ( left < other right)
            var overlapHorizontal   = bottomRight.x > _other.topLeft.x      && topLeft.x        < _other.bottomRight.x;

            // if (up > other bottom) && (down < other up)
            var overlapVertical     = topLeft.y     > _other.bottomRight.y  && bottomRight.y    < _other.topLeft.y;

            return overlapHorizontal && overlapVertical;
        }

        public void Draw(Color _color)
        {
            GizmoTools.DrawOutlinedBox(new Vector3(center.x, center.y), new Vector3(width, height), _color, _color.a, true, 0.5f);
        }

        public static void Draw(Color _color, int _width, int _height)
        {
            GizmoTools.DrawOutlinedBox(new Vector3(0, 0), new Vector3(_width, _height), _color, _color.a, true, 0.5f);
        }
    }
}