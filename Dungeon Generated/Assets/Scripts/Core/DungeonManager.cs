

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] private Tilemap m_groundTilemap;
    [SerializeField] private TileBase m_groundTile;
    [Space]
    [SerializeField] private Transform m_entityParent;
    [Space]
    [SerializeField] private Player m_player;

    //  Sub-components:
    private NavigationManager m_navigation  = new();
    private List<Entity> m_entities         = new();

    //  Entity cache:
    private Queue<ITurnReceiver> m_turnReceivers    = new();
    private ITurnReceiver m_currentTurnReceiver     = null;

    //  Properties:
    public Player player => m_player;

    public void CreateFromComposite(Dictionary<Vector2Int, Tile> _composite)
    {
        //  Gather all the entities from their rooms.
        m_entities = FindObjectsByType<Entity>(FindObjectsSortMode.InstanceID).ToList();

        //  Drag all entities to a separate parent.
        foreach (var entity in m_entities)
        {
            entity.transform.parent = m_entityParent;
        }

        //  Decorating the scene based on the composite.
        foreach (var pair in _composite)
        {
            m_groundTilemap.SetTile(new Vector3Int(pair.Key.x, pair.Key.y, 0), m_groundTile);
        }

        //  Registering entities in the turn order.
        for (int i = 0; i < m_entities.Count; i++)
        {
            m_turnReceivers.Enqueue(m_entities[i]);
        }

        //  Registering entities in the tile map.
        m_navigation.RegisterTileMap(_composite);
        m_navigation.RegisterEntities(m_entities);
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

    public bool TargetInBounds(Vector2Int _coordinates)
    {
        return m_navigation.TargetInBounds(_coordinates);
    }
}
