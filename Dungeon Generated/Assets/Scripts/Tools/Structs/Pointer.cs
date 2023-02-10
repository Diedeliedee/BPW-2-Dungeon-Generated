using UnityEngine;

namespace Dodelie.Tools
{
    public struct Pointer
    {
        public readonly Vector3 origin;
        public readonly Vector3 end;
        public readonly Vector3 offset;

        public float distance { get=> offset.magnitude; }
        public Vector3 direction { get => offset.normalized; }

        /// <summary>
        /// Create a new pointer using a position A, and a position B.
        /// </summary
        public Pointer(Vector3 positionA, Vector3 positionB)
        {
            origin  = positionA;
            end     = positionB;
            offset  = positionB - positionA;
        }

        /// <summary>
        /// Create a new pointer using an origin position, the direction, and the distance towards that direction.
        /// </summary>
        public Pointer(Vector3 position, Vector3 direction, float distance)
        {
            this = new Pointer(position, position + (direction * distance));
        }

        /// <summary>
        /// Create a new pointer sliding the existing origin, and start points along their direction.
        /// </summary>
        public Pointer SlidePointer(float originSlide, float endPointSlide)
        {
            var newOrigin = origin + (direction * originSlide);
            var newEndPoint = end + (direction * endPointSlide);

            return new Pointer(newOrigin, newEndPoint);
        }

        /// <summary>
        /// Changes the pointer's distance.
        /// </summary>
        public void SetNewDistance(float distance)
        {
            this = new Pointer(origin, direction, distance);
        }
    
        /// <summary>
        /// Treats the pointer like a raycast, and returns true if the raycast hit it's target.
        /// </summary>
        public bool HitByPointer(out RaycastHit hitInfo, LayerMask layerMask)
        {
            return Physics.Raycast(origin, direction, out hitInfo, distance, layerMask);
        }

        /// <summary>
        /// Draws the pointer as a line.
        /// </summary>
        public void Draw(Color color, float opacity = 1)
        {
            GizmoTools.DrawRay(origin, offset, color, opacity);
        }
    }
}
