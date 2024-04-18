using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools.AI.Steering
{
    public class Flocking : Behavior
    {
        private readonly List<IBoid> m_peerboids = null;

        private List<FlockRule> m_rules = new List<FlockRule>();

        public Flocking(IFlock _flock, IBoid _boid, params FlockRule[] _rules)
        {
            m_peerboids = _flock.GetPeerBoids(_boid);
            m_rules = _rules.ToList();
        }

        public void SetRules(params FlockRule[] _rules)
        {
            m_rules.Clear();
            m_rules = _rules.ToList();
        }

        public void ClearRules()
        {
            m_rules.Clear();
        }

        public override Vector3 GetDesiredVelocity(Context context)
        {
            //  In the rare case that the boid is in a flock with zero other boids, return a velocity of zero.
            if (m_peerboids.Count <= 0) return Vector3.zero;

            //  Gather, and return each flock rule's contribution to the velocity.
            var velocity = Vector3.zero;
            for (int i = 0; i < m_rules.Count; i++)
            {
                velocity += m_rules[i].GetContribution(context.deltaTime, context.position, m_peerboids);
            }
            return velocity;
        }

        public override void DrawGizmos(Vector3 position)
        {
            for (int i = 0; i < m_rules.Count; i++) m_rules[i].DrawGizmos(position, m_peerboids);
        }
    }
}
