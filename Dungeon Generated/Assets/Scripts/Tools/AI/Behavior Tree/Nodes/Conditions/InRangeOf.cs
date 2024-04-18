using UnityEngine;

namespace Joeri.Tools.AI.BehaviorTree
{
    public class InRangeOf : LeafNode
    {
        private Transform m_target = default;
        private float m_range = 0f;

        public InRangeOf(Transform _target, float _range, string _name = "") : base(_name)
        {
            m_target = _target;
            m_range = _range;
        }

        public override State OnUpdate()
        {
            var selfPos = board.Get<SelfMemory>().position;
            var targetPos = m_target.position;

            if (m_target != null && (targetPos - selfPos).sqrMagnitude <= m_range * m_range)
            {
                return State.Succes;
            }
            return State.Failure;
        }
    }
}
