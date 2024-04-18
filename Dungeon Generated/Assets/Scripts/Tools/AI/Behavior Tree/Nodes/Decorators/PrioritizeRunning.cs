namespace Joeri.Tools.AI.BehaviorTree
{
    public class PrioritizeRunning : DecoratorNode
    {
        public PrioritizeRunning(Node _child) : base(_child) { }

        public override State Evaluate()
        {
            switch(child.Evaluate())
            {
                default: return State.Failure;

                case State.Running:
                case State.Succes:
                    return State.Running;
            }
        }
    }
}
