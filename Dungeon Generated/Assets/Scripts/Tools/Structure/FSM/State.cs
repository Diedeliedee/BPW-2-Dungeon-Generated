using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joeri.Tools.Structure
{
    public abstract class State
    {
        /// <summary>
        /// The state machine this state is a part of.
        /// </summary>
        protected FSM machine { get; private set; }

        /// <summary>
        /// Called whenever the finite state machine the state is in, is created.
        /// </summary>
        public virtual void Initialize(FSM machine)
        {
            this.machine = machine;
        }

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
        /// Switches to another state using a type variable.
        /// </summary>
        protected State SwitchToState(System.Type state)
        {
            return machine.SwitchToState(state);
        }

        /// <summary>
        /// Functions for drawing gizmos of the state.
        /// </summary>
        public virtual void OnDrawGizmos() { }

    }
}
