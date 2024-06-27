using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Compositor
    {
        public Dictionary<Vector2Int, Tile> GetDungeonComposite(List<Room> rooms, List<Room> corridors)
        {
            var composite   = new Dictionary<Vector2Int, Tile>();
            var all         = rooms;

            all.AddRange(corridors);
            foreach (var room in all)
            {
                for (int x = 0; x < room.width; x++)
                {
                    for (int y = 0; y < room.height; y++)
                    {
                        var coordinates = new Vector2Int(room.xPos + x, room.yPos + y);

                        if (composite.ContainsKey(coordinates)) continue;
                        composite.Add(coordinates, new(coordinates));
                    }
                }
            }
            return composite;
        }
    }
}