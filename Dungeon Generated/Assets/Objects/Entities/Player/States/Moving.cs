using Joeri.Tools.Structure.StateMachine.Simple;
using UnityEngine;

public class Moving : State
{
    private InputReader m_input                             = null;
    private System.Action<Vector2Int> m_movementCallback    = null;

    public Moving(InputReader _input, System.Action<Vector2Int> _movementCallback)
    {
        m_input             = _input;
        m_movementCallback  = _movementCallback;
    }

    public override void OnTick()
    {
        if (m_input.selectorPressed)
        {
            Switch(typeof(Selecting));
            return;
        }
        if (!m_input.movementPressed) return;

        m_movementCallback.Invoke(NavigationManager.ProcessDesiredInput(m_input.movementDirection));
    }
}