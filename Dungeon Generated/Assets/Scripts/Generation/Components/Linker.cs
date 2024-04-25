using DelaunatorSharp;
using Joeri.Tools.Debugging;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGeneration
{
    public class Linker
    {
        private Delaunator m_delenautor = null;

        public Delaunator delenautor = null;

        public void CreateDelenautor(List<Room> _rooms)
        {
            var points = new IPoint[_rooms.Count];

            for (int i = 0; i < _rooms.Count; i++)
            {
                points[i] = new Point(_rooms[i].center.x, _rooms[i].center.y);
            }
            m_delenautor = new(points);
        }

        public void Draw(Color _color)
        {
            if (m_delenautor == null) return;

            var edges = m_delenautor.GetEdges();

            foreach (var edge in edges)
            {
                var point1 = new Vector2((float)edge.P.X, (float)edge.P.Y);
                var point2 = new Vector2((float)edge.Q.X, (float)edge.Q.Y);

                GizmoTools.DrawLine(point1, point2, _color, 0.75f);
            }
        }
    }
}