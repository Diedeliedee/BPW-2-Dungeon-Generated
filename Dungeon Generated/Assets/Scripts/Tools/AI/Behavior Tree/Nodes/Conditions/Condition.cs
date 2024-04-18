namespace Joeri.Tools.AI.BehaviorTree
{
    public class Condition : LeafNode
    {
        private EmptyPredicate m_condition = null;

        public Condition(EmptyPredicate _condition, string _name = "") : base(_name)
        {
            m_condition = _condition;
        }

        public override State OnUpdate()
        {
            if (m_condition.Invoke()) return State.Succes;
            return State.Failure;
        }

        public delegate bool EmptyPredicate();
    }
}
