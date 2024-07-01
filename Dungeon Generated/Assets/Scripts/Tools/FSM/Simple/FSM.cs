using Joeri.Tools.Debugging;
using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine.Simple
{
    /// <summary>
    /// Class handling a class-based finite state machine system,
    /// </summary>
    public class FSM : IStateMachine
    {
        protected State m_activeState = null;

        protected readonly Dictionary<System.Type, State> m_states = new();

        public FSM(params State[] states)
        {
            //  Adding all given states to the dictionary.
            foreach (var state in states)
            {
                state.Setup(this);
                m_states.Add(state.GetType(), state);
            }

            //  Switching to the first given state.
            OnSwitch(states[0].GetType());
        }

        public virtual void Tick()
        {
            if (m_activeState == null)
            {
                Debug.LogError("Active state is not yet set.");
                return;
            }

            m_activeState.OnTick();
        }

        public virtual bool OnSwitch(System.Type state)
        {
            m_activeState?.OnExit();
            try     { m_activeState = m_states[state]; }
            catch   { Debug.LogError($"The state: '{state.Name}' is not found within the state dictionary."); return false; }
            m_activeState?.OnEnter();
            return true;
        }

        /// <summary>
        /// Function to call the gizmos of the current active state.
        /// </summary>
        public virtual void DrawGizmos(Vector3 position)
        {
            if (m_activeState == null) return;

            //  Drawing text in the world describing the current state the agent is in.
            GizmoTools.DrawLabel(position, m_activeState.GetType().Name, Color.black);

            //  Drawing the gizmos of the current state, if it isn't null.
            m_activeState.OnDrawGizmos();
        }
    }
}