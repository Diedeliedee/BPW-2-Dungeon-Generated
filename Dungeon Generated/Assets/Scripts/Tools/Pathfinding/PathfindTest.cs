using UnityEngine;
using Joeri.Tools.Utilities;
using Joeri.Tools.Debugging;

namespace Joeri.Tools.Pathfinding
{
    public class PathfindTest : MonoBehaviour
    {
        [Header("Properties:")]
        [SerializeField] private bool m_realtime                    = false;
        [SerializeField] private int m_gridExtents                  = 1;
        [SerializeField] private Vector2Int[] m_allowedDirections   = new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        [Space]
        [SerializeField] [Range(0f, 1f)] private float m_pathlerp   = 0f;

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
            m_pathFinder        = new Pathfinder(IsValidCoordinate, m_allowedDirections);

            m_validPositions    = GetPositions();
            m_result            = m_pathFinder.GetPathResult(GetCoordinate(m_start.position), GetCoordinate(m_goal.position));

            m_lerpTest.SetActive(true);
            m_pathlerp = 0f;
        }

        private void Update()
        {
            if (m_realtime)
            {
                m_validPositions    = GetPositions();
                m_result            = m_pathFinder.GetPathResult(GetCoordinate(m_start.position), GetCoordinate(m_goal.position));
            }

            if (m_result != null && m_result.path != null)
            {
                m_lerpTest.transform.position = GetWorldPosition(m_result.path.Lerp(m_pathlerp));
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

        private Vector3 GetWorldPosition(Vector2 flatPos, bool applyOffset = true)
        {
            var offset = applyOffset ? 0.5f : 0f;

            var localPosition   = new Vector3(flatPos.x - m_gridExtents + offset, 0f, flatPos.y - m_gridExtents + offset);
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
            var nodeSize        = Vector3.one * 0.9f;
            var opacity         = 0.5f;
            var solid           = true;
            var solidOpacity    = 0.5f;

            if (Application.isPlaying)
            {
                if (m_result == null || m_validPositions == null) return;
                for (int x = 0; x < m_validPositions.GetLength(0); x++)
                {
                    for (int y = 0; y < m_validPositions.GetLength(1); y++)
                    {
                        var coordinates = new Vector2Int(x, y);
                        var position    = GetWorldPosition(coordinates);

                        GizmoTools.DrawOutlinedBox(position, nodeSize, GetNodeColor(coordinates, !m_validPositions[x, y]), opacity, solid, solidOpacity);
                    }
                }
            }
            else
            {
                void OnEvaluate(Vector2Int coordinates, bool occupied)
                {
                    GizmoTools.DrawOutlinedBox(GetWorldPosition(coordinates), nodeSize, GetNodeColor(coordinates, occupied), opacity, solid, solidOpacity);
                }

                GetPositions(OnEvaluate);
            }
        }
    }
}
