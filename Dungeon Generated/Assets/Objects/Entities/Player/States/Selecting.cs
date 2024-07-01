using Joeri.Tools.Structure.StateMachine.Simple;

public class Selecting : State
{
    private InputReader m_input         = null;
    private Player m_player             = null;
    private Highlighter m_highlighter   = null;

    public Selecting(InputReader _input, Player _player, Highlighter _highlighter)
    {
        m_input         = _input;
        m_player        = _player;
        m_highlighter   = _highlighter;
    }

    public override void OnEnter()
    {
        if (m_player.currentActions <= 0)
        {
            Switch(typeof(Moving));
            return;
        }

        m_highlighter.Activate(m_player.coordinates + NavigationManager.ProcessDesiredInput(m_input.selectorDirection));
    }

    public override void OnTick()
    {
        if (m_input.movementPressed)
        {
            Switch(typeof(Moving));
            return;
        }
        if (m_input.selectorPressed)
        {
            m_highlighter.Tick(m_player.coordinates, NavigationManager.ProcessDesiredInput(m_input.selectorDirection));
        }
        if (m_input.confirmedSelection)
        {
            m_player.Attack(m_highlighter.coordinates);
            Switch(typeof(Moving));
        }
    }

    public override void OnExit()
    {
        m_highlighter.Deactivate();
    }
}