using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyControl : ControlModule
{
    private List<Enemy> m_alarmedEnemies    = new List<Enemy>();
    private int m_enemyIndex                = 0;

    /// <summary>
    /// Amount of enemies currently active and in combat.
    /// </summary>
    public int enemyCount { get => m_alarmedEnemies.Count; }

    public override void Setup(TurnHandler turnHandler)
    {
        base.Setup(turnHandler);

        GameManager.instance.events.onPlayerSpotted += Enqueue;
        GameManager.instance.events.onEnemyDespawn  += Remove;
    }

    public override void Activate(System.Action onFinish)
    {
        void StartEnemyTurn()
        {
            //  Increment enemy index, and start their turn.
            m_turnHandler.StartTurn(m_alarmedEnemies[m_enemyIndex++], OnTurnFinish);
        }
        
        void OnTurnFinish()
        {
            //  Deactivate if all turns have been completed.
            if (m_enemyIndex >= enemyCount) { Deactivate(); return; }
            StartEnemyTurn();
        }

        //  Initialize turn loop.
        base.Activate(onFinish);
        StartEnemyTurn();
    }

    public override void Deactivate()
    {
        m_enemyIndex = 0;
        base.Deactivate();
    }

    public void Enqueue(Enemy enemy)    => m_alarmedEnemies.Add(enemy);

    public void Remove(Enemy enemy)     => m_alarmedEnemies.Remove(enemy);
}
