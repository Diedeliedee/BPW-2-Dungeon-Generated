/*
using UnityEngine;

namespace Steering
{
    public class Flee : Behavior
    {
        private Transform m_target;

        public Flee(Transform target)
        {
            m_target = target;
        }

        public override Vector3 CalculateSteeringForce(float deltaTime, BehaviorContext context)
        {
            //  Only update the target position if the follow transform is not null.
            if (m_target != null)
            {
                //  If stopping of this actions is enabled.
                if (context.settings.safetyDistance > 0)
                {
                    var distanceFromThreat = (m_target.position - context.position).magnitude;

                    //  Return a steering force of (0,0,0) if the object is further away from the threat than the safety distance,
                    if (distanceFromThreat > context.settings.safetyDistance)
                    {
                        SetTargetPosition(context.position, context);
                        return TargetToSteeringForce(context);
                    }
                }

                var targetDirection = (context.position - m_target.position).normalized;

                SetTargetPosition(context.position + (targetDirection * context.settings.maxDesiredVelocity), context);
            }

            return TargetToSteeringForce(context);
        }

        public override void DrawGizmos(BehaviorContext context)
        {
            base.DrawGizmos(context);

            //  Draw a circle around the threat indicating the safety distance.
            GizmoTools.DrawCircle(m_target.position, context.settings.safetyDistance, Color.red, 0.75f);
        }
    }
}
*/