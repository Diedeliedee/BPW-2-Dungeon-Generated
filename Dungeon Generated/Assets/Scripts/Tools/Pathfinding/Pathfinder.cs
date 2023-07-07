using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Joeri.Tools.Utilities;

namespace Joeri.Tools.Pathfinding
{
    /// <summary>
    /// Simple A* pathfinder
    /// </summary>
    public class Pathfinder
    {
        private readonly ValidNodeCheck m_nodeCheck     = null;
        private readonly Vector2Int[] m_validDirections = null;

        public Pathfinder(ValidNodeCheck nodeCheck, Vector2Int[] validDirections)
        {
            m_nodeCheck         = nodeCheck;
            m_validDirections   = validDirections;
        }

        public Path FindPath(Vector2Int start, Vector2Int goal)
        {
            return GetPathResult(start, goal).path;
        }

        public Result GetPathResult(Vector2Int start, Vector2Int goal)
        {
            var openNodes   = new Dictionary<Vector2Int, Node>();
            var closedNodes = new Dictionary<Vector2Int, Node>();

            var currentNode = new Node(start, goal);

            /// Adds the passed in node to the open Nodes dictionary.
            void AddNodeToOpen(Node node)
            {
                openNodes.Add(node.coordinates, node);
            }

            /// Checks if the passed in coordinates have a Node in the open dictionary, and switches the Node to the closed dictionary.
            void SwitchNodeToClosed(Node node)
            {
                openNodes   .Remove(node.coordinates);
                closedNodes .Add(node.coordinates, node);
            }

            /// Returns the Node with the lowest f-cost in the open Nodes dictionary.
            Node FindLowestFCost()
            {
                Node lowest = openNodes.First().Value;

                foreach (var node in openNodes)
                {
                    var lowerFCost = node.Value.fCost < lowest.fCost;
                    var lowerHCost = node.Value.fCost == lowest.fCost && node.Value.hCost < lowest.hCost;

                    if (!lowerFCost && !lowerHCost) continue;
                    lowest = node.Value;
                }
                return lowest;
            }

            /// Returns a dictionary of all the neighboring coordinates of the current Node.
            List<Vector2Int> GetNeighboringCoordinates()
            {
                var coordinates = new List<Vector2Int>();

                for (int i = 0; i < m_validDirections.Length; i++)
                {
                    coordinates.Add(currentNode.coordinates + m_validDirections[i]);
                }
                return coordinates;
            }

            /// Returns a Path from the start to the current node.
            Path RetraceNodes()
            {
                var coordinatesBundle   = new List<Vector2Int>();
                var crumb               = currentNode;

                while (true)
                {
                    coordinatesBundle.Add(crumb.coordinates);
                    crumb = crumb.parent;
                    if (crumb.coordinates == start)
                    {
                        coordinatesBundle.Add(crumb.coordinates);
                        break;
                    }
                }
                coordinatesBundle.Reverse();

                return new Path(coordinatesBundle.ToArray());
            }

            AddNodeToOpen(currentNode);
            while (true)
            {
                //  Exit the loop if there is no open path to the goal.
                if (openNodes.Count <= 0)
                {
                    Debug.Log($"Pathfinding failed with zero remaining open nodes. {closedNodes.Count} nodes have been closed in the process.");
                    return new Result(null, openNodes, closedNodes);
                }

                //  Find the Node with the lowest f-cost, and switch it to the closed Nodes dictionary.
                currentNode = FindLowestFCost();
                SwitchNodeToClosed(currentNode);

                //  Return out of the loop if we reached the goal.
                if (currentNode.coordinates == goal)
                {
                    return new Result(RetraceNodes(), openNodes, closedNodes);
                }

                //  Check every valid neighbor in the surroundings of the current node, and add the qualified nodes to the open dictionary.
                foreach (var coordinates in GetNeighboringCoordinates())
                {
                    //  If the neighbor's coordinates cannot bear a valid node, or already bears a node in the closed dictionary, skip the iteration.
                    if (!IsValidNode(coordinates) || closedNodes.ContainsKey(coordinates)) continue;

                    var newNeighbor = new Node(coordinates, goal, currentNode);

                    //  If a node already exists in with the neighboring coordinates..
                    if (openNodes.TryGetValue(coordinates, out Node neighboringNode))
                    {
                        //  Skip if the path through the newly created node is longer than the existing neighboring node.
                        if (newNeighbor.gCost >= neighboringNode.gCost) continue;

                        //  Otherwise, remove the existing node first, and..
                        openNodes.Remove(coordinates);
                    }

                    //  Add the newly created node in the open dictionary.
                    AddNodeToOpen(newNeighbor);
                }
            }
        }

        /// <returns>True if the passed in coordinates could harbor a valid Node. False if not.</returns>
        private bool IsValidNode(Vector2Int coordinates)
        {
            return m_nodeCheck(coordinates);
        }

        public class Result
        {
            public readonly Path path                       = null;
            public readonly List<Vector2Int> openNodes      = null;
            public readonly List<Vector2Int> closedNodes    = null;

            public Result(Path path, Dictionary<Vector2Int, Node> openNodes, Dictionary<Vector2Int, Node> closedNodes)
            {
                this.path           = path;
                this.openNodes      = openNodes.Keys.ToList();
                this.closedNodes    = closedNodes.Keys.ToList();
            }
        }

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

        public class Node
        {
            public readonly Node parent = null;
            public readonly Vector2Int coordinates;

            public readonly int gCost = 0;
            public readonly int hCost = 0;
            public readonly int fCost = 0;

            public Node(Vector2Int coordinates, Vector2Int goal, Node parent = null)
            {
                this.parent         = parent;
                this.coordinates    = coordinates;

                if (parent != null) gCost = parent.gCost + Mathf.RoundToInt((coordinates - parent.coordinates).magnitude * 10);
                else                gCost = 0;

                hCost = Mathf.RoundToInt((goal - coordinates).magnitude * 10);
                fCost = gCost + hCost;
            }
        }
    
        public delegate bool ValidNodeCheck(Vector2Int coordinates);
    }
}