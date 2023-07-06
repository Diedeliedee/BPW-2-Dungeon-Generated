using System;

public abstract class ControlModule
{
    protected bool m_active             = false;
    protected TurnHandler m_turnHandler = null;

    private Action m_onFinish   = null;

    /// <summary>
    /// Set-up for control module.
    /// </summary>
    public virtual void Setup(TurnHandler turnHandler)
    {
        m_turnHandler = turnHandler;
    }

    /// <summary>
    /// Activates control module, starting the current controller's turn.
    /// </summary>
    public virtual void Activate(Action onFinish)
    {
        m_active    = true;
        m_onFinish  = onFinish;
    }

    /// <summary>
    /// Deactivates control module, ending the current controller's turn. Call base after resetting values!
    /// </summary>
    public virtual void Deactivate()
    {
        var onFinish = m_onFinish;

        m_turnHandler.FinishCurrentTurn();
        m_active    = false;
        m_onFinish  = null;

        onFinish.Invoke();
    }
}
