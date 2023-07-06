﻿using Joeri.Tools.Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl
{
    [System.Serializable]
    public class MovementDrag : Option
    {
        [Header("Reference:")]
        [SerializeField] private GameObject m_pathMarker;
        [SerializeField] private Transform m_pathParent;

        //  Dependencies:
        private Player m_player = null;

        //  Run-time:
        private MovementSelection m_selection   = null;
        private List<GameObject> m_pathMarkers  = new List<GameObject>();

        public void Setup()
        {
            m_player = GameManager.instance.entities.player;
        }

        #region Events
        public override void OnClick(Vector2 mousePos)
        {
            m_selection = new MovementSelection
                (
                    mousePos,
                    m_player.coordinates,
                    OnTileChange,
                    GameManager.instance.camera.camera,
                    GameManager.instance.dungeon
                );

            InstantiatePathIndicator(m_selection.GetPath().coordinates);
        }

        public override void OnDrag(Vector2 mousePos)
        {
            m_selection.UpdateMousePosition(mousePos);
        }

        public override void OnRelease(Vector2 mousePos)
        {
            ClearPathIndicator();
            if (m_selection.GetOffset(m_player.coordinates) != Vector2Int.zero)
            {
                var movementPath = new Pathfinder.Path(m_selection.GetPath().coordinates);

                m_player.MoveAlong(movementPath);
            }
            m_selection = null;
        }

        private void OnTileChange(Vector2Int newTile)
        {
            InstantiatePathIndicator(m_selection.GetPath().coordinates);
        }
        #endregion

        #region Path Instantiation
        private void InstantiatePathIndicator(params Vector2Int[] path)
        {
            ClearPathIndicator();

            for (int i = 0; i < path.Length; i++)
            {
                var spawnedMarker = Object.Instantiate(m_pathMarker, Dungeon.CoordsToPos(path[i]), Quaternion.identity, m_pathParent);

                spawnedMarker.name = $"Player path marker: {i}";
                m_pathMarkers.Add(spawnedMarker);
            }
        }

        private void ClearPathIndicator()
        {
            for (int i = 0; i < m_pathMarkers.Count; i++) Object.Destroy(m_pathMarkers[i]);
            m_pathMarkers.Clear();
        }
        #endregion

        private class MovementSelection
        {
            public Vector2Int coordinates           = Vector2Int.zero;
            private Vector2Int m_playerCoordinates  = Vector2Int.zero;

            public readonly System.Action<Vector2Int> onTileChange = null;

            private Camera m_camera     = null;
            private Dungeon m_dungeon   = null;

            public MovementSelection(Vector2 mousePos, Vector2Int playerCoords, System.Action<Vector2Int> onTileChange, Camera camera, Dungeon dungeon)
            {
                m_camera    = camera;
                m_dungeon   = dungeon;

                this.onTileChange = onTileChange;

                m_playerCoordinates = playerCoords;
                coordinates         = GetSelectedCoordinates(mousePos);
            }

            public void UpdateMousePosition(Vector2 mousePos)
            {
                var newCoordinates = GetSelectedCoordinates(mousePos);

                if (newCoordinates == coordinates) return;

                coordinates = newCoordinates;
                onTileChange.Invoke(newCoordinates);
            }

            public Vector2Int GetSelectedCoordinates(Vector2 mousePos)
            {
                var worldPoint = m_camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -m_camera.transform.position.z));

                return Dungeon.PosToCoords(worldPoint);
            }

            public Path GetPath()
            {
                return new Path(m_playerCoordinates, coordinates, m_dungeon);
            }

            public Vector2Int GetOffset(Vector2Int playerCoordinates)
            {
                return coordinates - playerCoordinates;
            }
        }
    }
}