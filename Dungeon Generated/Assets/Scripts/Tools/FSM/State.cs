using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dodelie.Tools
{
    /// <summary>
    /// Abstract base for a state within a state machine.
    /// </summary>
    public abstract class State<Root> where Root : Object
    {
        /// <summary>
        /// The state machine this state is a part of.
        /// </summary>
        protected FSM<Root> parent { get; private set; }

        protected Root root { get => parent.root; }

        /// <summary>
        /// Called whenever the finite state machine the state is in, is created.
        /// </summary>
        public virtual void Initialize(FSM<Root> parent)
        {
            this.parent = parent;
        }

        #region Local Interface

        /// <summary>
        /// Switches to another state using a generic variable.
        /// </summary>
        protected State SwitchToState<State>() where State : State<Root>
        {
            return parent.SwitchToState<State>();
        }

        /// <summary>
        /// Switches to another state using a type variable.
        /// </summary>
        protected State<Root> SwitchToState(System.Type state)
        {
            return parent.SwitchToState(state);
        }

        #endregion

        /// <summary>
        /// Update function for the state.
        /// </summary>
        public virtual void OnTick(float deltaTime) { }

        /// <summary>
        /// Called whenevet the state is entered.
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// Called whenever the state is exited.
        /// </summary>
        public virtual void OnExit() { }

        /// <summary>
        /// Functions for drawing gizmos of the state.
        /// </summary>
        public virtual void OnDrawGizmos() { }
    }
}