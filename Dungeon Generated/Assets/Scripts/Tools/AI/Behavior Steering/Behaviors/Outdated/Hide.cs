/*
using UnityEngine;
using System.Collections.Generic;

namespace Steering
{
    public class Hide : Behavior
    {
        readonly private Transform m_target;

        private List<Collider> m_colliders;
        private List<Vector3> m_hidingPlaces;
        private Vector3 m_hidingPlace;

        public Hide(Transform target)
        {
            m_target = target;
        }

        public override void StartBehavior(BehaviorContext context)
        {
            base.StartBehavior(context);

            m_colliders = FindCollidersWithLayer(context.settings.hideLayer);
        }

        public override Vector3 CalculateSteeringForce(float deltaTime, BehaviorContext context)
        {
            SetTargetPosition(CalculateHidingPlace(m_target.position, context), context);

            return TargetToSteeringForce(context);
        }

        /// <returns>The hiding place the behavior object should go to.</returns>
        private Vector3 CalculateHidingPlace(Vector3 threatPosition, BehaviorContext context)
        {
            var closestDistance = float.MaxValue;

            m_hidingPlace = context.position;
            m_hidingPlaces = new List<Vector3>();

            //  Loop trough all colliders.
            for (int i = 0; i < m_colliders.Count; i++)
            {
                //  Get the hiding place from the current collider.
                var hidingPlace = CalculateHidingPlace(threatPosition, m_colliders[i], context);

                //  Add it to the list.
                m_hidingPlaces.Add(hidingPlace);

                //  If the hiding place of this loop is closer to the object than the closest one,
                //  set it as the target hiding place
                {
                    var distanceToHidingPlace = (context.position - hidingPlace).magnitude;

                    if (distanceToHidingPlace < closestDistance)
                    {
                        closestDistance = distanceToHidingPlace;
                        m_hidingPlace = hidingPlace;
                    }
                }
            }

            return m_hidingPlace;
        }

        /// <returns>The hiding place for a given collider.</returns>
        private Vector3 CalculateHidingPlace(Vector3 threatPosition, Collider collider, BehaviorContext context)
        {
            var direction = (collider.transform.position - threatPosition).normalized;

            ///  Collider.ClosestPoint won't return a point on the surface of the collider
            ///  if the given point is inside of the collider.
            ///  We take the largest horizontal radius of the collider's bounds,
            ///  and use that distance with the direction in the closest point calculation

            var colliderLargestRadius = (collider.bounds.size.x + collider.bounds.size.z) / 2;

            var wallPosition = collider.ClosestPoint(collider.transform.position + (direction * colliderLargestRadius));
            var hidingPlace = wallPosition + (direction * context.settings.hideOffset);

            return hidingPlace;
        }

        /// <returns>List of colliders with the given layer.</returns>
        public static List<Collider> FindCollidersWithLayer(string layerName)
        {
            var layer = LayerMask.NameToLayer(layerName);

            var allColliders = Object.FindObjectsOfType(typeof(Collider)) as Collider[];
            var colliders = new List<Collider>();

            foreach (var collider in allColliders)
            {
                if (collider.gameObject.layer == layer)
                {
                    colliders.Add(collider);
                }
            }

            return colliders;
        }

        public override void DrawGizmos(BehaviorContext context)
        {
            base.DrawGizmos(context);

            if (m_hidingPlaces != null)
            {
                //  Draw a blue disc at every hiding place, with a transparent line towards it from the object.
                foreach (var hidingPlace in m_hidingPlaces)
                {
                    GizmoTools.DrawLine(context.position, hidingPlace, Color.blue, 0.25f);
                    GizmoTools.DrawSolidDisc(hidingPlace, 0.25f, Color.blue, 0.5f);
                }

                //  Draw a blue halo around the closest/chosen hiding place.
                GizmoTools.DrawCircle(m_hidingPlace, 0.25f, Color.white);

                if (ArriveEnabled(context))
                {
                    OnDrawArriveGizmos(context);
                }
            }
        }
    }
}
*/