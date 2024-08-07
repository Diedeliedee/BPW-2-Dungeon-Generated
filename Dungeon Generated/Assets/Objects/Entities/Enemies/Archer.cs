using Joeri.Tools;
using Joeri.Tools.Debugging;
using UnityEngine;

public class Archer : Entity
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
        base.OnTurnStart();

        var distanceToPlayer = Vector2.Distance(transform.position, m_dungeon.player.transform.position);

        //  Deciding what to do this turn.
        if      (distanceToPlayer < m_aggroDistance)    m_state = State.COMBAT;
        else if (distanceToPlayer > m_aggroDistance)    m_state = State.IDLE;
    }

    public override void DuringTurn()
    {
        switch (m_state)
        {
            case State.IDLE:
                EndTurn();
                break;

            case State.COMBAT:
                //  To prevent lightning fast stuff, wait some time before each choice.
                if (!m_choiceTimer.ResetOnReach(Time.deltaTime)) break;

                //  Calculate desider input.
                var direction       = -(m_dungeon.player.transform.position - transform.position).normalized;
                var input           = NavigationManager.ProcessDesiredInput(direction);
                var playerPos       = m_dungeon.player.coordinates;

                if (!Move(input, out MovementCallBack _callback))
                {
                    m_dungeon.player.Damage(m_damage);
                    EndTurn();
                }
                if (Vector2Int.Distance(coordinates, playerPos) >= m_aggroDistance)
                {
                    m_dungeon.player.Damage(m_damage);
                    EndTurn();
                }
                if (currentMovement <= 0)
                {
                    m_dungeon.player.Damage(m_damage);
                    EndTurn();
                }
                break;
        }
    }

    public override void Damage(int _damage)
    {
        m_dungeon.Deregister(this);
        gameObject.SetActive(false);
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
