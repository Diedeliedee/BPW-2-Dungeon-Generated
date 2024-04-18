using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools.Movement
{
    public partial class Accel
    {
        public class Omni
        {
            public Vector3 velocity         = Vector3.zero;
            public Vector3 desiredVelocity  = Vector3.zero;

            /// <returns>The desired velocity based on the given parameters and current conditions.</returns>
            public Vector3 CalculateVelocity(Vector3 desiredVelocity, float grip, float deltaTime)
            {
                //  Calculating steering.
                var steering                        = desiredVelocity - velocity;
                if (grip < Mathf.Infinity) steering *= Mathf.Clamp01(grip * deltaTime);

                //  Calculating velocity.
                velocity += steering;

                //  Halting movement if slowing down, and below epsilon.
                var desiredMagn = desiredVelocity.sqrMagnitude;
                var currentMagn = velocity.sqrMagnitude;

                if (desiredMagn < currentMagn && currentMagn < m_epsilon) velocity = Vector3.zero;

                //  Returning velocity.
                this.desiredVelocity = desiredVelocity;
                return velocity;
            }

            /// <summary>
            /// Overload for CalculateVelocity(...) in which both input and speed are seperate parameters.
            /// </summary>
            public Vector3 CalculateVelocity(Vector3 input, float speed, float grip, float deltaTime)
            {
                return CalculateVelocity(Vector3.ClampMagnitude(input, 1f) * speed, grip, deltaTime);
            }

            /// <summary>
            /// Overload for CalculateVelocity(...) in which the velocity is calculated based on uncontrolled physics.
            /// </summary>
            public Vector3 CalculateVelocity(float drag, float deltaTime)
            {
                velocity = Vector3.ClampMagnitude(velocity, velocity.magnitude - drag * deltaTime);
                return velocity;
            }

            /// <summary>
            /// Draws the acceleration class as two omni-directional rays.
            /// </summary>
            public void Draw(Vector3 position, Color velocityColor, Color steeringColor, float opacity = 1f)
            {
                GizmoTools.DrawRay(position, desiredVelocity, steeringColor, opacity);
                GizmoTools.DrawRay(position, velocity, velocityColor, opacity);
            }
        }
    }
}