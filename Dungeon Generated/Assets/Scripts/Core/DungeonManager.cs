

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

    private Dictionary<Vector2Int, Tile> m_tileMap;

    private Queue<ITurnReceiver> m_turnReceivers = new();
    private ITurnReceiver m_currentTurnReceiver = null;

    private void Awake()
    {
        var entities = m_entities.GetComponentsInChildren<ITurnReceiver>();

        for (int i = 0; i < entities.Length; i++)
        {
            m_turnReceivers.Enqueue(entities[i]);
        }
    }

    public void CreateFromComposite(Dictionary<Vector2Int, Tile> _composite)
    {
        m_tileMap = _composite;

        foreach (var pair in m_tileMap)
        {
            var position = new Vector3Int(pair.Key.x, pair.Key.y, 0);

            m_groundTilemap.SetTile(position, m_groundTile);
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
}
