

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

    //  THE Tilemap:
    private Dictionary<Vector2Int, Tile> m_tileMap;

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
        m_tileMap = _composite;

        //  Registering the entities.
        for (int i = 0; i < entities.Length; i++)
        {
            //  Registering in the turn order.
            m_turnReceivers.Enqueue(entities[i]);

            //  Registering to a tile.
            if (!m_tileMap.TryGetValue(entities[i].coordinates, out Tile _tile))
            {
                Debug.LogWarning($"Warning: Entity: {entities[i].gameObject.name} is standing outside of bounds, collision disables.");
            }
            else
            {
                m_tileMap[entities[i].coordinates].occupation = entities[i];
            }

            //  Snap entities if they're not aligned on thegrid in editor.
            entities[i].Snap();
        }
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

    public bool RequestMoveTo(Entity _entity, Vector2Int _coordinates)
    {
        var entityInBounds = m_tileMap.TryGetValue(_entity.coordinates, out Tile _currentTile);
        var targetInBounds = m_tileMap.TryGetValue(_coordinates, out Tile _targetTile);

        void ConfirmMove()
        {
            _entity.coordinates     = _coordinates;
            _currentTile.occupation = null;
            _targetTile.occupation  = _entity;
        }

        //  If the entity is standing out of bounds, permit any movement.
        if (!entityInBounds)
        {
            ConfirmMove();
            return true;
        }

        //  If the target tile is out of bounds, disallow movement
        if (!targetInBounds)
        {
            return false;
        }

        //  If the target tile is occupied, disallow movement.
        if (_targetTile.occupation != null)
        {
            return false;
        }

        //  Permit movement if not of the guard clauses are true.
        ConfirmMove();
        return true;
    }
}
