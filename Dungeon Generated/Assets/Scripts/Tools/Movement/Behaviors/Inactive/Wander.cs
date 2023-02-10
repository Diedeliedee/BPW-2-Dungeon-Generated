/*
using UnityEngine;

namespace Dodelie.Tools
{
    public class Wander : Behavior
    {
        private Clock m_wanderClock;

        public Wander()
        {
            m_wanderClock = new Clock(Vector3.zero, 0, Random.Range(0f, 360f));
        }

        public Wander(Transform transform)
        {
            m_wanderClock = new Clock(Vector3.zero, 0, transform.eulerAngles.y);
        }

        public override Vector2 GetDesiredVelocity(Context context)
        {
            //  Adding a random offset to the current angle.
            var halfAngle       = context.settings.wanderAngle / 2;
            var wanderAngle     = Random.Range(-halfAngle, halfAngle);

            //  Calculating the circle position based on the current velocity and the desired circle distance.
            var circlePosition  = context.position + (context.velocity.normalized * context.settings.wanderDistance);
        }

        public override Vector3 CalculateSteeringForce(float deltaTime, BehaviorContext context)
        {
            //  Adding a random offset to the current angle.
            var halfAngle = context.settings.wanderAngle / 2;
            var wanderAngle = Random.Range(-halfAngle, halfAngle);

            //  Calculating the circle position based on the current velocity and the desired circle distance.
            var circlePosition = context.position + (context.velocity.normalized * context.settings.wanderDistance);

            //  Creating a new clock struct to calculate the end point from a position and radius.
            m_wanderClock = new Clock(circlePosition, context.settings.wanderRadius, m_wanderClock.angle + wanderAngle);

            //  Applying the end point of the clock as the target position.
            SetTargetPosition(m_wanderClock.pointer.endPoint, context);

            return TargetToSteeringForce(context);
        }

        public override void DrawGizmos(Vector3 position)
        {
            m_wanderClock.Draw(Color.white, 1);
        }
    }
}
*/