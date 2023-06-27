using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Structure
{
    /// <summary>
    /// Abstract base for a state within a state machine.
    /// </summary>
    public abstract class FlexState<Root> : State
    {
        /// <summary>
        /// The settings of the state, as an abstract settings interface.
        /// </summary>
        private Settings settings { get; set; }

        /// <summary>
        /// The root class that the state machine is harbored in.
        /// </summary>
        protected Root root { get; private set; }

        /// <summary>
        /// Create a new state, and pass in the state's settings.
        /// </summary>
        public FlexState(Root root, Settings settings)
        {
            this.root = root;
            this.settings = settings;
        }

        /// <summary>
        /// Create a new state, without any required settings.
        /// </summary>
        public FlexState(Root root)
        {
            this.root = root;
            settings = null;
        }

        /// <summary>
        /// Switches to another state using a generic variable.
        /// </summary>
        protected T SwitchToState<T>() where T : FlexState<Root>
        {
            return machine.SwitchToState<T>();
        }

        /// <returns>The state's settings class casted as a settings sub-class.</returns>
        protected T GetSettings<T>() where T : Settings
        {
            return settings as T;
        }

        public abstract class Settings { }
    }
}