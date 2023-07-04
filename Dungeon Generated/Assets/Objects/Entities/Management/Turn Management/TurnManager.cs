﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TurnManager
{
    //  Run-time:
    public LinkedList<Entity> turnOrder = new LinkedList<Entity>();

    //  Cache:
    private Player m_player = null;

    //  Events:
    public Action onTurnEnd = null;

    #region Properties
    public int enemyCount                       { get; private set; }

    private LinkedListNode<Entity> playerNode   { get => turnOrder.Find(m_player); }
    private Entity activeEntity                 { get => turnOrder.First(); }
    #endregion

    public TurnManager(Player player)
    {
        m_player    = player;
        turnOrder   .AddFirst(player);
    }

    /// <summary>
    /// Calls the next entity's OnStartTurn(...) function, and sends out a callback whenever the turn has ended.
    /// </summary>
    public void StartNextTurn(Action onFinish)
    {
        var entity = activeEntity;

        void OnTurnEnd()
        {
            //  Set up the next entity's turn.
            turnOrder   .RemoveFirst();
            turnOrder   .AddLast(entity);

            //  Callback event.
            onFinish    .Invoke();
        }

        //  Debug:
        Debug.Log($"New turn started! It's {entity}'s turn!", entity);

        //  Call turn function in entity.
        entity.OnStartTurn(OnTurnEnd);
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