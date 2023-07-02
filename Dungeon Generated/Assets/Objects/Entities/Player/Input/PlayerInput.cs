using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools;

public class PlayerInput
{
    [Header("Reference:")]
    [SerializeField] private Transform m_pathParent;
    [SerializeField] private GameObject m_pathMarker;

    //  Dependencies:
    private Player m_player = null;

    //  Run-time:
    private Vector2Int m_selectedCoordinates    = Vector2Int.zero;
    private List<GameObject> m_pathMarkers      = null;

    //  Properties:
    private Dungeon dungeon { get => GameManager.instance.dungeon; }
    private Camera camera   { get => GameManager.instance.camera; }

    public void Setup(Player player)
    {
        m_player        = player;
        m_pathMarkers   = new List<GameObject>();
    }

    public void OnClick(Vector2 mousePos)
    {
        var coordinates         = GetSelectedCoordinates(mousePos);

        m_selectedCoordinates   = coordinates;
        InstantiatePathIndicator(coordinates);
    }

    public void OnDrag(Vector2 mousePos)
    {
        var coordinates = GetSelectedCoordinates(mousePos);

        if (coordinates == m_selectedCoordinates) return;

        m_selectedCoordinates = coordinates;
        OnTileChange(coordinates);
    }

    public void OnRelease(Vector2 mousePos)
    {
        ClearPathIndicator();
        m_player.Move(GetPath(mousePos).last - m_player.coordinates);
    }

    private void OnTileChange(Vector2Int newTile)
    {
        InstantiatePathIndicator(GetPath(newTile).coordinates);
    }

    private Path GetPath(Vector2 mousePos)
    {
        //  Red out the path if the given direction is not valid to a locked vector?

        return GetPath(GetSelectedCoordinates(mousePos));
    }

    private Path GetPath(Vector2Int coordinates)
    {
        return new Path(m_player.coordinates, coordinates, dungeon);
    }

    private Vector2Int GetSelectedCoordinates(Vector2 mousePos)
    {
        var worldPoint = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -camera.transform.position.z));

        return Dungeon.PosToCoords(worldPoint);
    }

    private void InstantiatePathIndicator(params Vector2Int[] path)
    {
        ClearPathIndicator();

        for (int i = 0; i < path.Length; i++)
        {
            var spawnedMarker   = Object.Instantiate(m_pathMarker, Dungeon.CoordsToPos(path[i]), Quaternion.identity, m_pathParent);

            spawnedMarker.name  = $"Player path marker: {i}";
            m_pathMarkers       .Add(spawnedMarker);
        }
    }

    private void ClearPathIndicator()
    {
        for (int i = 0; i < m_pathMarkers.Count; i++) Object.Destroy(m_pathMarkers[i]);
        m_pathMarkers.Clear();
    }
}
