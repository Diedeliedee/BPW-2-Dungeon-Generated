using UnityEngine;
using System.Collections.Generic;
using Joeri.Tools.Debugging;

namespace DungeonGeneration
{
    public class Spanner
    {
        public Dictionary<int, MainRoom> m_visited = new();

        public void SelectStartRoom(MainRoom _room)
        {
            _room.visited = true;
            m_visited.Add(_room.GetHashCode(), _room);
        }

        public bool IterateSpanningTree(List<MainRoom> _rooms)
        {
            if (m_visited.Count == _rooms.Count)
            {
                return true;
            }

            var connection = GetShortestConnectionAvailable();

            connection.start.CreatePath(connection.end);

            m_visited.Add(connection.end.GetHashCode(), connection.end);
            return false;
        }

        /// <returns>The closest connection to an </returns>
        private MainRoom.Connection GetShortestConnectionAvailable()
        {
            MainRoom.Connection shortestConnection = null;

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
    }

}