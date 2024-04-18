namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class SimpleCondition : ICondition
    {
        public ICondition.Condition condition   { get; private set; }
        public System.Type state                { get; private set; }

        public SimpleCondition(ICondition.Condition _condition, System.Type _stateToSwitchTo)
        {
            condition   = _condition;
            state       = _stateToSwitchTo;
        }

    }
}