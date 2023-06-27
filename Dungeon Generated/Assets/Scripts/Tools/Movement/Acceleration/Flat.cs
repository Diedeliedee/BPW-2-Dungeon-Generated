using UnityEngine;
using Joeri.Tools.Debugging;
using Joeri.Tools.Utilities;

namespace Joeri.Tools.Movement
{
    public partial class Accel
    {
        public class Flat
        {
            public Vector2 velocity = Vector2.zero;
            public Vector2 desiredVelocity = Vector2.zero;

            /// <returns>The desired velocity based on the given parameters and current conditions.</returns>
            public Vector2 CalculateVelocity(Vector2 desiredVelocity, float grip, float deltaTime)
            {
                //  Calculating steering.
                var steering = desiredVelocity - velocity;
                steering *= Mathf.Clamp01(grip * deltaTime);

                //  Calculating velocity.
                velocity += steering;

                //  Halting movement if slowing down, and below epsilon.
                var desiredMagn = desiredVelocity.sqrMagnitude;
                var currentMagn = velocity.sqrMagnitude;

                if (desiredMagn < currentMagn && currentMagn < m_epsilon) velocity = Vector2.zero;

                //  Returning velocity.
                return velocity;
            }

            /// <summary>
            /// Overload for CalculateVelocity(...) in which both input and speed are seperate parameters.
            /// </summary>
            public Vector2 CalculateVelocity(Vector2 input, float speed, float grip, float deltaTime)
            {
                return CalculateVelocity(Vector2.ClampMagnitude(input, 1f) * speed, grip, deltaTime);
            }

            /// <summary>
            /// Overload for CalculateVelocity(...) in which the velocity is calculated based on uncontrolled physics.
            /// </summary>
            public Vector2 CalculateVelocity(float drag, float deltaTime)
            {
                velocity = Vector2.ClampMagnitude(velocity, velocity.magnitude - drag * deltaTime);
                return velocity;
            }

            /// <summary>
            /// Draws the acceleration class as two flat rays.
            /// </summary>
            public void Draw(Vector3 position, Color velocityColor, Color steeringColor, float opacity = 1f)
            {
                var desiredRay = Vectors.FlatToVector(desiredVelocity);
                var velocityRay = Vectors.FlatToVector(velocity);

                GizmoTools.DrawRay(position, desiredRay, steeringColor, opacity);
                GizmoTools.DrawRay(position, velocityRay, velocityColor, opacity);
            }
        }
    }
}