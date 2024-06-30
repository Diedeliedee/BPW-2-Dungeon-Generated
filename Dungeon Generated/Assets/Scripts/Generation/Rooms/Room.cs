using DelaunatorSharp;
using Joeri.Tools.Debugging;
using UnityEngine;

namespace DungeonGeneration
{
    public class Room : MonoBehaviour, ICompositePart, IPoint
    {
        [SerializeField] protected int m_width    = 2;
        [SerializeField] protected int m_height   = 2;
        [Space]
        [SerializeField] protected Color m_debugColor = new(1f, 1f, 1f, 0.25f);

        #region Properties
        public int width
        {
            get => m_width;
            set => m_width = value;
        }
        public int height
        {
            get => m_height;
            set => m_height = value;
        }

        public int xPos
        {
            get => Mathf.RoundToInt(center.x - (float)width / 2);
            set => center = new(value + (float)width / 2, center.y);
        }
        public int yPos
        {
            get => Mathf.RoundToInt(center.y - (float)height / 2);
            set => center = new(center.x, value + (float)height / 2);
        }

        public Vector2 center
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector2 topLeft      => new(center.x - (float)width / 2, center.y + (float)height / 2);
        public Vector2 bottomRight  => new(center.x + (float)width / 2, center.y - (float)height / 2);

        public double X { get => center.x; set => center = new((float)value, center.y); }
        public double Y { get => center.y; set => center = new(center.x, (float)value); }
        #endregion

        public void Setup(int _width, int _height, Vector2 _center)
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

        protected virtual void OnDrawGizmos()
        {
            GizmoTools.DrawOutlinedBox(new Vector3(center.x, center.y), new Vector3(width, height), m_debugColor, m_debugColor.a, true, 0.5f);
        }
    }
}