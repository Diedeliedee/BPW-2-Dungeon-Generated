using UnityEngine;
using System.Collections.Generic;
using Joeri.Tools.Utilities;

namespace DungeonGeneration
{
    public class DungeonGenerator
    {
        private List<Room> m_rooms      = new();
        private List<Room> m_mainRooms  = null;
        private Stage m_stage           = default;

        private Separator m_separator       = new();
        private Prioritizer m_prioritizer   = new();

        public void Iterate(GenerationSettings _settings)
        {
            switch (m_stage)
            {
                case Stage.ROOM_SPAWNING:
                    var position    = Util.RandomCirclePoint() * _settings.circleRadius;
                    var width       = Random.Range(_settings.minRoomWidth, _settings.maxRoomWidth);
                    var height      = Random.Range(_settings.minRoomHeight, _settings.maxRoomHeight);

                    m_rooms.Add(new Room(width, height, position));

                    if (m_rooms.Count >= _settings.rooms)
                    {
                        m_stage             = Stage.ROOM_SEPARATION;
                        m_separator.rooms   = m_rooms;
                        return;
                    }
                    break;

                case Stage.ROOM_SEPARATION:
                    for (int i = 0; i < m_rooms.Count; i++)
                    {
                        m_separator.SteerRoomAway(m_rooms[i]);
                    }

                    //  If absolutely none of the rooms still overlap, move on to the next stage.
                    if (!m_separator.OverlapsRemain())
                    {
                        m_stage = Stage.ROOM_PRIORITIZATION;
                        return;
                    }
                    break;


                case Stage.ROOM_PRIORITIZATION:
                    m_mainRooms = m_prioritizer.GetMainRooms(m_rooms, _settings.averageSizeMulitplier);
                    break;
            }
        }

        public void Draw(Color _color)
        {
            for (int i = 0; i < m_rooms.Count; i++)
            {
                m_rooms[i].Draw(_color);
            }

            if (m_mainRooms == null) return;
            for (int i = 0; i < m_mainRooms.Count; i++)
            {
                m_mainRooms[i].Draw(Color.red);
            }
        }

        private enum Stage
        {
            ROOM_SPAWNING,
            ROOM_SEPARATION,
            ROOM_PRIORITIZATION,
            ROOM_LINKING,
        }
    }
}
