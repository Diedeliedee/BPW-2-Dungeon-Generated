using Joeri.Tools;
using Joeri.Tools.Debugging;
using UnityEngine;

public class Bower : Entity
{
    [SerializeField] private float m_aggroDistance = 5f;
    [SerializeField] private float m_choiceTime = 0.5f;

    private State m_state = default;
    private Timer m_choiceTimer = null;

    private void Start()
    {
        m_choiceTimer = new Timer(m_choiceTime);
    }

    public override void OnTurnStart()
    {
        Debug.Log("Bower's Turn!!");

        var distanceToPlayer = Vector2.Distance(transform.position, m_dungeon.player.transform.position);

        //  Deciding what to do this turn.
        if      (distanceToPlayer < m_aggroDistance)    m_state = State.COMBAT;
        else if (distanceToPlayer > m_aggroDistance)    m_state = State.IDLE;
    }

    public override void DuringTurn()
    {
        var distanceToPlayer = Vector2.Distance(transform.position, m_dungeon.player.transform.position);
        switch (m_state)
        {
            case State.IDLE:
                onTurnEnd.Invoke();
                break;

            case State.COMBAT:
                if (!m_choiceTimer.ResetOnReach(Time.deltaTime)) break;

                if (!Move(ProcessDesiredInput((m_dungeon.player.transform.position - transform.position).normalized)))
                {
                    m_currentMovement = m_movementPerTurn;
                    onTurnEnd.Invoke();
                }

                if (m_currentMovement <= 0)
                {
                    m_currentMovement = m_movementPerTurn;
                    onTurnEnd.Invoke();
                }
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        GizmoTools.DrawSphere(transform.position + new Vector3(0.5f, 0.5f, 0f), m_aggroDistance, Color.red, 0.25f);
    }

    private enum State
    {
        IDLE,
        COMBAT,
    }
}
