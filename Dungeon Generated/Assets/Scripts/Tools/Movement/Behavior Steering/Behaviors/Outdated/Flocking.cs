/*
using UnityEngine;

namespace Steering
{
    public class Flocking : Behavior
    {
        private readonly Collider m_collider;

        private int m_flockLayer;
        private float m_largestRadius;

        public Flocking(Collider myCollider)
        {
            m_collider = myCollider;
        }

        public override void StartBehavior(BehaviorContext context)
        {
            base.StartBehavior(context);

            //  Getting the flock physics layer id.
            m_flockLayer = LayerMask.GetMask(context.settings.flockLayer);

            //  Determining the largest radius of all behavior parts.
            {
                m_largestRadius = 0;

                if (context.settings.alignmentWeight > 0)
                    m_largestRadius = Mathf.Max(m_largestRadius, context.settings.alignmentRadius);
                if (context.settings.cohesionWeight > 0)
                    m_largestRadius = Mathf.Max(m_largestRadius, context.settings.cohesionRadius);
                if (context.settings.separationWeight > 0)
                    m_largestRadius = Mathf.Max(m_largestRadius, context.settings.separationRadius);
            }
        }

        public override Vector3 CalculateSteeringForce(float deltaTime, BehaviorContext context)
        {
            SetTargetVelocity(DesiredVelocity(context));
            SetTargetPosition(VelocityToTarget(deltaTime, context), context);   //  Artificial target position.

            return VelocityDifference(context);
        }

        public override void DrawGizmos(BehaviorContext context)
        {
            base.DrawGizmos(context);

            GizmoTools.DrawCircle(context.position, m_largestRadius, Color.cyan, 0.25f);
            GizmoTools.DrawCircle(context.position, context.settings.separationRadius, Color.red, 0.25f);
        }

        /// <returns>The desired velocity using all three flocking behavior parts.</returns>
        private Vector3 DesiredVelocity(BehaviorContext context)
        {
            var neighbors = Physics.OverlapSphere(context.position, m_largestRadius, m_flockLayer, QueryTriggerInteraction.Ignore);

            //  Return (0,0,0) if nothing is found within the radius.
            if (neighbors.Length == 0)
            {
                return Vector3.zero;
            }

            //  Preparing the flock calculation classes.
            var alignment = new FlockAlignment(context.settings.alignmentRadius);
            var cohesion = new FlockCohesion(context.settings.cohesionRadius);
            var separation = new FlockSeparation(context.settings.separationRadius);

            foreach (var neighbor in neighbors)
            {
                //  Skip if the current collider is that of the object.
                if (neighbor == m_collider)
                {
                    continue;
                }

                var neighborOffset = neighbor.gameObject.transform.position - context.position;

                //  Skip if the neighbor is out of the object's sight.
                if (Vector3.Angle(m_collider.transform.forward, neighborOffset) > context.settings.visibilityAngle)
                {
                    continue;
                }

                var neighborSteering = neighbor.gameObject.GetComponent<Steering>();

                //  Skip if the neighbor does not have a steering script.
                if (neighborSteering == null)
                {
                    Debug.LogError($"{neighbor.gameObject.name} is found in a flock by {m_collider.gameObject.name}, but has no 'Steering' script attached.");
                    continue;
                }

                var distance = neighborOffset.magnitude;

                alignment.AddToTotal(distance, neighborSteering.velocity);
                cohesion.AddToTotal(distance, neighbor.transform.position);
                separation.AddToTotal(distance, neighborOffset);

            }

            var desiredDirection = alignment.DesiredDirection() * context.settings.alignmentWeight +
                                   cohesion.DesiredDirection(context.position) * context.settings.cohesionWeight +
                                   separation.DesiredDirection() * context.settings.separationWeight;

            return desiredDirection.normalized * context.settings.maxDesiredVelocity;
        }
    }
}
*/