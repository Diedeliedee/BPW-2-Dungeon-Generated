public class FlexibleCondition : ICondition
{
    private ICondition.Condition m_condition    = null;
    private StateRequest m_stateRequest         = null;

    public ICondition.Condition condition   => m_condition;
    public System.Type state                => m_stateRequest.Invoke();

    public FlexibleCondition(ICondition.Condition _condition, StateRequest _stateRequest)
    {
        m_condition     = _condition;
        m_stateRequest  = _stateRequest;
    }

    public delegate System.Type StateRequest();
}