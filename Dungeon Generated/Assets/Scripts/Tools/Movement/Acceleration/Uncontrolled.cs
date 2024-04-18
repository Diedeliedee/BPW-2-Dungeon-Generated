using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools.Movement
{
    public partial class Accel
    {
        /// <summary>
        /// Class handling the properties of velocity on a singular axis, only applicable in the air.
        /// </summary>
        public class Uncontrolled
        {
            public float acceleration   = 0f;
            public float velocity       = 0f;
            public float drag           = 0f;

            public Uncontrolled() { }

            public Uncontrolled(float initialAcceleration, float initialVelocity, float initialDrag)
            {
                acceleration    = initialAcceleration;
                velocity        = initialVelocity;
                drag            = initialDrag;
            }

            /// <summary>
            /// Iterates the velocity based on the current set properties.
            /// </summary>
            public float CalculateVelocity(float deltaTime)
            {
                velocity += acceleration * deltaTime;

                if (velocity == 0f) return 0f;

                var magnitude = Mathf.Abs(velocity);
                var direction = velocity / magnitude;

                magnitude -= drag * deltaTime;
                return direction * magnitude;
            }

            public void AddVelocity(float velocity)
            {
                this.velocity += velocity;
            }

            /// <summary>
            /// Draws the velocity as a vector in a given direction.
            /// </summary>
            public void Draw(Vector3 position, Vector3 direction, Color color, float opacity = 1f)
            {
                GizmoTools.DrawRay(position, direction * velocity, color, opacity);
            }
        }
    }
}