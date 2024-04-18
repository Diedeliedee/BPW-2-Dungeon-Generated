namespace Joeri.Tools.Structure.StateMachine
{
    public interface IState
    {
        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        public void OnEnter();

        /// <summary>
        /// Called by the state machine whenever Tick() is called.
        /// </summary>
        public void OnTick();

        /// <summary>
        /// Called when the state is exited.
        /// </summary>
        public void OnExit();
    }
}
