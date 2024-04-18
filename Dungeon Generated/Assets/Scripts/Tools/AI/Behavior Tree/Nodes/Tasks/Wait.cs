namespace Joeri.Tools.AI.BehaviorTree
{
    public class Wait : LeafNode
    {
        private Timer m_timer = null;
        private bool m_indefinite = false;

        public Wait(float _seconds, string _name = "") : base(_name)
        {
            m_timer = new Timer(_seconds);
        }

        public Wait(string _name = "") : base(_name)
        {
            m_indefinite = true;
        }

        public override State OnUpdate()
        {
            if (!m_indefinite && m_timer.ResetOnReach(board.Get<TimeMemory>().deltaTime)) return State.Succes;
            return State.Running;
        }

        public override void OnAbort()
        {
            m_timer?.Reset();
        }
    }
}
