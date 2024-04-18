using UnityEngine;

namespace Joeri.Tools.AI.BehaviorTree
{
    public class BehaviorTree
    {
        private Node m_root = null;

        public BehaviorTree(Node rootNode)
        {
            m_root = rootNode;
        }

        public void Tick()
        {
            m_root?.Evaluate();
        }

        public void Draw(Vector3 _center)
        {
            m_root?.OnDraw(_center);
        }

        public void PassBlackboard(Patterns.FittedBlackboard _blackboard)
        {
            m_root.PassBlackboard(_blackboard);
        }
    }
}
