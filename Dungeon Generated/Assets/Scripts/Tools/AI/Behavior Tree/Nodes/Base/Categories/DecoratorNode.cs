using UnityEngine;
using Joeri.Tools.Patterns;

namespace Joeri.Tools.AI.BehaviorTree
{
    public abstract class DecoratorNode : Node
    {
        public readonly Node child = null;

        public DecoratorNode(Node _child)
        {
            child = _child;             //  Attach child to the node.
            _child.AttachParent(this);  //  Attach this node as the children's parent.
        }

        public override void OnAbort()
        {
            child.OnAbort();
        }

        public override void PassBlackboard(FittedBlackboard _blackboard)
        {
            base.PassBlackboard(_blackboard);
            child.PassBlackboard(_blackboard);
        }

        public override void OnDraw(Vector3 _center)
        {
            child.OnDraw(_center);
        }
    }
}
