using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools
{
    public class PathfindTest : MonoBehaviour
    {
        [Header("Properties:")]
        [SerializeField] private int m_gridExtents = 1;
        [SerializeField] private Vector2Int[] m_allowedDirections = new Vector2Int[]
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };
        [Space]
        [SerializeField] [Range(0f, 1f)] private float m_pathLerp = 0f;

        [Header("References:")]
        [SerializeField] private Transform m_start;
        [SerializeField] private Transform m_goal;
        [Space]
        [SerializeField] private GameObject m_lerpTest;

        //  Run-time;
        private Pathfinder m_pathFinder     = null;
        private bool[,] m_validPositions    = null;

        private Pathfinder.Result m_result  = null;

        private void Start()
        {
            m_validPositions    = GetPositions();
            m_pathFinder        = new Pathfinder(IsValidCoordinate, m_allowedDirections);
            m_result            = m_pathFinder.GetPathResult(GetCoordinate(m_start.position), GetCoordinate(m_goal.position));

            m_lerpTest.SetActive(true);
            m_pathLerp = 0f;
        }

        private void Update()
        {
            if (m_result != null && m_result.path != null)
            {
                m_lerpTest.transform.position = GetWorldPosition(m_result.path.Lerp(m_pathLerp));
            }
        }

        private bool[,] GetPositions(System.Action<Vector2Int, bool> onEvaluate = null)
        {
            var positions = new bool[m_gridExtents * 2, m_gridExtents * 2];

            for (int x = 0; x < positions.GetLength(0); x++)
            {
                for (int y = 0; y < positions.GetLength(1); y++)
                {
                    var pos         = GetWorldPosition(new Vector2Int(x, y));
                    var size        = Vector3.one;
                    var occupied    = Physics.CheckBox(pos, size * 0.5f);

                    positions[x, y] = !occupied;
                    onEvaluate?.Invoke(new Vector2Int(x, y), occupied);
                }
            }
            return positions;
        }
        private bool IsValidCoordinate(Vector2Int coordinate)
        {
            var gridSize    = m_gridExtents * 2;
            var inXBounds   = coordinate.x >= 0 && coordinate.x < gridSize;
            var inYBounds   = coordinate.y >= 0 && coordinate.y < gridSize;

            if (!inXBounds || !inYBounds) return false;
            return m_validPositions[coordinate.x, coordinate.y];
        }

        private Vector3 GetWorldPosition(Vector2 coordinates)
        {
            var localPosition   = new Vector3(coordinates.x - m_gridExtents + 0.5f, 0f, coordinates.y - m_gridExtents + 0.5f);
            var worldPosition   = transform.position + localPosition;

            return worldPosition;
        }

        private Vector2Int GetCoordinate(Vector3 worldPos)
        {
            var localPosition   = worldPos - transform.position;
            var localCoordinate = new Vector2Int(Mathf.FloorToInt(localPosition.x) + m_gridExtents, Mathf.FloorToInt(localPosition.z) + m_gridExtents);

            return localCoordinate;
        }

        private Color GetNodeColor(Vector2Int coordinate, bool occupied)
        {
            if (m_start != null && GetCoordinate(m_start.position) == coordinate)   return Color.yellow;
            if (m_goal != null && GetCoordinate(m_goal.position) == coordinate)     return Color.yellow;
            if (m_result != null)
            {
                if (m_result.path != null && m_result.path.Has(coordinate))         return new Color(0f, 0.5f, 1f);
                if (m_result.openNodes.Contains(coordinate))                        return Color.green;
                if (m_result.closedNodes.Contains(coordinate))                      return Color.red;
            }
            if (!occupied)                                                          return Color.white;
                                                                                    return Color.black;
        }

        private void OnDrawGizmos()
        {
            void OnEvaluate(Vector2Int coordinates, bool occupied)
            {
                GizmoTools.DrawOutlinedBox(GetWorldPosition(coordinates), Vector3.one * 0.9f, GetNodeColor(coordinates, occupied), 0.5f, true, 0.5f);
            }

            GetPositions(OnEvaluate);
        }
    }
}
