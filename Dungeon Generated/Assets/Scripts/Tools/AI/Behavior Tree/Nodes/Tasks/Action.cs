namespace Joeri.Tools.AI.BehaviorTree
{
    public class Action : LeafNode
    {
        private System.Action m_action = null;

        public Action(System.Action _action, string _name = "") : base(_name)
        {
            m_action = _action;
        }

        public override State OnUpdate()
        {
            m_action.Invoke();
            return State.Succes;
        }
    }
}
