using Joeri.Tools.Utilities;

namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class EventCondition : ICondition
    {
        public ICondition.Condition condition   { get; private set; }
        public System.Type state                { get; private set; }

        private bool m_conditionTriggered = false;

        public EventCondition(EventWrapper _event, System.Type _stateToSwitchTo)
        {
            _event.Subscribe(EventReceiver);

            condition   += ConfirmEvent;
            state       = _stateToSwitchTo;
        }

        private void EventReceiver()
        {
            m_conditionTriggered = true;
        }

        private bool ConfirmEvent()
        {
            //  If the condition has not been triggered, return false.
            if (!m_conditionTriggered)
            {
                return false;
            }

            m_conditionTriggered = false;   //  Reset condition once confirmed.
            return true;                    //  Confirm positive condition.
        }
    }
}