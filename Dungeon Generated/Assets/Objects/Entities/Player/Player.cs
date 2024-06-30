using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : Entity
{
    private InputReader m_input;
    private Highlighter m_highlighter;

    private State m_state = default;

    protected override void Awake()
    {
        base.Awake();

        m_input         = FindObjectOfType<InputReader>();
        m_highlighter   = GetComponentInChildren<Highlighter>();
    }

    private void Start()
    {
        m_highlighter.Deactivate();
    }

    public override void OnTurnStart()
    {
        Debug.Log("Player's Turn!!");

        m_onTurnStart.Invoke();
        m_state = State.MOVING;
    }

    public override void DuringTurn()
    {
        if (m_state == State.SELECTING && m_input.movementPressed)
        {
            m_state = State.MOVING;
            m_highlighter.Deactivate();
        }
        else if (m_state == State.MOVING && m_input.selectorPressed)
        {
            m_state = State.SELECTING;
            m_highlighter.Activate(coordinates);
        }

        switch (m_state)
        {
            case State.MOVING:
                if (m_input.movementPressed)
                {
                    Move(NavigationManager.ProcessDesiredInput(m_input.movementDirection), out MovementCallBack _callback);
                }

                if (Input.GetKeyDown(KeyCode.Return))   //  Debug purposes.
                {
                    EndTurn();
                }
                break;

            case State.SELECTING:
                if (m_input.selectorPressed)
                {
                    m_highlighter.Tick(NavigationManager.ProcessDesiredInput(m_input.selectorDirection));
                }

                if (Input.GetKeyDown(KeyCode.Return))   //  Debug purposes.
                {
                    
                }
                break;
        }

    }

    public void EndTurn()
    {
        onTurnEnd.Invoke();
        m_currentMovement = m_movementPerTurn;
    }

    private enum State
    {
        MOVING,
        SELECTING,
    }
}
