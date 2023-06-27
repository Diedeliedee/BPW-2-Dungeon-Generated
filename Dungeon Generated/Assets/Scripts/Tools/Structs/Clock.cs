using UnityEngine;
using Joeri.Tools.Utilities;
using Joeri.Tools.Debugging;

namespace Joeri.Tools
{
    public struct Clock
    {
        public Pointer pointer { get; private set; }
        public float angle { get; private set; }

        /// <summary>
        /// Create a theoretical clock at a position with a radius, and with a hand pointing with an angle from the top.
        /// </summary>
        public Clock(Vector3 position, float radius, float handAngle)
        {
            var handDirection = Vectors.AngleToVector(handAngle);

            //  Using the Pointer struct for the clock handle.
            pointer = new Pointer(position, new Vector3(position.x + handDirection.x, position.y, position.z + handDirection.y), radius);
            angle = handAngle;
        }

        public void Draw(Color color, float opacity = 1)
        {
            GizmoTools.DrawCircle(pointer.origin, pointer.distance, color, opacity);
            GizmoTools.DrawLine(pointer.end, pointer.origin, color, opacity * 0.5f);
        }
    }
}
