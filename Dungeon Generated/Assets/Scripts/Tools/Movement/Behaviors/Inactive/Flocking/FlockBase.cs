/*
using UnityEngine;

namespace Steering
{
    public abstract class FlockBase
    {
        public Vector3 averageTotal { get => (m_total / (float)m_neightborCount).normalized; }

        protected float m_radius;
        protected int m_neightborCount;
        protected Vector3 m_total;

        /// <summary>
        /// Adds the given Vector3 to the 'm_total' variable if the given distance is within the radius.
        /// The Vector3 can be anything, including a position or a velocity, depending on the behavior part role.
        /// </summary>
        public virtual void AddToTotal(float distance, Vector3 addingVector)
        {
            if (distance < m_radius)
            {
                m_total += addingVector;
                m_neightborCount++;
            }
        }
    }
}
*/