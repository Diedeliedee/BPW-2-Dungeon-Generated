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
    [Space]
    [SerializeField] private AudioClip m_movementAudio;

    //  Run-time:
    protected Coroutine m_activeRoutine                         = null;
    protected TurnHandler.TurnRequirements m_turnRequirements   = null;

    public bool activeTurn { get => m_turnRequirements != null; }

    public virtual void OnStartTurn(TurnHandler.TurnRequirements finishRequirements)
    {
        //  Initializing variables.
        m_turnRequirements = finishRequirements;
    }

    public virtual void EndTurn()
    {
        //  Resetting variables.
        m_turnRequirements = null;

        //  Clean-up.
        StopAllCoroutines();
        Move(new Vector2Int(0, 0));
    }

    public void MoveAlong(Pathfinder.Path path)
    {
        MoveAlong(path, null);
    }

    public virtual void MoveAlong(Pathfinder.Path path, Action onFinish)
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
            StopSound();

            onFinish?.Invoke();
        }

        PlaySound(m_movementAudio, 0.25f, 1.1f);
        m_activeRoutine = StartCoroutine(Routines.Progression(time, OnTick, OnFinish));
    }

    public void PerformAttack(Attack attack, Vector2Int coords, Action onFinish)
    {
        void OnFinish()
        {
            m_turnRequirements.hasAttacked = true;

            onFinish?.Invoke();
        }

        var attackInstance = GameManager.instance.effects.Instantiate<AttackInstance>(attack.instance, coords, Dungeon.GetGeneralDirection(coords - coordinates));

        attackInstance.Activate(attack.damage, OnFinish);
    }
}