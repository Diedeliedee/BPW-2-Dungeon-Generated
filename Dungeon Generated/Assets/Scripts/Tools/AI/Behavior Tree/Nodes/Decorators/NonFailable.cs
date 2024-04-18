namespace Joeri.Tools.AI.BehaviorTree
{
    public class NonFailable : DecoratorNode
    {
        public NonFailable(Node _child) : base(_child) { }

        public override State Evaluate()
        {
            switch(child.Evaluate())
            {
                default: return State.Succes;

                case State.Failure:
                case State.Running:
                    return State.Running;
            }
        }
    }
}
