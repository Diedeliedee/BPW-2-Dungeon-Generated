using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dodelie.Tools
{
    /// <summary>
    /// Class handling a class-based finite state machine system,
    /// </summary>
    public class FSM<Root> where Root : Object
    {
        private readonly Root m_root                                    = null;
        private readonly Dictionary<System.Type, State<Root>> m_states  = null;

        private State<Root> m_currentState                              = null;

        public Root root { get => m_root; }

        public FSM(Root root, System.Type startState, params State<Root>[] states)
        {
            m_root      = root;
            m_states    = new Dictionary<System.Type, State<Root>>();

            //  All the state instances in the parameter get initialized, and added to the dictionary.
            foreach (var state in states)
            {
                state.Initialize(this);
                m_states.Add(state.GetType(), state);
            }
            SwitchToState(startState);
        }

        /// <summary>
        /// Switches to a new state based on the passed in type parameter.
        /// </summary>
        public State<Root> SwitchToState(System.Type state)
        {
            m_currentState?.OnExit();
            try     { m_currentState = m_states[state]; }
            catch   { Debug.LogError($"The state: '{state.Name}' is not found within the available state dictionary.", m_root); return null; }
            m_currentState?.OnEnter();
            return m_currentState;
        }

        /// <summary>
        /// Switches to a new state based on the generic parameter.
        /// </summary>
        public State SwitchToState<State>() where State : State<Root>
        {
            return SwitchToState(typeof(State)) as State;
        }

        /// <summary>
        /// Updates the finite state machine's logic.
        /// </summary>
        public virtual void Tick(float deltaTime)
        {
            m_currentState.OnTick(deltaTime);
        }

        /// <summary>
        /// Function to call the gizmos of the current active state.
        /// </summary>
        public virtual void DrawGizmos(Vector3 position)
        {
            //  Drawing text in the world describing the current state the agent is in.
            GizmoTools.DrawLabel(position, m_currentState.GetType().Name, Color.black);

            //  Drawing the gizmos of the current state, if it isn't null.
            m_currentState?.OnDrawGizmos();
        }
    }
}