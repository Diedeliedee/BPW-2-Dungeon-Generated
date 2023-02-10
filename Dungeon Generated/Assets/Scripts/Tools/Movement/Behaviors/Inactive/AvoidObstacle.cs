/*
using UnityEngine;

namespace Steering
{
    public class AvoidObstacle : Avoid
    {
        public override void StartBehavior(BehaviorContext context)
        {
            base.StartBehavior(context);
            m_obstacleLayer = LayerMask.GetMask(context.settings.obstacleLayer);
        }

        public override Vector3 CalculateSteeringForce(float deltaTime, BehaviorContext context)
        {
            //  Cast the sensor spherecast.
            SendSensor(out RaycastHit hitInfo, context);

            //  Reset obstacle position and return zero steering force if nothing is detected.
            if (!m_impendingCollision)
            {
                return Vector3.zero;
            }

            //  Calculating the values to avoid the obstacle with.
            var obstaclePosition = new Vector3(hitInfo.transform.position.x, context.position.y, hitInfo.transform.position.z);
            var offsetToHitPoint = m_hitPosition - obstaclePosition;

            //  The avoid fotcr will be in the direction from the obstacle's center to the end of it's sensor.
            SetAvoidForce(offsetToHitPoint.normalized, context.settings.avoidObstacleForce);

            PreventVelocityAlignment(context);

            return VelocityDifference(context);
        }
    }
}
*/