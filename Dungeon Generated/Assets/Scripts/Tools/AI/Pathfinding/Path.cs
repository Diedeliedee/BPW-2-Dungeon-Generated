using UnityEngine;

/*
namespace Joeri.Tools.AI.Pathfinding
{
    public class Path
    {
        public readonly Vector2Int[] coordinates = null;
    
        private Tools.Path m_path;
    
        public float length     { get => m_path.length; }
        public Vector2Int first { get => coordinates[0]; }
        public Vector2Int last  { get => coordinates[^1]; }
    
        public Path(params Vector2Int[] coordinatesBundle)
        {
            m_path      = new Tools.Path(CoordsToPosBundle(coordinatesBundle));
            coordinates = coordinatesBundle;
        }
    
        public Tools.Path.Slice GetSlice(int index)
        {
            return m_path[index];
        }
    
        public bool Has(Vector2Int coordinates)
        {
            for (int i = 0; i < m_path.positions.Length; i++)
            {
                var position = m_path.positions[i];
    
                if (new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y)) == coordinates) return true;
            }
            return false;
        }
    
        public Vector2 Lerp(float t)
        {
            return m_path.Lerp(t); ;
        }
    
        private Vector3[] CoordsToPosBundle(Vector2Int[] coordsBundle)
        {
            var positions = new Vector3[coordsBundle.Length];
    
            for (int i = 0; i < coordsBundle.Length; i++)
            {
                positions[i] = new Vector3(coordsBundle[i].x, coordsBundle[i].y);
            }
            return positions;
        }
    }
}
*/