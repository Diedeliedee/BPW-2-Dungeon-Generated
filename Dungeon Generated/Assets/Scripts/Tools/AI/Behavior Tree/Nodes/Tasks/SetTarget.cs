using UnityEngine;

namespace Joeri.Tools.AI.BehaviorTree
{
    public class SetTarget : LeafNode
    {
        private Transform m_target = default;

        public SetTarget(Transform _target)
        {
            m_target = _target;
        }

        public override State OnUpdate()
        {
            board.Get<TargetMemory>().SetTarget(m_target.position);
            return State.Succes;
        }

        public override void OnAbort()
        {
            board.Get<TargetMemory>().Reset();
        }
    }
}
