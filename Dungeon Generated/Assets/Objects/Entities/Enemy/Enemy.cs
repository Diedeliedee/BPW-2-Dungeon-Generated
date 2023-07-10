using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Debugging;
using Joeri.Tools.Structure;

public class Enemy : Character
{
    [Header("Enemy Properties:")]
    [SerializeField] private float m_discoverDistance = 4.5f;
    [SerializeField] private LayerMask m_wallLayer;

    public override void Setup()
    {
        base.Setup();

        GameManager.instance.events.onPlayerMoved += EvaluatePlayerInRange;
    }

    public override void OnStartTurn(TurnHandler.TurnRequirements turnReq)
    {
        base.OnStartTurn(turnReq);
    }

    public override void EndTurn()
    {
        base.EndTurn();
        //  No idea what to put here.
    }

    public override void OnDeath()
    {
        base.OnDeath();

        //  Play sound?
        GameManager.instance.events.onEnemyDespawn(this);
        Destroy(gameObject);
    }

    private void EvaluatePlayerInRange(Vector2Int newCoordinates)
    {
        var offsetToPlayer  = newCoordinates - coordinates;
        var distance        = Vector2.Distance(newCoordinates, coordinates);

        if (distance > m_discoverDistance) return;
        if (Physics2D.Raycast(position, offsetToPlayer, distance, m_wallLayer)) return;
        GameManager.instance.events.onPlayerSpotted?.Invoke(this);
    }

    private void OnDrawGizmosSelected()
    {
        GizmoTools.DrawSphere(position, m_discoverDistance, Color.red, 0.5f, true, 0.5f);
    }
}
