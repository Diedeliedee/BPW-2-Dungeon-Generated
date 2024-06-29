

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] private Tilemap m_groundTilemap;
    [SerializeField] private TileBase m_groundTile;
    [Space]
    [SerializeField] private Transform m_entities;
    [Space]
    [SerializeField] private Player m_player;

    //  Sub-components:
    private NavigationManager m_navigation = new();

    //  Entity cache:
    private Queue<ITurnReceiver> m_turnReceivers    = new();
    private ITurnReceiver m_currentTurnReceiver     = null;

    //  Properties:
    public Player player => m_player;

    public void CreateFromComposite(Dictionary<Vector2Int, Tile> _composite)
    {
        var entities = m_entities.GetComponentsInChildren<Entity>();

        //  Decorating the scene based on the composite.
        foreach (var pair in _composite)
        {
            m_groundTilemap.SetTile(new Vector3Int(pair.Key.x, pair.Key.y, 0), m_groundTile);
        }

        //  Registering entities in the turn order.
        for (int i = 0; i < entities.Length; i++)
        {
            m_turnReceivers.Enqueue(entities[i]);
        }

        //  Registering entities in the tile map.
        m_navigation.RegisterTileMap(_composite);
        m_navigation.RegisterEntities(entities);

    }

    public void Tick()
    {
        if (m_currentTurnReceiver == null)
        {
            m_currentTurnReceiver = m_turnReceivers.Dequeue();
            m_currentTurnReceiver.OnTurnStart();
            m_currentTurnReceiver.onTurnEnd.Subscribe(OnTurnEnd);
        }
        else
        {
            m_currentTurnReceiver.DuringTurn();
        }
    }

    private void OnTurnEnd()
    {
        m_turnReceivers.Enqueue(m_currentTurnReceiver);

        m_currentTurnReceiver.onTurnEnd.Unsubscribe(OnTurnEnd);
        m_currentTurnReceiver = null;

    }

    public bool RequestMoveTo(MovementRequest _request, out MovementCallBack _callback)
    {
        return m_navigation.RequestMoveTo(_request, out _callback);
    }
}
