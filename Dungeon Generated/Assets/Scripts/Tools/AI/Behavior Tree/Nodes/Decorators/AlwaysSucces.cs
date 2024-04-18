namespace Joeri.Tools.AI.BehaviorTree
{
    public class AlwaysSucces : DecoratorNode
    {
        public AlwaysSucces(Node _child) : base(_child) { }

        public override State Evaluate()
        {
            child.Evaluate();
            return State.Succes;
        }
    }
}
