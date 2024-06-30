using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGeneration
{
    public class DungeonGenerator
    {
        private Stage m_stage = Stage.ROOM_SPAWNING;

        private Transform m_mainRoomParent      = null;
        private Transform m_liminalRoomParent   = null;
        private GenerationSettings m_settings   = default;

        private Spawner m_spawner           = new();
        private Separator m_separator       = new();
        private Linker m_linker             = new();
        private Spanner m_spanner           = new();
        private Corridorer m_corrider       = new();
        private Includer m_includer         = new();
        private Compositor m_compositor     = new();

        private List<MainRoom> m_mainRooms      = new();
        private List<Room> m_liminalRooms       = new();
        private List<Room> m_allRooms           = new();
        private List<Corridor> m_corridors      = new();
        private List<Room> m_intersectingRooms  = new();

        public float percent
        {
            get
            {
                int phases = 8;
                int currentPhase = (int)m_stage;

                return (float)currentPhase / (float)phases;
            }
        }

        public void Setup(GenerationSettings _settings, Transform _mainRoomParent, Transform _liminalRoomParent)
        {
            m_mainRoomParent    = _mainRoomParent;
            m_liminalRoomParent = _liminalRoomParent;
            m_settings          = _settings;

            m_mainRooms = m_mainRoomParent.GetComponentsInChildren<MainRoom>().ToList();
        }

        public bool Iterate(out Dictionary<Vector2Int, Tile> _composite)
        {
            _composite = null;

            switch (m_stage)
            {
                case Stage.ROOM_SPAWNING:
                    if (!m_spawner.IterateSpawningRooms(m_settings, m_liminalRoomParent)) break;

                    //  Gather liminal rooms.
                    m_liminalRooms  = m_liminalRoomParent.GetComponentsInChildren<Room>().ToList();

                    //  Combine main, and liminal rooms for ease.
                    m_allRooms.AddRange(m_mainRooms);
                    m_allRooms.AddRange(m_liminalRooms);
                    m_stage = Stage.ROOM_SEPARATION;
                    break;

                case Stage.ROOM_SEPARATION:
                    if (!m_separator.IterateSteeringRooms(m_allRooms)) break;
                    m_stage = Stage.ROOM_LINKING;
                    break;

                case Stage.ROOM_LINKING:
                    //  Create a delaunay triangulation from the main rooms.
                    m_linker.CreateDelenautor(m_mainRooms);

                    //  Select the main room for the spanner.
                    m_spanner.SelectStartRoom(m_mainRooms[0]);

                    m_stage = Stage.ROOM_SPANNING;
                    break;

                case Stage.ROOM_SPANNING:
                    if (!m_spanner.IterateSpanningTree(m_mainRooms)) break;
                    m_stage = Stage.ROOM_CORRIDORING;
                    break;

                case Stage.ROOM_CORRIDORING:
                    m_corrider.IterateCorridoring(m_mainRooms, m_corridors, m_settings);
                    m_stage = Stage.ROOM_INCLUDING;
                    break;

                case Stage.ROOM_INCLUDING:
                    m_includer.IncludeIntersectingRooms(m_corridors, m_liminalRooms, m_intersectingRooms);
                    m_stage = Stage.ROOM_COMPOSITING;
                    break;

                case Stage.ROOM_COMPOSITING:
                    var compositeList = new List<ICompositePart>();

                    compositeList.AddRange(m_mainRooms);
                    compositeList.AddRange(m_intersectingRooms);
                    compositeList.AddRange(m_corridors);
                    _composite = m_compositor.GetDungeonComposite(compositeList);

                    m_stage = Stage.FINISHED;
                    return true;
            }
            return false;
        }

        public void Draw(Color _color)
        {
            if (!Application.isPlaying) return;

            foreach (var corridor in m_corridors)
            {
                corridor.Draw(_color);
            }
        }

        private enum Stage
        {
            ROOM_SPAWNING       = 1,
            ROOM_SEPARATION     = 2,
            ROOM_LINKING        = 3,
            ROOM_SPANNING       = 4,
            ROOM_CORRIDORING    = 5,
            ROOM_INCLUDING      = 6,
            ROOM_COMPOSITING    = 7,
            FINISHED            = 8
        }
    }
}
