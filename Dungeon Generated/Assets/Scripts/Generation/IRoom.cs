using UnityEngine;

namespace DungeonGeneration
{
    public interface IRoom
    {
        public int width    { get; set; }
        public int height   { get; set; }

        public int xPos { get; set; }
        public int yPos { get; set; }

        public Vector2 center   { get; set; }
        
        public Vector2 topLeft      { get; }
        public Vector2 bottomRight  { get; }

        public bool OverlapsWith(IRoom _other);
    }
}
