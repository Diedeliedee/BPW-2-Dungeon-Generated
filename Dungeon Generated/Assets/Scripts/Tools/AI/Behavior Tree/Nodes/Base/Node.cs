using UnityEngine;

namespace Joeri.Tools.AI.BehaviorTree
{
    public abstract class Node
    {
        public Node parent { get; private set; }
        public Patterns.FittedBlackboard board { get; private set; }

        /// <returns>The state that the evaluation resulted in.</returns>
        public abstract State Evaluate();

        /// <summary>
        /// Called if the branch the node is a part of is aborted.
        /// </summary>
        public abstract void OnAbort();

        /// <summary>
        /// Sets the passed in node as the this node's parent.
        /// </summary>
        public void AttachParent(Node _parent)
        {
            parent = _parent;
        }

        /// <summary>
        /// Attaches the passed in blackboard to the Node. And depending on overrides, can pass it down to the Node's children too.
        /// </summary>
        public virtual void PassBlackboard(Patterns.FittedBlackboard _blackboard)
        {
            board = _blackboard;
        }

        /// <summary>
        /// Draws any gizmos that leaf nodes may want to display.
        /// </summary>
        public abstract void OnDraw(Vector3 _center);
    }
}
