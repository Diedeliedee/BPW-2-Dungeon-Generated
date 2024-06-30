using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Compositor
    {
        public Dictionary<Vector2Int, Tile> GetDungeonComposite(IEnumerable<ICompositePart> _compositeParts)
        {
            var composite = new Dictionary<Vector2Int, Tile>();

            foreach (var part in _compositeParts)
            {
                for (int x = 0; x < part.width; x++)
                {
                    for (int y = 0; y < part.height; y++)
                    {
                        var coordinates = new Vector2Int(part.xPos + x, part.yPos + y);

                        if (composite.ContainsKey(coordinates)) continue;
                        composite.Add(coordinates, new(coordinates));
                    }
                }
            }
            return composite;
        }
    }
}