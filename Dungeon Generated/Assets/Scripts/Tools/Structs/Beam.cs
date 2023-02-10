using UnityEngine;

namespace Dodelie.Tools
{
    public struct Beam
    {
        public Pointer pointer;
        public float radius { get; private set; }

        public Vector3 firstCirclePosition { get => pointer.origin + (pointer.direction * radius); }
        public Vector3 lastCirclePosition { get => pointer.end - (pointer.direction * radius); }
        public float distanceFromCircles { get => pointer.distance - beamWidth; }
        public float beamWidth { get => radius * 2; }

        /// <summary>
        /// Create a theoretical beam ranging from position A to position B.
        /// </summary>
        public Beam(Vector3 positionA, Vector3 positionB, float beamRadius)
        {
            pointer = new Pointer(positionA, positionB);
            radius = beamRadius;

            //  If the distance of the beam is shorter than it's width, it would create a beam not supported with this current logic.
            if (pointer.distance < beamWidth)
            {
                //  Set the distance of the beam to be it's with.
                pointer.SetNewDistance(beamWidth);
            }
        }

        /// <summary>
        /// Create a theoretical beam starting at the given position, and going in the given direction.
        /// </summary>
        public Beam(Vector3 position, Vector3 direction, float distance, float beamRadius)
        {
            this = new Beam(position, position + (direction * distance), beamRadius);
        }

        /// <summary>
        /// Checks whether the beam would hit an object in the given layermask. It also provides the hit info.
        /// </summary>
        public bool BeamHit(out RaycastHit hitInfo, LayerMask layerMask)
        {
            var hit = Physics.SphereCast(firstCirclePosition, radius, pointer.direction, out hitInfo, distanceFromCircles, layerMask);

            //  A spherecast won't return true if the distance of the from the hitpoint is smaller than it's width. A back-up raycast can be used.
            if (!hit)
            {
                hit = pointer.HitByPointer(out hitInfo, layerMask);
            }

            return hit;
        }

        /// <summary>
        /// Draws the beam as a flat ray of two connected circles.
        /// </summary>
        public void Draw(Color color, float opacity = 1)
        {
            GizmoTools.DrawWideLine(firstCirclePosition, radius, lastCirclePosition, color, opacity);
        }
    }
}
