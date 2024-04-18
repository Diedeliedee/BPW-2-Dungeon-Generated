/*
using UnityEngine;

namespace Steering
{
    public class Evade : Behavior
    {
        private Transform m_target;
        private Vector3 m_previousTargetPosition;
        private Vector3 m_futureTargetPosition;

        public Evade(Transform target)
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

                //  The target's velocity is the target's estimated position one second into the future, so multiply it by the look ahead time.
                m_futureTargetPosition = GetFutureTargetPosition(deltaTime, context);

                //  Set the target position to the current position added with the opposite of the future target position direction.
                var targetPointer = new Pointer(context.position, m_futureTargetPosition);
                var targetPosition = context.position + (-targetPointer.direction * context.settings.maxDesiredVelocity);

                SetTargetPosition(targetPosition, context);

                m_previousTargetPosition = m_target.position;
            }

            return TargetToSteeringForce(context);
        }

        private Vector3 GetFutureTargetPosition(float deltaTime, BehaviorContext context)
        {
            //  The velocity can be calculated by calculating the frame offset to velocity in m/s.
            var targetVelocity = (m_target.position - m_previousTargetPosition) / deltaTime;

            //  The target's velocity is the target's estimated position one second into the future, so multiply it by the look ahead time.
            return m_target.position + (targetVelocity * context.settings.lookAheadTime);
        }

        public override void DrawGizmos(BehaviorContext context)
        {
            base.DrawGizmos(context);

            //  Draw a line towards the expected target position.
            GizmoTools.DrawLine(context.position, m_futureTargetPosition, Color.gray);

            //  Draw a circle around the threat indicating the safety distance.
            GizmoTools.DrawCircle(m_target.position, context.settings.safetyDistance, Color.black, 0.75f);
        }
    }
}
*/