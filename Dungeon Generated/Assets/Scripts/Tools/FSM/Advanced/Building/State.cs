namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class State<T>
    {
        public readonly Execution<T> execution  = null;
        public readonly Conditions conditions   = null;
        public readonly State<T>[] children     = null;

        public State(Execution<T> _execution, Conditions _conditions, params State<T>[] _children)
        {
            execution   = _execution;
            conditions  = _conditions;
            children    = _children;
        }
    }
}