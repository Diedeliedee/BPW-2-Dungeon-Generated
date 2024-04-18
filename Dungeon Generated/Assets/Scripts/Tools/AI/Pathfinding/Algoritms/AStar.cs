using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools.AI.Pathfinding
{
    public class AStar : IAlgorithm
    {
        private readonly NodeEvaluation m_nodeEvaluation    = null;
        private readonly Vector2Int[] m_validDirections     = null;

        public AStar(NodeEvaluation _nodeEvaluation, Vector2Int[] _validDirections)
        {
            m_nodeEvaluation    = _nodeEvaluation;
            m_validDirections   = _validDirections;
        }

        public List<Vector2Int> GetPath(Vector2Int _start, Vector2Int _end)
        {
            var openNodes   = new Dictionary<Vector2Int, Node>();
            var closedNodes = new Dictionary<Vector2Int, Node>();

            var currentNode = new Node(_start, _end);

            AddNodeToOpen(currentNode);
            while (true)
            {
                //  Exit the loop if there is no open path to the goal.
                if (openNodes.Count <= 0)
                {
                    Debug.Log($"Pathfinding failed with zero remaining open nodes. {closedNodes.Count} nodes have been closed in the process.");
                    return null;
                }

                //  Find the Node with the lowest f-cost, and switch it to the closed Nodes dictionary.
                currentNode = FindLowestFCost();
                SwitchNodeToClosed(currentNode);

                //  Return out of the loop if we reached the goal.
                if (currentNode.coordinates == _end) return RetraceNodes();

                //  Check every valid neighbor in the surroundings of the current node, and add the qualified nodes to the open dictionary.
                foreach (var coordinates in GetNeighboringCoordinates())
                {
                    //  If the neighbor's coordinates cannot bear a valid node, or already bears a node in the closed dictionary, skip the iteration.
                    if (!m_nodeEvaluation(currentNode.coordinates, coordinates) || closedNodes.ContainsKey(coordinates)) continue;

                    var newNeighbor = new Node(coordinates, _end, currentNode);

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

            /// Adds the passed in node to the open Nodes dictionary.
            void AddNodeToOpen(Node _node)
            {
                openNodes.Add(_node.coordinates, _node);
            }

            /// Checks if the passed in coordinates have a Node in the open dictionary, and switches the Node to the closed dictionary.
            void SwitchNodeToClosed(Node _node)
            {
                openNodes.Remove(_node.coordinates);
                closedNodes.Add(_node.coordinates, _node);
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
            List<Vector2Int> RetraceNodes()
            {
                var coordinatesBundle   = new List<Vector2Int>(Mathf.Abs(_start.x - _end.x * _start.y - _end.y));
                var crumb               = currentNode;

                while (true)
                {
                    coordinatesBundle.Add(crumb.coordinates);
                    if (crumb.coordinates == _start)
                    {
                        coordinatesBundle.Add(crumb.coordinates);
                        break;
                    }
                    crumb = crumb.parent;
                }
                coordinatesBundle.Reverse();
                return coordinatesBundle;
            }
        }

        public class Node
        {
            public readonly Node parent             = null;
            public readonly Vector2Int coordinates  = default;

            public readonly int gCost   = 0;
            public readonly int hCost   = 0;
            public readonly int fCost   = 0;

            public Node(Vector2Int _coordinates, Vector2Int _goal, Node _parent = null)
            {
                parent      = _parent;
                coordinates = _coordinates;

                if (_parent != null) gCost  = _parent.gCost + Mathf.RoundToInt((_coordinates - _parent.coordinates).magnitude * 10);
                else gCost                  = 0;

                hCost   = Mathf.RoundToInt((_goal - _coordinates).magnitude * 10);
                fCost   = gCost + hCost;
            }
        }

        public delegate bool NodeEvaluation(Vector2Int _from, Vector2Int _to);
    }
}
