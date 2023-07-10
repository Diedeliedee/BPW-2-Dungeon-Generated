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

        //  Properties:
        private EventManager events { get => GameManager.instance.events; }

        public void Setup()
        {
            m_player = GameManager.instance.entities.player;
        }

        public override void Activate(System.Action<Option> onStart)
        {
            base.Activate(onStart);

            events.onPlayerClicked += OnClick;
        }

        public override void Deactivate()
        {
            events.onPlayerClicked  -= OnClick;
            Cancel();

            base.Deactivate();
        }

        #region Events
        private void OnClick(Vector2 mousePos)
        {
            Prioritize();

            m_selection = new MovementSelection
                (
                    m_player.coordinates,
                    OnTileChange,
                    GameManager.instance.dungeon
                );

            InstantiatePathIndicator(GetSelectedCoordinates(mousePos));

            events.onPlayerDragged  += OnDrag;
            events.onPlayerReleased += OnRelease;
        }

        public void OnDrag(Vector2 mousePos)
        {
            m_selection.UpdateCoordinates(GetSelectedCoordinates(mousePos));
        }

        private void OnRelease(Vector2 mousePos)
        {
            if (m_selection.path != null && m_selection.offset != Vector2Int.zero)
            {
                m_player.MoveAlong(m_selection.path);
            }
            Deactivate();
        }

        public override void Cancel()
        {
            m_selection             = null;
            events.onPlayerDragged  -= OnDrag;
            events.onPlayerReleased -= OnRelease;
            ClearPathIndicator();

            base.Cancel();
        }

        private void OnTileChange(Vector2Int newTile)
        {
            if (m_selection.path == null) return;
            InstantiatePathIndicator(m_selection.path.coordinates);
        }
        #endregion

        #region Path Instantiation
        private void InstantiatePathIndicator(params Vector2Int[] coordinates)
        {
            ClearPathIndicator();

            for (int i = 0; i < coordinates.Length; i++)
            {
                var spawnPos        = Dungeon.CoordsToPos(coordinates[i]);
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
            private Dungeon m_dungeon       = null;
            private Pathfinder m_pathFinder = null;

            //  Properties:
            public Pathfinder.Path path             = null;
            public Vector2Int coordinates           = Vector2Int.zero;
            private Vector2Int m_playerCoordinates  = Vector2Int.zero;

            //  Events:
            public readonly System.Action<Vector2Int> onTileChange = null;

            public Vector2Int offset { get => coordinates - m_playerCoordinates; }

            public MovementSelection(Vector2Int playerCoords, System.Action<Vector2Int> onTileChange, Dungeon dungeon)
            {
                m_dungeon       = dungeon;
                m_pathFinder    = new Pathfinder(m_dungeon.HasTile, m_dungeon.allowedDirections);

                m_playerCoordinates = playerCoords;
                coordinates         = playerCoords;

                this.onTileChange = onTileChange;
            }

            public void UpdateCoordinates(Vector2Int newCoordinates)
            {
                if (newCoordinates == coordinates) return;

                                                coordinates = newCoordinates;
                if (offset != Vector2Int.zero)  path        = m_pathFinder.FindPath(m_playerCoordinates, coordinates);

                onTileChange.Invoke(newCoordinates);
            }
        }
    }
}