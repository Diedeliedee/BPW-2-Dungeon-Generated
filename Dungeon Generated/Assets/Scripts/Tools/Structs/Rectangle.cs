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

        public Vector2 top          { get => GetOffset(0, 1); }
        public Vector2 down         { get => GetOffset(0, -1); }
        public Vector2 left         { get => GetOffset(-1, 0); }
        public Vector2 right        { get => GetOffset(1, 0); }

        public Vector2 topLeft      { get => GetOffset(-1, 1); }
        public Vector2 topRight     { get => GetOffset(1, 1); }
        public Vector2 bottomLeft   { get => GetOffset(-1, -1); }
        public Vector2 bottomRight  { get => GetOffset(1, -1); }

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

        public bool CollidesWith(Vector2 point)
        {
            return CollidesWith(point, out bool xCol, out bool yCol);
        }

        public bool CollidesWith(Rectangle other, out bool xCol, out bool yCol)
        {
            var bottomLeft      = this.bottomLeft;
            var topRight        = this.topRight;

            var otherBottomLeft = other.bottomLeft;
            var otherTopRight   = other.topRight;

            xCol = topRight.x > otherBottomLeft.x && bottomLeft.x < otherTopRight.x;
            yCol = topRight.y > otherBottomLeft.y && bottomLeft.y < otherTopRight.y;

            return xCol && yCol;
        }

        public bool CollidesWith(Vector2 point, out bool xCol, out bool yCol)
        {
            var bottomLeft  = this.bottomLeft;
            var topRight    = this.topRight;

            xCol = point.x > bottomLeft.x && point.x < topRight.x;
            yCol = point.y > bottomLeft.y && point.y < topRight.y;

            return xCol && yCol;
        }
    }
}