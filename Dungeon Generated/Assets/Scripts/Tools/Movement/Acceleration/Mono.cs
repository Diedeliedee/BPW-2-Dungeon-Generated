using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools.Movement
{
    public partial class Accel
    {
        public class Mono
        {
            public float velocity = 0f;
            public float desiredVelocity = 0f;

            /// <returns>The desired velocity based on the given parameters and current conditions.</returns>
            public float CalculateVelocity(float desiredVelocity, float grip, float deltaTime)
            {
                //  Calculating steering.
                var steering                        = desiredVelocity - velocity;
                if (grip < Mathf.Infinity) steering *= Mathf.Clamp01(grip * deltaTime);

                //  Calculating velocity.
                velocity += steering;

                //  Halting movement if slowing down, and below epsilon.
                var desiredMagn = Mathf.Abs(desiredVelocity);
                var currentMagn = Mathf.Abs(velocity);

                if (desiredMagn < currentMagn && currentMagn < m_epsilon) velocity = 0f;

                //  Returning velocity.
                this.desiredVelocity = desiredVelocity;
                return velocity;
            }

            /// <summary>
            /// Overload for CalculateVelocity(...) in which both input and speed are seperate parameters.
            /// </summary>
            public float CalculateVelocity(float input, float speed, float grip, float deltaTime)
            {
                return CalculateVelocity(Mathf.Clamp(input, -1f, 1f) * speed, grip, deltaTime);
            }

            /// <summary>
            /// Overload for CalculateVelocity(...) in which the velocity is calculated based on uncontrolled physics.
            /// </summary>
            public float CalculateVelocity(float drag, float deltaTime)
            {
                if (velocity == 0f) return 0f;

                var magnitude = Mathf.Abs(velocity);
                var direction = velocity / magnitude;

                magnitude -= drag * deltaTime;
                return direction * magnitude;
            }

            /// <summary>
            /// Draws the acceleration class as two flat rays.
            /// </summary>
            public void Draw(Vector3 position, Vector3 direction, Color velocityColor, Color steeringColor, float opacity = 1f)
            {
                var desiredRay = direction.normalized * desiredVelocity;
                var velocityRay = direction.normalized * velocity;

                GizmoTools.DrawRay(position + velocityRay, desiredRay, steeringColor, opacity);
                GizmoTools.DrawRay(position, velocityRay, velocityColor, opacity);
            }
        }
    }
}
