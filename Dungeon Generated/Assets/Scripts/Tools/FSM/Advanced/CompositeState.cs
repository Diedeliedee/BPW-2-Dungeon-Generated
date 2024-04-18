using Joeri.Tools.Debugging;
using System.Linq;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class CompositeState<T> : IState, IStateMachine
    {
        public readonly IStateMachine parent = null;

        private Execution<T> m_execution    = null;
        private Conditions m_conditions     = null;
        private Management<T> m_management  = null;

        public string name      => m_execution.GetType().Name;
        public System.Type type => m_execution.GetType();

        /// <summary>
        /// Create a composite state with not branching states.
        /// </summary>
        public CompositeState(Execution<T> _execution, Conditions _conditions, IStateMachine _parent, T _source)
        {
            m_execution = _execution;
            m_conditions = _conditions;

            parent = _parent;
            m_execution.source = _source;
        }

        #region FSM Functions
        public void Tick()
        {
            OnTick();
            m_management?.Tick();

            if (m_conditions.GetStateToSwitchTo(out System.Type _state))
            {
                parent.OnSwitch(_state);
                return;
            }
        }

        public bool OnSwitch(System.Type state)
        {
            //  If the given state is not found in the children of this state.
            if (!m_management.OnSwitch(state))
            {
                //  Try asking the parent to switch to that state.
                parent.OnSwitch(state);
                return false;
            }
            return true;
        }

        public void RegisterChildren(CompositeState<T>[] _children)
        {
            m_management = new Management<T>(_children);
        }
        #endregion

        #region State Functions
        public virtual void OnEnter() => m_execution.OnEnter();

        public virtual void OnTick() => m_execution.OnTick();

        public virtual void OnExit() => m_execution.OnExit();
        #endregion

        #region Composite Functions
        /// <summary>
        /// Activates this state's FSM to it's default state.
        /// </summary>
        public void Activate() => m_management?.Activate();

        /// <summary>
        /// Resets this state's FSM to it's default state.
        /// </summary>
        public void Reset() => m_management?.Reset();
        #endregion

        public void Draw(Vector3 _position, string _text)
        {
            _text = string.Concat(_text, $" > {name}");

            if (m_management == null)
            {
                GizmoTools.DrawLabel(_position, _text, Color.white);
                return;
            }
            m_management.DrawChild(_position, _text);
        }
    }
}