using UnityEngine;
using System.Collections.Generic;
using Joeri.Tools.Debugging;

namespace DungeonGeneration
{
    public class Spanner
    {
        public Dictionary<int, Room> m_visited = new();

        public void Setup(Room _room)
        {
            m_visited.Add(_room.GetHashCode(), _room);
        }

        public bool IterateSpanningTree(List<Room> _rooms)
        {
            if (m_visited.Count == _rooms.Count)
            {
                return true;
            }

            var connection = GetShortestConnectionAvailable();

            connection.start.CreatePath(connection.end);
            connection.end.CreatePath(connection.start);

            m_visited.Add(connection.end.GetHashCode(), connection.end);
            return false;

        }

        /// <returns>The closest connection to an </returns>
        private Room.Connection GetShortestConnectionAvailable()
        {
            Room.Connection shortestConnection = null;

            foreach (var room in m_visited.Values)
            {
                foreach (var connection in room.connections)
                {
                    //  If the connection leads to a room that is already visited, continue.
                    if (m_visited.ContainsKey(connection.end.GetHashCode())) continue;

                    //  If no shortest connection has yet been found, immediately assign this connection.
                    if (shortestConnection == null)
                    {
                        shortestConnection = connection;
                        continue;
                    }

                    //  If there is a shortest connection, but seems to be longer than this one, override it.
                    if (shortestConnection.length > connection.length)
                    {
                        shortestConnection = connection;
                    }
                }
            }

            return shortestConnection;
        }

        public void Draw(Color _color)
        {
            foreach (var room in m_visited.Values)
            {
                //  Draw every visited room in the given color.
                room.Draw(_color);

                //  Also draw evey connection as a live in the given color.
                foreach (var connection in room.mainPath)
                {
                    GizmoTools.DrawLine(room.center, connection.end.center, _color, 1f);
                }
            }
        }
    }

}