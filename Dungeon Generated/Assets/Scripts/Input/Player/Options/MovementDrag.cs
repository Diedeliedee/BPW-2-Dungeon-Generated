using Joeri.Tools.Pathfinding;
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

            InstantiatePathIndicator();
        }

        public override void OnDrag(Vector2 mousePos)
        {
            m_selection.UpdateCoordinates(mousePos);
        }

        public override void OnRelease(Vector2 mousePos)
        {
            ClearPathIndicator();

            if (m_selection.path != null && m_selection.offset != Vector2Int.zero)
            {
                m_player.MoveAlong(m_selection.path);
            }

            m_selection = null;
        }

        private void OnTileChange(Vector2Int newTile)
        {
            InstantiatePathIndicator();
        }
        #endregion

        #region Path Instantiation
        private void InstantiatePathIndicator()
        {
            ClearPathIndicator();

            if (m_selection.path == null) return;
            for (int i = 0; i < m_selection.path.coordinates.Length; i++)
            {
                var spawnPos        = Dungeon.CoordsToPos(m_selection.path.coordinates[i]);
                var spawnedMarker   = Object.Instantiate(m_pathMarker, spawnPos, Quaternion.identity, m_pathParent);

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
            //  Dependencies:
            private Camera m_camera         = null;
            private Dungeon m_dungeon       = null;
            private Pathfinder m_pathFinder = null;

            //  Properties:
            public Pathfinder.Path path           = null;
            public Vector2Int coordinates           = Vector2Int.zero;
            private Vector2Int m_playerCoordinates  = Vector2Int.zero;

            //  Events:
            public readonly System.Action<Vector2Int> onTileChange = null;

            public Vector2Int offset { get => coordinates - m_playerCoordinates; }

            public MovementSelection(Vector2 mousePos, Vector2Int playerCoords, System.Action<Vector2Int> onTileChange, Camera camera, Dungeon dungeon)
            {
                m_camera        = camera;
                m_dungeon       = dungeon;
                m_pathFinder    = new Pathfinder(m_dungeon.HasTile, m_dungeon.allowedDirections);

                m_playerCoordinates = playerCoords;
                coordinates         = GetSelectedCoordinates(mousePos);

                this.onTileChange = onTileChange;
            }

            public void UpdateCoordinates(Vector2 mousePos)
            {
                var newCoordinates = GetSelectedCoordinates(mousePos);

                if (newCoordinates == coordinates) return;

                coordinates = newCoordinates;
                onTileChange.Invoke(newCoordinates);

                if (offset != Vector2Int.zero) path = m_pathFinder.FindPath(m_playerCoordinates, coordinates);
            }

            private Vector2Int GetSelectedCoordinates(Vector2 mousePos)
            {
                var worldPoint = m_camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -m_camera.transform.position.z));

                return Dungeon.PosToCoords(worldPoint);
            }
        }
    }
}