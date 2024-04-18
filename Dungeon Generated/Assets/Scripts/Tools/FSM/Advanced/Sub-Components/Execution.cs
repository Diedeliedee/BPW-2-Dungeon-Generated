namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public abstract class Execution<T>
    {
        public T source = default;

        public virtual void OnEnter() { }

        public virtual void OnTick() { }

        public virtual void OnExit() { }
    }
}
