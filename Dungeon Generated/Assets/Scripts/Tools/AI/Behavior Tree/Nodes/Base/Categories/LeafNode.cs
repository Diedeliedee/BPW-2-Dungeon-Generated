using Joeri.Tools.Debugging;
using UnityEditor;
using UnityEngine;

namespace Joeri.Tools.AI.BehaviorTree
{
    public abstract class LeafNode : Node
    {
        private readonly string m_name = "";
        private bool m_activated = false;

        public LeafNode()
        {
            m_name = GetType().Name;
        }

        public LeafNode(string _name)
        {
            if (_name == "") _name = GetType().Name;
            m_name = _name;
        }

        public override State Evaluate()
        {
            //  If this is the first time running the task since it has been last completed, run OnEnter().
            if (!m_activated)
            {
                m_activated = true;
                OnEnter();
            }

            //  Run the OnUpdate() function, and save the result for further evaluation.
            var result = OnUpdate();

            //  If the result is anything other than running, such as succes or failure, the task is over. Run OnExit().
            if (result != State.Running)
            {
                OnExit();
                m_activated = false;
            }

            //  Return the result to higher in the hierarchy.
            return result;
        }

        /// <summary>
        /// Called every frame that the task is running.
        /// </summary>
        /// <returns>The evaluated node state from the task.</returns>
        public abstract State OnUpdate();

        /// <summary>
        /// Called at the start of the task.
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// Called when the task has either succeeded, or failed.
        /// </summary>
        public virtual void OnExit() { }

        /// <summary>
        /// Called when the task needs to be aborted, because the branch the task is a part of is interrupted.
        /// </summary>
        public override void OnAbort()
        {
            OnExit();
            m_activated = false;
        }

        public override void OnDraw(Vector3 _center)
        {
            GizmoTools.DrawLabel(_center, m_name, Color.white);
        }
    }
}
