using UnityEngine;

/// <summary>
/// Class for ease of acces to relevant player input data.
/// </summary>
public class InputReader : MonoBehaviour
{
    private PlayerInputActions m_actions = null;

    public bool movementPressed         => m_actions.Player.Movement.triggered;
    public Vector2 movementDirection    => m_actions.Player.Movement.ReadValue<Vector2>();

    public bool selectorPressed         => m_actions.Player.Selector.triggered;
    public Vector2 selectorDirection    => m_actions.Player.Selector.ReadValue<Vector2>();

    public bool confirmedSelection      => m_actions.Player.Confirm.triggered;

    private void Awake()
    {
        m_actions = new PlayerInputActions();
    }

    private void Start()
    {
        m_actions.Enable();
    }
}
