using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools
{
    [System.Serializable]
    public struct BoxArea
    {
        public Vector3 position;
        public Vector3 size;

        public float upperBorder    { get => position.y + (size.y / 2); }
        public float lowerBorder    { get => position.y - (size.y / 2); }
        public float leftBorder     { get => position.x - (size.x / 2); }
        public float rightBorder    { get => position.x + (size.x / 2); }
        public float backBorder     { get => position.z + (size.z / 2); }
        public float frontBorder    { get => position.z - (size.z / 2); }

        public BoxArea(Vector3 position, Vector3 size)
        {
            this.position   = position;
            this.size       = size;
        }

        /// <summary>
        /// Draws the box.
        /// </summary>
        public void Draw(Color color, float opacity, bool solid = false, float solidOpacityMultiplier = 0.5f)
        {
            GizmoTools.DrawOutlinedBox(position, size, color, opacity, solid, solidOpacityMultiplier);
        }
    }
}