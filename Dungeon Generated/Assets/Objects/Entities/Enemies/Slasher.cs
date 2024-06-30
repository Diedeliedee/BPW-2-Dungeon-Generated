using Joeri.Tools;
using Joeri.Tools.Debugging;
using UnityEngine;

public class Slasher : Entity
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
        Debug.Log("Slasher's Turn!!");

        var distanceToPlayer = Vector2.Distance(transform.position, m_dungeon.player.transform.position);

        //  Deciding what to do this turn.
        if      (distanceToPlayer < m_aggroDistance)    m_state = State.COMBAT;
        else if (distanceToPlayer > m_aggroDistance)    m_state = State.IDLE;

        m_onTurnStart.Invoke();
    }

    public override void DuringTurn()
    {
        switch (m_state)
        {
            case State.IDLE:
                onTurnEnd.Invoke();
                break;

            case State.COMBAT:
                //  To prevent lightning fast stuff, wait some time before each choice.
                if (!m_choiceTimer.ResetOnReach(Time.deltaTime)) break;

                //  Calculate desider input.
                var direction   = (m_dungeon.player.transform.position - transform.position).normalized;
                var input       = NavigationManager.ProcessDesiredInput(direction);

                //  If we can't move in the desired input..
                if (!Move(input, out MovementCallBack _callback))
                {
                    //  And that move is caused by the player standing right beside us..
                    if (_callback.condition == MovementCallBack.Condition.OCCUPIED && _callback.targetTile.occupation is Player _player)
                    {
                        //  Damage the player.
                        _player.Damage(5);
                    }

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
