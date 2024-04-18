using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools.AI.Steering
{
    public class Alignment : FlockRule
    {
        private readonly float m_sqrRadius = 0;

        public Alignment(float _radius)
        {
            m_sqrRadius = _radius * _radius;
        }

        public override Vector3 GetContribution(float _deltaTime, Vector3 _position, List<IBoid> _peerBoids)
        {
            var totalVelocity = Vector3.zero;
            var boidsInRange = 0;

            _peerBoids = FilterToNearbyBoids(_position, m_sqrRadius, _peerBoids);
            if (_peerBoids.Count <= 0) return Vector3.zero;
            foreach (var peer in _peerBoids)
            {
                //  Add the position of the boid to be used in the average equation.
                totalVelocity += peer.velocity;
                boidsInRange++;
            }

            //  Calculate and return the average velocity of all nearby boids.
           return totalVelocity /= boidsInRange;
        }
    }
}