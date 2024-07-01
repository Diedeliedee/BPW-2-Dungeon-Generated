using Joeri.Tools.Structure.StateMachine.Simple;
using UnityEngine;

public class Player : Entity
{
    private InputReader m_input;
    private Highlighter m_highlighter;

    private FSM m_stateMachine = null;

    protected override void Awake()
    {
        base.Awake();

        m_input         = FindObjectOfType<InputReader>();
        m_highlighter   = GetComponentInChildren<Highlighter>(true);
    }

    private void Start()
    {
        var moving      = new Moving(m_input, Move);
        var selecting   = new Selecting(m_input, this, m_highlighter);

        m_stateMachine = new(moving, selecting);
    }

    public override void DuringTurn()
    {
        m_stateMachine.Tick();
    }

    public void EndTurn()
    {
        onTurnEnd.Invoke();
        m_stateMachine.OnSwitch(typeof(Moving));
        currentMovement = m_movementPerTurn;
    }
}
