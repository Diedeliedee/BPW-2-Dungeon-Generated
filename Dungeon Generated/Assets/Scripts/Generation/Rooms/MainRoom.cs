using UnityEngine;
using System.Collections.Generic;
using Joeri.Tools.Debugging;

namespace DungeonGeneration
{
    public class MainRoom : Room
    {
        [Space]
        [SerializeField] private Color m_pathColor      = Color.green;

        private Dictionary<int, Connection> m_connections           = new();
        private Dictionary<int, Connection> m_mainPathConnections   = new();

        public IEnumerable<Connection> connections  => m_connections.Values;
        public IEnumerable<Connection> mainPath     => m_mainPathConnections.Values;

        public bool visited { get; set; }

        public void LinkRoom(MainRoom _room)
        {
            //  Cease if the rooms is already in our connections.
            if (m_connections.ContainsKey(_room.GetHashCode()))
            {
                //Debug.Log($"Room: {GetHashCode()} is already linked with room: {_room.GetHashCode()}. Skipping..");
                return;
            }

            //  Construct a new connection class.
            var connection = new Connection
            {
                offset  = _room.center - center,
                length  = Vector2.Distance(center, _room.center),
                start   = this,
                end     = _room
            };

            //  Add it to the list.
            m_connections.Add(_room.GetHashCode(), connection);
        }

        public void CreatePath(MainRoom _room)
        {
            //  Grab the visiting room from our current connections.
            var connection = m_connections[_room.GetHashCode()];

            //  Move it to the main path list.
            m_connections.Remove(_room.GetHashCode());
            m_mainPathConnections.Add(connection.end.GetHashCode(), connection);

            //  Mark the other rooms as visited for gizmo purposes.
            _room.visited = true;
        }

        protected override void OnDrawGizmos()
        {
            if (!visited)
            {
                base.OnDrawGizmos();
            }
            else
            {
                GizmoTools.DrawOutlinedBox(new Vector3(center.x, center.y), new Vector3(width, height), m_pathColor, m_pathColor.a, true, 0.5f);

                foreach (var connection in mainPath)
                {
                    GizmoTools.DrawLine(connection.start.center, connection.end.center, m_pathColor, m_pathColor.a);
                }
            }

            foreach (var connection in connections)
            {
                GizmoTools.DrawLine(connection.start.center, connection.end.center, m_debugColor, m_debugColor.a);
            }
        }

        public class Connection
        {
            public Vector2 offset;
            public float length;
            public MainRoom start;
            public MainRoom end;
        }
    }
}
