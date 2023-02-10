using UnityEngine;

namespace Dodelie.Tools
{
    public class Pursue : Behavior
    {
        private float m_lookAheadTime = 0f;
        private Transform m_target = null;

        private Vector2 m_previousTargetPosition = Vector2.zero;
        private Vector2 m_futureTargetPosition = Vector2.zero;

        public Pursue(float lookAheadTime, Transform target)
        {
            m_lookAheadTime = lookAheadTime;
            m_target = target;

            m_previousTargetPosition = Calc.VectorToFlat(m_target.position);
        }

        public override Vector2 GetDesiredVelocity(Context context)
        {
            if (m_target == null) return Vector3.zero;

            var targetPosition = Calc.VectorToFlat(m_target.position);
            var targetVelocity = (targetPosition - m_previousTargetPosition) / context.deltaTime;

            m_futureTargetPosition = targetPosition + (targetVelocity * m_lookAheadTime);
            m_previousTargetPosition = targetPosition;
            return (m_futureTargetPosition - context.position).normalized * context.speed;
        }

        public override void DrawGizmos(Vector3 position)
        {
            var targetPosition = m_target.position;

            GizmoTools.DrawLine(position, m_target.position, Color.red);
            GizmoTools.DrawLine(targetPosition, Calc.FlatToVector(m_futureTargetPosition, targetPosition.y), Color.blue);
        }
    }
}