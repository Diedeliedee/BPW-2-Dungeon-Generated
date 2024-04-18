namespace Joeri.Tools.AI.BehaviorTree
{
    public class PrioritizeSucces : DecoratorNode
    {
        public PrioritizeSucces(Node _child) : base(_child) { }

        public override State Evaluate()
        {
            switch(child.Evaluate())
            {
                default: return State.Failure;

                case State.Running:
                case State.Succes:
                    return State.Succes;
            }
        }
    }
}
