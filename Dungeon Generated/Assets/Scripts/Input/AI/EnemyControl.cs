using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Pathfinding;

[System.Serializable]
public class EnemyControl : ControlModule
{
    private List<Enemy> m_alarmedEnemies    = new List<Enemy>();
    private int m_enemyIndex                = 0;

    private Pathfinder m_pathfinder = null;

    /// <summary>
    /// Amount of enemies currently active and in combat.
    /// </summary>
    public int enemyCount { get => m_alarmedEnemies.Count; }

    public override void Setup(TurnHandler turnHandler)
    {
        var dungeon = GameManager.instance.dungeon;

        base.Setup(turnHandler);

        m_pathfinder = new Pathfinder(dungeon.HasTile, dungeon.allowedDirections);

        GameManager.instance.events.onPlayerSpotted += Enqueue;
        GameManager.instance.events.onEnemyDespawn  += Remove;
    }

    public override void Activate(System.Action onFinish)
    {
        //  Initialize turn loop.
        base.Activate(onFinish);
        StartEnemyTurn();
    }

    private void StartEnemyTurn()
    {
        var player  = GameManager.instance.entities.player;
        var enemy   = m_alarmedEnemies[m_enemyIndex++];

        void Walk()
        {
            var desiredCoords   = player.coordinates + Dungeon.GetGeneralDirection(enemy.coordinates - player.coordinates);
            var pathToPlayer    = m_pathfinder.FindPath(enemy.coordinates, desiredCoords);

            if (pathToPlayer == null || Vector2.Distance(player.coordinates, enemy.coordinates) <= 1f) OnEnemyTurnFinish();
            else enemy.MoveAlong(pathToPlayer, Attack);
        }

        void Attack()
        {
            if (Vector2.Distance(player.coordinates, enemy.coordinates) <= 1f)
            {
                enemy.PerformAttack(enemy.defaultAttack, player.coordinates, null);
            }
            else
            {
                OnEnemyTurnFinish();
            }
        }

        //  Increment enemy index, and start their turn.
        m_turnHandler.StartTurn(enemy, OnEnemyTurnFinish);

        /// For some unkown reason the enemy decides to start it's turn after it's finished walking.
        /// Very cool Mister Enemy.

        //  Start with a walk.
        Walk();
    }

    private void OnEnemyTurnFinish()
    {
        //  Deactivate if all turns have been completed.
        if (m_enemyIndex >= enemyCount) { Deactivate(); return; }
        StartEnemyTurn();
    }
    public override void Deactivate()
    {
        base.Deactivate();
        m_enemyIndex = 0;
    }

    public void Enqueue(Enemy enemy)
    {
        if (m_alarmedEnemies.Contains(enemy)) return;
        m_alarmedEnemies.Add(enemy);
    }

    public void Remove(Enemy enemy)
    {
        m_alarmedEnemies.Remove(enemy);
    }
}
