using UnityEngine;

/// <summary>
/// Class for ease of acces to relevant player input data.
/// </summary>
public class InputReader : MonoBehaviour
{
    private PlayerInputActions m_actions = null;

    public bool movementPressed         => m_actions.Player.Movement.triggered;
    public Vector2 movementDirection    => m_actions.Player.Movement.ReadValue<Vector2>();

    private void Awake()
    {
        m_actions = new PlayerInputActions();
    }

    private void Start()
    {
        m_actions.Enable();
    }
}
