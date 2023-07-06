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
    protected Coroutine m_activeRoutine                         = null;
    protected TurnHandler.TurnRequirements m_turnRequirements   = null;

    public bool activeTurn { get => m_turnRequirements != null; }

    public virtual void OnStartTurn(TurnHandler.TurnRequirements finishRequirements)
    {
        m_turnRequirements = finishRequirements;
        m_turnRequirements.onTurnComplete += EndTurn;
    }

    public virtual void EndTurn()
    {
        //  Juggle memory, then execute final event.
        var req = m_turnRequirements;

        m_turnRequirements = null;
        req.onTurnFinish.Invoke();
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
            coordinates                 = path.last;
            m_activeRoutine             = null;
            m_turnRequirements.hasMoved = true;
        }

        m_activeRoutine = StartCoroutine(Routines.Progression(time, OnTick, OnFinish));
    }
}