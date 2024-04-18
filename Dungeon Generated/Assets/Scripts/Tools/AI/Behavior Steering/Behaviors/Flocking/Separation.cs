using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Utilities;

namespace Joeri.Tools.AI.Steering
{
    public class Separation : FlockRule
    {
        private readonly float m_radius = 0;
        private readonly float m_sqrRadius = 0;
        private readonly float m_force = 0;

        public Separation(float _radius, float _force)
        {
            m_radius = _radius;
            m_sqrRadius = _radius * _radius;
            m_force = _force;
        }

        public override Vector3 GetContribution(float _deltaTime, Vector3 _position, List<IBoid> _peerBoids)
        {
            var totalPositions = Vector3.zero;
            var boidsInRange = 0;

            _peerBoids = FilterToNearbyBoids(_position, m_sqrRadius, _peerBoids);
            if (_peerBoids.Count <= 0) return Vector3.zero;
            foreach (var peer in _peerBoids)
            {
                //  Add the position of the boid to be used in the average equation.
                totalPositions += peer.position;
                boidsInRange++;
            }

            //  Calculate desired velocity away from the average of all nearby boids.
            totalPositions /= boidsInRange;
            return (_position - totalPositions).normalized * m_force;
        }

        public override void DrawGizmos(Vector3 _position, List<IBoid> _peerBoids)
        {
            _peerBoids = FilterToNearbyBoids(_position, m_sqrRadius, _peerBoids);
            foreach (var peer in _peerBoids)
            {
                Gizmos.color = new Color(1f, 0f, 0f, Util.OneMinus(Vector3.Distance(_position, peer.position) / m_radius));
                Gizmos.DrawLine(_position, peer.position);
            }
        }
    }
}