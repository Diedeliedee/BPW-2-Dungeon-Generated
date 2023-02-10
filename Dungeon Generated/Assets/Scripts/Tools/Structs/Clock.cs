using UnityEngine;

namespace Dodelie.Tools
{
    public struct Clock
    {
        public Pointer pointer { get; private set; }
        public float angle { get; private set; }

        /// <summary>
        /// Create a theoretical clock at a position with a radius, and with a handle pointing with an angle from the top.
        /// </summary>
        public Clock(Vector3 clockPosition, float clockRadius, float pointerAngle)
        {
            ///  Cosine and sine both seem to convert radians to a number ranging from (-1 - 1), however the results are slightly shifted from another.
            ///  Wheras the cosine of PI is (-1), and the cosine of 0 is (1), the cosine of half of PI as radians, a.k.a 90 degrees returns (0).
            ///  This would mean that a wander angle of 0 would return an offset of (1,0), and an angle of 90 would return (0,1).

            var pointerRadians = pointerAngle * Mathf.Deg2Rad;
            var pointerDirection = new Vector3(Mathf.Cos(pointerRadians), 0, Mathf.Sin(pointerRadians));

            //  Using the Pointer struct for the clock handle.
            pointer = new Pointer(clockPosition, pointerDirection, clockRadius);
            angle = pointerAngle;
        }

        public void Draw(Color color, float opacity = 1)
        {
            GizmoTools.DrawCircle(pointer.origin, pointer.distance, color, opacity);
            GizmoTools.DrawLine(pointer.end, pointer.origin, color, opacity * 0.5f);
        }
    }
}
