

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
        int count = 0;

        while (m_currentTurnReceiver == null)
        {
            var next = m_turnReceivers.Dequeue();

            if (!m_entities.Contains(next))
            {
                count++;
                continue;
            }

            m_currentTurnReceiver = next;
            m_currentTurnReceiver.OnTurnStart();
            m_currentTurnReceiver.endTurnCallback.Subscribe(OnTurnEnd);

            if (count >= 100)
            {
                Debug.LogError("No entities present in the turn order!!");
                return;
            }
        }

        m_currentTurnReceiver.DuringTurn();
    }

    private void OnTurnEnd()
    {
        m_turnReceivers.Enqueue(m_currentTurnReceiver);

        m_currentTurnReceiver.endTurnCallback.Unsubscribe(OnTurnEnd);
        m_currentTurnReceiver = null;

    }

    public bool RequestMoveTo(MovementRequest _request, out MovementCallBack _callback)
    {
        return m_navigation.RequestMoveTo(_request, out _callback);
    }

    public void AttackAt(Vector2Int _coordinates, int _damage)
    {
        if (!m_navigation.TryGetTile(_coordinates, out Tile _tile)) return;

        if (_tile == null) return;
        if (_tile.occupation == null) return;
        _tile.occupation.Damage(_damage);
    }

    public void Deregister(Entity _entity)
    {
        m_entities.Remove(_entity);
        m_navigation.TryGetTile(_entity.coordinates, out Tile _tile);
        _tile.occupation = null;
    }

    public bool TargetInBounds(Vector2Int _coordinates)
    {
        return m_navigation.TryGetTile(_coordinates, out Tile _tile);
    }
}
