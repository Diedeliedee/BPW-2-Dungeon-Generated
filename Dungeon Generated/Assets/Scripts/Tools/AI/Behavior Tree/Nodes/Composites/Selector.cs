using UnityEngine;

namespace Joeri.Tools.AI.BehaviorTree
{
    public class Selector : CompositeNode
    {
        private int m_index = -1;
        private int m_lastIndex = -1;

        public Selector(params Node[] _children) : base(_children) { }

        public override State Evaluate()
        {
            //  Check node states of the children.
            for (m_index = 0; m_index < children.Length; m_index++)
            {
                switch (children[m_index].Evaluate())
                {
                    //  If the current node has failed, move on to evaluate the next.
                    case State.Failure: continue;

                    // If the current child is running, the selector is still running too.
                    case State.Running:
                        RegisterIndex();
                        return State.Running;

                    //  If the current child has been a succes, the selector has been a succes too.
                    case State.Succes:
                        RegisterIndex();
                        return State.Succes;
                }
            }

            //  If no children are running, none can be selected and the selector has failed.
            m_index = -1;
            m_lastIndex = -1;
            return State.Failure;
        }

        private void RegisterIndex()
        {
            if (m_lastIndex > 0 && m_index != m_lastIndex) children[m_lastIndex].OnAbort();
            m_lastIndex = m_index;
        }

        public override void OnAbort()
        {
            if (m_index < 0) return;

            children[m_index].OnAbort();
            m_index = -1;
            m_lastIndex = -1;
        }

        public override void OnDraw(Vector3 _center)
        {
            children[m_index].OnDraw(_center);
        }
    }
}
