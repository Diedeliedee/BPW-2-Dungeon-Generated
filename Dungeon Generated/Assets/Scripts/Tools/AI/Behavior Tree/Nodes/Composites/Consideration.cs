using UnityEngine;

namespace Joeri.Tools.AI.BehaviorTree
{
    public class Consideration : CompositeNode
    {
        private int m_index = 0;

        private readonly System.Func<bool> m_consideration = null;

        public Consideration(System.Func<bool> _consideration, Node _if, Node _else) : base(_if, _else)
        {
            m_consideration = _consideration;
        }

        public override State Evaluate()
        {
            if (m_consideration())  m_index = 0;
            else                m_index = 1;
            return children[m_index].Evaluate();
        }

        public override void OnDraw(Vector3 _center)
        {
            children[m_index].OnDraw(_center);
        }
    }
}
