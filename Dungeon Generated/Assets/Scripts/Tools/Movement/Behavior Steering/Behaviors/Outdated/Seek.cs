/*
using UnityEngine;

namespace Steering
{
    public class Seek : Behavior
    {
        private Transform m_target;

        public Seek(Transform target)
        {
            m_target = target;
        }

        public override Vector3 CalculateSteeringForce(float deltaTime, BehaviorContext context)
        {
            //  Only update the target position if the follow transform is not null.
            if (m_target != null)
            {
                SetTargetPosition(m_target.position, context);
            }

            return TargetToSteeringForce(context);
        }

        public override void DrawGizmos(BehaviorContext context)
        {
            base.DrawGizmos(context);

            if (ArriveEnabled(context))
            {
                OnDrawArriveGizmos(context);
            }
        }
    }
}
*/