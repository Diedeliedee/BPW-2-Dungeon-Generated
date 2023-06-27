/*
using UnityEngine;

namespace Steering
{
    public class FlockSeparation : FlockBase
    {
        public FlockSeparation(float radius)
        {
            m_radius = radius;
        }

        public Vector3 DesiredDirection()
        {
            if (m_neightborCount > 0)
            {
                //  Returning a direction from the average relative direction of all nearby boids.
                return -averageTotal.normalized;
            }

            return Vector3.zero;
        }
    }
}
*/