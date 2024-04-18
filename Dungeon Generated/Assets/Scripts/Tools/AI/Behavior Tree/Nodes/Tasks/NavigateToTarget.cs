using UnityEngine.AI;

namespace Joeri.Tools.AI.BehaviorTree
{
    public class IterateToTarget : LeafNode
    {
        private NavMeshAgent m_agent = null;
        private TargetMemory m_memory = null;

        public IterateToTarget(string _name = "") : base(_name) { }

        public override void OnEnter()
        {
            m_memory ??= board.Get<TargetMemory>();
            m_agent ??= board.Get<NavMeshAgent>();
            m_agent.isStopped = false;
        }

        public override State OnUpdate()
        {
            m_agent.SetDestination(m_memory.target);
            if (m_agent.pathPending || m_agent.remainingDistance > m_agent.stoppingDistance) return State.Running;
            return State.Succes;
        }
    }
}
