/*
using UnityEngine;

namespace DungeonSlasher.Agents
{

    public abstract class Avoid : Behavior
    {
        protected LayerMask m_obstacleLayer;    //  The layer used to check for objects to avoid.

        protected bool m_impendingCollision;    //  Whether the sensor has detected an object.
        protected Beam m_sensor;                //  The sensor responsible for detecting inbound objects.

        protected Vector3 m_hitPosition;        //  The position the sensor hit an object.

        /// <summary>
        /// Send a spherecast in the object's velocit direction, and update the corresponding variables.
        /// </summary>
        protected void SendSensor(out RaycastHit hitInfo, BehaviorContext context)
        {
            //  Creating a sensor for the raycast.
            m_sensor = new Beam(context.position, context.velocity.normalized, context.settings.sensorDistance, context.settings.sensorWidth / 2);

            //  Casting a spherecast.
            m_impendingCollision = m_sensor.BeamHit(out hitInfo, m_obstacleLayer);

            //  Setting the hit position.
            if (m_impendingCollision)
            {
                m_hitPosition = hitInfo.point;
                return;
            }

            m_hitPosition = Vector3.zero;
        }

        /// <summary>
        /// Set the target velocity to the force the object should avoid with.
        /// </summary>
        protected void SetAvoidForce(Vector3 direction, float force)
        {
            var avoidForce = direction.normalized * force;

            SetTargetVelocity(avoidForce);
        }

        /// <summary>
        /// Will prevent the object from getting stuck with opposing target velocity values.
        /// </summary>
        protected void PreventVelocityAlignment(BehaviorContext context)
        {
            /// If the current velocity, and the target velocity are directly opposite from another, the object will get stuck.
            var angle = Vector3.Angle(targetVelocity, context.velocity);

            /// To prevent this the cross product of (0,1,0), and the current velocity will become the new velocity which should be to the right.
            if (angle > 179)
            {
                SetTargetVelocity(Vector3.Cross(Vector3.up, context.velocity));
            }
        }

        public override void DrawGizmos(BehaviorContext context)
        {
            var gizmoBeam = m_sensor;

            var sensorColor = Color.white;
            var sensorOpacity = 0.5f;

            //  The drawn sensor should be shorter and colored red if a collision is inbound.
            if (m_impendingCollision)
            {
                sensorColor = Color.red;
                sensorOpacity = 1;

                gizmoBeam.pointer.SetNewDistance((m_hitPosition - gizmoBeam.pointer.origin).magnitude);
            }

            //  Drawing the sensor beam.
            gizmoBeam.Draw(sensorColor, sensorOpacity);
        }
    }
}
*/