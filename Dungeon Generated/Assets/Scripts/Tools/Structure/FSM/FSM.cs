using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools.Structure
{
    /// <summary>
    /// Class handling a class-based finite state machine system,
    /// </summary>
    public class FSM
    {
        protected readonly Dictionary<System.Type, State> m_states = null;

        protected State m_currentState = null;

        public System.Type activeState { get => m_currentState.GetType(); }

        public FSM(System.Type startState, params State[] states)
        {
            m_states = new Dictionary<System.Type, State>();

            //  All the state instances in the parameter get initialized, and added to the dictionary.
            foreach (var state in states)
            {
                state.Initialize(this);
                m_states.Add(state.GetType(), state);
            }
            SwitchToState(startState);
        }

        /// <summary>
        /// Updates the finite state machine's logic.
        /// </summary>
        public virtual void Tick(float deltaTime)
        {
            m_currentState.OnTick(deltaTime);
        }

        /// <summary>
        /// Switches to a new state based on the passed in type parameter.
        /// </summary>
        public State SwitchToState(System.Type state)
        {
            m_currentState?.OnExit();
            try { m_currentState = m_states[state]; }
            catch { Debug.LogError($"The state: '{state.Name}' is not found within the available state dictionary."); return null; }
            m_currentState?.OnEnter();
            return m_currentState;
        }

        /// <summary>
        /// Switches to a new state based on the generic parameter.
        /// </summary>
        public T SwitchToState<T>() where T : State
        {
            return (T)SwitchToState(typeof(T));
        }

        /// <summary>
        /// Function to call the gizmos of the current active state.
        /// </summary>
        public virtual void DrawGizmos(Vector3 position)
        {
            void DrawLabel(string label)
            {
                GizmoTools.DrawLabel(position, label, Color.black);
            }

            if (m_currentState == null) return;
            //  Drawing text in the world describing the current state the agent is in.
            DrawLabel(m_currentState.GetType().Name);

            //  Drawing the gizmos of the current state, if it isn't null.
            m_currentState.OnDrawGizmos();
        }
    }
}