using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TurnManager
{
    //  Run-time:
    public LinkedList<Character> turnOrder = new LinkedList<Character>();

    //  Cache:
    private Player m_player = null;

    //  Events:
    public Action onTurnEnd = null;

    #region Properties
    public int enemyCount { get; private set; }

    private LinkedListNode<Character> playerNode    { get => turnOrder.Find(m_player); }
    private Character activeEntity                  { get => turnOrder.First(); }
    #endregion

    public TurnManager(Player player)
    {
        m_player    = player;
        turnOrder   .AddFirst(player);
    }

    /// <summary>
    /// Calls the next entity's OnStartTurn(...) function, and sends out a callback whenever the turn has ended.
    /// </summary>
    public void StartNextTurn(Action<Character, Action> onTurnPrepare, Action onFinish)
    {
        var entity = activeEntity;

        void OnTurnStart()
        {
            //  Debug:
            Debug.Log($"New turn started! It's {entity}'s turn!", entity);

            //  Call turn function in entity.
            entity.OnStartTurn(OnTurnEnd);
        }

        void OnTurnEnd()
        {
            //  Set up the next entity's turn.
            turnOrder   .RemoveFirst();
            turnOrder   .AddLast(entity);

            //  Callback event.
            onFinish    .Invoke();
        }

        //  Call prepare event.
        onTurnPrepare.Invoke(entity, OnTurnStart);
    }

    /// <summary>
    /// Enqueues an enemy in the turnOrder, but before the player, assuming that the enemy entered combat by a player's actions.
    /// </summary>
    public void Enqueue(Enemy enemy)
    {
        turnOrder.AddBefore(playerNode, enemy);
        enemyCount++;
    }

    /// <summary>
    /// Removes an enemy from the list.
    /// </summary>
    public void Remove(Enemy enemy)
    {
        turnOrder.Remove(enemy);
        enemyCount--;
    }
}
