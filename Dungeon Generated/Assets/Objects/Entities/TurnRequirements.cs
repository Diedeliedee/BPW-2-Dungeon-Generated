public abstract partial class Entity
{
    protected class TurnRequirements
    {
        //  Event:
        public System.Action onTurnComplete = null;

        //  Requirements:
        private bool m_hasMoved = false;

        //  Accesors:
        public bool hasMoved
        {
            get => m_hasMoved;
            set
            {
                m_hasMoved = true;
                CheckForFinishTurn();
            }
        }

        public TurnRequirements(System.Action onTurnFinish)
        {
            onTurnComplete += onTurnFinish;
        }

        public void CheckForFinishTurn()
        {
            if (!m_hasMoved) return;

            CompleteTurn();
        }

        public void SkipTurn()
        {
            CompleteTurn();
        }

        private void CompleteTurn()
        {
            onTurnComplete?.Invoke();
        }
    }
}
