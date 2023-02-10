/*
using UnityEngine;

namespace Steering
{
    public class FlockCohesion : FlockBase
    {
        public FlockCohesion(float radius)
        {
            m_radius = radius;
        }

        public Vector3 DesiredDirection(Vector3 position)
        {
            if (m_neightborCount > 0)
            {
                //  Returning a direction towards the average position of all nearby boids.
                return (averageTotal - position).normalized;
            }

            return Vector3.zero;
        }
    }
}
*/