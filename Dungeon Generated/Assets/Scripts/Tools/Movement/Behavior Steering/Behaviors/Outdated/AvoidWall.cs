/*
using UnityEngine;

namespace Steering
{
    public class AvoidWall : Avoid
    {
        public override void StartBehavior(BehaviorContext context)
        {
            base.StartBehavior(context);
            m_obstacleLayer = LayerMask.GetMask(context.settings.wallLayer);
        }

        public override Vector3 CalculateSteeringForce(float deltaTime, BehaviorContext context)
        {
            //  Cast the sensor spherecast.
            SendSensor(out RaycastHit hitInfo, context);

            //  Return zero steering force if nothing is detected.
            if (!m_impendingCollision)
            {
                return Vector3.zero;
            }

            //  The avoid force will be in the direction away from the wall.
            SetAvoidForce(hitInfo.normal, context.settings.avoidWallForce);

            PreventVelocityAlignment(context);

            return VelocityDifference(context);
        }
    }
}
*/