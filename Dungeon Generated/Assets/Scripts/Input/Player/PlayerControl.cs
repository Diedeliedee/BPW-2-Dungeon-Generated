using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Pathfinding;

[Serializable]
public partial class PlayerControl : ControlModule
{
    [SerializeField] private MovementDrag m_movement;
    [SerializeField] private SelectAttack m_attack;

    private Option m_activeOption = null;

    public override void Setup(TurnHandler turnHandler)
    {
        base.Setup(turnHandler);

        m_movement  .Setup();
        m_attack    .Setup();
    }

    public override void Activate(Action onFinish)
    {
        var player = GameManager.instance.entities.player;

        base.Activate(onFinish);

        m_movement  .Activate(OnSwitchOption);
        m_attack    .Activate(OnSwitchOption);

        m_turnHandler.StartTurn(player, Deactivate);

        //  Call event to activate UI stuff?
    }

    public override void Deactivate()
    {
        m_movement  .Deactivate();
        m_attack    .Deactivate();

        m_activeOption = null;

        //  Call event to disable UI stuff?

        base.Deactivate();
    }

    public override void Tick()
    {
        //  Option to skip your turn.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Deactivate();
        }

        m_activeOption?.Tick();
    }

    private void OnSwitchOption(Option option)
    {
        m_activeOption?.Cancel();
        m_activeOption = option;
    }

    public abstract class Option
    {
        private Action<Option> m_onStart = null;

        public virtual void Activate(Action<Option> onStart)
        {
            m_onStart = onStart;
        }

        public virtual void Tick() { }

        public virtual void Prioritize()
        {
            m_onStart.Invoke(this);
        }

        public virtual void Deactivate()
        {
            m_onStart = null;
        }

        public virtual void Cancel() { }

        protected Vector2Int GetSelectedCoordinates(Vector2 mousePos)
        {
            var cam         = GameManager.instance.camera.camera;
            var worldPoint  = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -cam.transform.position.z));

            return Dungeon.PosToCoords(worldPoint);
        }
    }
}
