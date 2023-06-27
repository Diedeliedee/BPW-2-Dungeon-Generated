using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools
{
    public struct Rectangle
    {
        public Vector2 position;
        public float width;
        public float height;

        #region Properties

        public Vector2 top          { get => GetOffset(-1, 0); }
        public Vector2 down         { get => GetOffset(1, 0); }
        public Vector2 left         { get => GetOffset(0, -1); }
        public Vector2 right        { get => GetOffset(0, 1); }

        public Vector2 topLeft      { get => GetOffset(-1, -1); }
        public Vector2 topRight     { get => GetOffset(1, -1); }
        public Vector2 bottomLeft   { get => GetOffset(-1, 1); }
        public Vector2 bottomRight  { get => GetOffset(1, 1); }

        #endregion

        public Rectangle(Vector2 pos, float w, float h)
        {
            position = pos;
            width = w;
            height = h;
        }

        public Rectangle(Vector2 pos, Vector2 size)
        {
            position = pos;
            width = size.x;
            height = size.y;
        }

        public Vector2 GetOffset(float xOffset, float yOffset)
        {
            return new Vector2(position.x + xOffset * (width / 2), position.y + yOffset * (height / 2));
        }

        public bool CollidesWith(Rectangle other)
        {
            return CollidesWith(other, out bool xCol, out bool yCol);
        }

        public bool CollidesWith(Rectangle other, out bool xCol, out bool yCol)
        {
            var topLeft             = this.topLeft;
            var bottomRight         = this.bottomRight;

            var otherTopLeft        = other.topLeft;
            var otherBottomRight    = other.bottomRight;

            xCol = bottomRight.x > otherTopLeft.x && topLeft.x < otherBottomRight.x;
            yCol = bottomRight.y > otherTopLeft.y && topLeft.y < otherBottomRight.y;

            return xCol && yCol;
        }
    }
}