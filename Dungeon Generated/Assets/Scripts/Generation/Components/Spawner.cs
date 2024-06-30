using UnityEngine;
using Joeri.Tools.Utilities;

namespace DungeonGeneration
{
    public class Spawner
    {
        private int m_roomsSpawned = 0;

        public bool IterateSpawningRooms(GenerationSettings _settings, Transform _parent)
        {
            //  Configuring random room conditions.
            var position    = Util.RandomCirclePoint() * _settings.circleRadius;
            var width       = Random.Range(_settings.minRoomWidth, _settings.maxRoomWidth);
            var height      = Random.Range(_settings.minRoomHeight, _settings.maxRoomHeight);

            //  Creating the game object.
            var empty   = new GameObject("Random Liminal Room", typeof(Room));
            var room    = empty.GetComponent<Room>();

            //  Configuring the game object.
            empty.transform.parent = _parent;
            room.Setup(width, height, position);

            //  State management.
            m_roomsSpawned++;
            if (m_roomsSpawned == _settings.rooms)  return true;
                                                    return false;
        }
    }
}
