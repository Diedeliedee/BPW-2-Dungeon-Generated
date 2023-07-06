using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Pathfinding;

[System.Serializable]
public partial class PlayerControl : ControlModule
{
    [Header("Different Options:")]
    [SerializeField] private MovementDrag m_movement;

    //  Run-time:
    private Dictionary<System.Type, Option> m_options   = null;
    private Option m_activeOption                       = null;

    //  Cache:
    private Player m_player         = null;
    private EventManager m_events   = null;

    public override void Setup(TurnHandler turnHandler)
    {
        base.Setup(turnHandler);

        m_movement.Setup();

        m_options = new Dictionary<System.Type, Option>
        {
            { typeof(Player), m_movement }
        };

        m_player    = GameManager.instance.entities.player;
        m_events    = GameManager.instance.events;
    }

    public override void Activate(System.Action onFinish)
    {
        base.Activate(onFinish);

        m_events.onObjectClicked  += OnObjectClick;
        m_events.onObjectDrag     += OnObjectDrag;
        m_events.onObjectReleased += OnObjectRelease;

        m_turnHandler.StartTurn(m_player, Deactivate);

        //  Call event to activate UI stuff?
    }

    public override void Deactivate()
    {
        m_events.onObjectClicked  -= OnObjectClick;
        m_events.onObjectDrag     -= OnObjectDrag;
        m_events.onObjectReleased -= OnObjectRelease;

        //  Call event to disable UI stuff?

        base.Deactivate();
    }

    private void OnObjectClick(Object gameObject, Vector2 mousePos)
    {
        m_activeOption = m_options[gameObject.GetType()];
        m_activeOption?.OnClick(mousePos);
    }

    private void OnObjectDrag(Object gameObject, Vector2 mousePos)
    {
        if (m_activeOption == null) return;

        m_activeOption.OnDrag(mousePos);
    }

    private void OnObjectRelease(Object gameObject, Vector2 mousePos)
    {
        if (m_activeOption == null) return;

        m_activeOption?.OnRelease(mousePos);
        m_activeOption = null;
    }

    public abstract class Option
    {
        public abstract void OnClick(Vector2 mousePos);

        public abstract void OnDrag(Vector2 mousePos);

        public abstract void OnRelease(Vector2 mousePos);
    }
}
