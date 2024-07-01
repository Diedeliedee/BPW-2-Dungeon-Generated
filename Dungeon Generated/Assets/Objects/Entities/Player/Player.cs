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

    public override void Damage(int _damage)
    {
        Debug.Log("Ouch");

        currentHealth -= _damage;

        if (currentHealth < 0) currentHealth = 0;
        m_onDamage.Invoke(currentHealth, m_maxHealth);
        if (currentHealth == 0) m_onDeath.Invoke();
    }

    public override void DuringTurn()
    {
        m_stateMachine.Tick();
    }

    public override void EndTurn()
    {
        base.EndTurn();
        m_stateMachine.OnSwitch(typeof(Moving));
    }
}
