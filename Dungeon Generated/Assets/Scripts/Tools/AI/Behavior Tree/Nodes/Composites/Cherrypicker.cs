using UnityEngine;

namespace Joeri.Tools.AI.BehaviorTree
{
    public class Cherrypicker : CompositeNode
    {
        private State m_preference = default;

        public Cherrypicker(State _preference, params Node[] _children) : base(_children)
        {
            m_preference = _preference;
        }

        public override State Evaluate()
        {
            //  Check node states of the children.
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].Evaluate() != m_preference) continue;
                return m_preference;
            }

            return State.Failure;
        }
    }
}
