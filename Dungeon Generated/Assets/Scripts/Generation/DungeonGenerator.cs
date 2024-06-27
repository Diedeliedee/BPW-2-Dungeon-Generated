using UnityEngine;
using System.Collections.Generic;
using Joeri.Tools.Utilities;

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

        private List<Room> rooms => m_spawner.rooms;

        public void Iterate(GenerationSettings _settings)
        {
            switch (m_stage)
            {
                case Stage.ROOM_SPAWNING:
                    //  Spawn a new room.
                    m_spawner.SpawnRoom(_settings);

                    //  If no more rooms need to be spawned, move on to the next stage.
                    if (rooms.Count >= _settings.rooms)
                    {
                        m_stage = Stage.ROOM_SEPARATION;
                        return;
                    }
                    break;

                case Stage.ROOM_SEPARATION:
                    //  Iterate through all the rooms, and nudge them away from overlapping rooms.
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        m_separator.SteerRoomAway(rooms[i], rooms);
                    }

                    //  If absolutely none of the rooms still overlap, move on to the next stage.
                    if (!m_separator.OverlapsRemain(rooms))
                    {
                        for (int i = 0; i < rooms.Count; i++) rooms[i].Snap();
                        m_stage = Stage.ROOM_PRIORITIZATION;
                        return;
                    }
                    break;

                case Stage.ROOM_PRIORITIZATION:
                    m_prioritizer.CacheMainRooms(rooms, _settings.averageSizeMulitplier);
                    m_stage = Stage.ROOM_LINKING;
                    break;

                case Stage.ROOM_LINKING:
                    m_linker.CreateDelenautor(m_prioritizer.mainRooms);
                    m_stage = Stage.ROOM_SPANNING;
                    m_spanner.Setup(m_prioritizer.mainRooms[0]);
                    break;


                case Stage.ROOM_SPANNING:
                    if (!m_spanner.IterateSpanningTree(m_prioritizer.mainRooms)) break;
                    break;
            }
        }

        public void Draw(Color _color)
        {
            m_spawner.Draw(_color);
            m_prioritizer.Draw(_color);
            m_linker.Draw(_color);
            m_spanner.Draw(Color.green);
        }

        private enum Stage
        {
            ROOM_SPAWNING,
            ROOM_SEPARATION,
            ROOM_PRIORITIZATION,
            ROOM_LINKING,
            ROOM_SPANNING,
        }
    }
}
