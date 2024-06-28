namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class Conditions
    {
        public readonly ICondition[] conditions = null;

        public Conditions(params ICondition[] _conditions)
        {
            conditions = _conditions;
        }

        public Conditions()
        {
            conditions = new ICondition[0];
        }

        /// <summary>
        /// Runs the predicate of every condition listed in the array of this container, and retrieves the desired state based on that.
        /// Conditions that have been inserted earlier in the building process have higher priority.
        /// </summary>
        /// <returns>Whether the state machine these conditions belong to should switch to another state or not.</returns>
        public bool GetStateToSwitchTo(out System.Type _state)
        {
            _state = null;
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].condition()) continue;
                _state = conditions[i].state;
                return true;
            }
            return false;
        }
    }
}