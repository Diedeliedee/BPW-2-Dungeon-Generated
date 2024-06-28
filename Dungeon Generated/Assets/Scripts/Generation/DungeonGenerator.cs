using UnityEngine;
using System.Collections.Generic;

namespace DungeonGeneration
{
    public class DungeonGenerator
    {
        private Stage m_stage           = default;

        private Spawner m_spawner           = new();
        private Separator m_separator       = new();
        private Prioritizer m_prioritizer   = new();
        private Linker m_linker             = new();
        private Spanner m_spanner           = new();
        private Corridorer m_corrider       = new();
        private Includer m_includer         = new();
        private Compositor m_compositor     = new();

        public bool Iterate(GenerationSettings _settings, out Dictionary<Vector2Int, Tile> _composite)
        {
            _composite = null;

            switch (m_stage)
            {
                case Stage.ROOM_SPAWNING:
                    if (!m_spawner.IterateSpawningRooms(_settings)) break;
                    m_stage = Stage.ROOM_SEPARATION;
                    break;

                case Stage.ROOM_SEPARATION:
                    if (!m_separator.IterateSteeringRooms(m_spawner.rooms)) break;
                    m_stage = Stage.ROOM_PRIORITIZATION;
                    break;

                case Stage.ROOM_PRIORITIZATION:
                    m_prioritizer.CacheMainRooms(m_spawner.rooms, _settings.averageSizeMulitplier);
                    m_stage = Stage.ROOM_LINKING;
                    break;

                case Stage.ROOM_LINKING:
                    m_linker.CreateDelenautor(m_prioritizer.mainRooms);
                    m_stage = Stage.ROOM_SPANNING;
                    m_spanner.Setup(m_prioritizer.mainRooms[0]);
                    break;

                case Stage.ROOM_SPANNING:
                    if (!m_spanner.IterateSpanningTree(m_prioritizer.mainRooms)) break;
                    m_stage = Stage.ROOM_CORRIDORING;
                    break;

                case Stage.ROOM_CORRIDORING:
                    m_corrider.IterateCorridoring(m_prioritizer.mainRooms, _settings);
                    m_stage = Stage.ROOM_INCLUDING;
                    break;

                case Stage.ROOM_INCLUDING:
                    m_includer.IncludeIntersectingRooms(m_corrider.corridors, m_spawner.rooms);
                    m_stage = Stage.ROOM_COMPOSITING;
                    break;

                case Stage.ROOM_COMPOSITING:
                    _composite  = m_compositor.GetDungeonComposite(m_includer.intersectingRooms, m_corrider.corridors);
                    m_stage     = Stage.FINISHED;
                    return true;
            }
            return false;
        }

        public void Draw(Color _color)
        {
            m_spawner       .Draw(_color);
            m_prioritizer   .Draw(_color);
            m_linker        .Draw(_color);
            m_spanner       .Draw(Color.green);
            m_corrider      .Draw(_color);
            m_includer      .Draw(Color.blue);
        }

        private enum Stage
        {
            ROOM_SPAWNING,
            ROOM_SEPARATION,
            ROOM_PRIORITIZATION,
            ROOM_LINKING,
            ROOM_SPANNING,
            ROOM_CORRIDORING,
            ROOM_INCLUDING,
            ROOM_COMPOSITING,
            FINISHED
        }
    }
}
