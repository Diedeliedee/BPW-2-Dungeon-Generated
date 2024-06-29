using UnityEngine;

public class Player : Entity
{
    private InputReader m_input;

    protected override void Awake()
    {
        base.Awake();
        m_input = FindObjectOfType<InputReader>();
    }

    public override void OnTurnStart()
    {
        Debug.Log("Player's Turn!!");
    }

    public override void DuringTurn()
    {
        if (m_input.movementPressed)
        {
            Move(ProcessDesiredInput(m_input.movementDirection));
        }

        if (m_currentMovement <= 0)
        {
            onTurnEnd.Invoke();
            m_currentMovement = m_movementPerTurn;
        }
    }
}
