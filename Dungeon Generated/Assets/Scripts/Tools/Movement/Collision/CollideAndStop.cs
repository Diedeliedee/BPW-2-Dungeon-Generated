using Joeri.Tools.Debugging;
using UnityEngine;

namespace Joeri.Tools.Movement.TwoDee
{
    public class CollideAndStop : ICollisionAlgorithm
    {
        private readonly BoxCollider2D m_collider   = default;
        private readonly LayerMask m_collisionMask  = default;
        private readonly float m_skinWidth          = 0f;

        public CollideAndStop(BoxCollider2D _collider, LayerMask _collisionMask)
        {
            m_collider      = _collider;
            //m_skinWidth     = _collider.size.x / 2 * 0.1f;
            m_skinWidth     = 0.15f;
            m_collisionMask = _collisionMask;
        }

        public Vector2 RunCollisionCheck(Vector2 _position, Vector2 _velocity)
        {
            var bounds      =  m_collider.bounds;                               //  Create a duplicate of the bounds.
            var distance    = _velocity.magnitude + m_skinWidth;                //  Cache the distance to travel.

            bounds.Expand(new Vector3(-m_skinWidth * 2, -m_skinWidth * 2, 0f)); //  Shrink the bounds with the skinwidth.
            
            //  Send out a box cast, and collect every collision.
            var hits = Physics2D.BoxCastAll(_position, bounds.size, 0f, _velocity.normalized, distance, m_collisionMask);

            //  Loop through all the collisions. Whichever collsion isn't a trigger, gets chosen.
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.isTrigger) continue;

                var snapToSurface = _velocity.normalized * (hits[i].distance - m_skinWidth);

                if (snapToSurface.magnitude <= m_skinWidth) snapToSurface = Vector2.zero;
                return snapToSurface;
            }

            //  If no collisions have been found, return the current velocity.
            return _velocity;
        }
    }
}