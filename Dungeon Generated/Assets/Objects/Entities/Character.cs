using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Pathfinding;

public class Character : Entity
{
    [Header("Properties:")]
    [SerializeField] private float m_speed;

    //  Run-time:
    protected Coroutine m_activeRoutine             = null;
    protected TurnRequirements m_turnRequirements   = null;

    //  Core Events:
    public System.Action onTurnEnd = null;

    public bool activeTurn { get => m_turnRequirements != null; }

    public virtual void OnStartTurn(System.Action onFinish)
    {
        m_turnRequirements = new TurnRequirements(EndTurn);
        onTurnEnd = onFinish;
    }

    public virtual void EndTurn()
    {
        var endEvent = onTurnEnd;

        //  Make sure to reset everything before calling the turn end event, as the turn might be recursive.
        m_turnRequirements = null;
        onTurnEnd = null;

        endEvent?.Invoke();
    }

    public void MoveAlong(Pathfinder.Path path)
    {
        var time = path.length / m_speed;

        void OnTick(float progress)
        {
            position = Dungeon.CoordsToPos(path.Lerp(progress));
        }

        void OnFinish()
        {
            coordinates = path.last;
            m_activeRoutine = null;
            m_turnRequirements.hasMoved = true;
        }

        m_activeRoutine = StartCoroutine(Routines.Progression(time, OnTick, OnFinish));
    }
}