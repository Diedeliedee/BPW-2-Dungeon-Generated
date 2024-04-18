using UnityEngine;

namespace Joeri.Tools.AI.BehaviorTree
{
    public class Routine : CompositeNode
    {
        private int m_index = 0;

        public Routine(params Node[] _children) : base(_children) { }

        public override State Evaluate()
        {
            //  Check node states of the children.
            for (; m_index < children.Length; m_index++)
            {
                switch (children[m_index].Evaluate())
                {
                    //  If any child has failed, the routine is broken.
                    case State.Failure:
                        m_index = 0;
                        return State.Failure;

                    // If any child is running, the routine is still running too.
                    case State.Running: return State.Running;

                    // If any child has succeeded already, continue to the next child.
                    case State.Succes: continue;
                }
            }

            //  If no children are running anymore, and none have failed, the routine is a succes.
            m_index = 0;
            return State.Succes;
        }

        public override void OnAbort()
        {
            children[m_index].OnAbort();
            m_index = 0;
        }

        public override void OnDraw(Vector3 _center)
        {
            children[m_index].OnDraw(_center);
        }
    }
}
