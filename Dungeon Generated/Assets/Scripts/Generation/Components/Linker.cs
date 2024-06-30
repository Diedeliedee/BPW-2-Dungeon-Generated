using DelaunatorSharp;
using System.Collections.Generic;

namespace DungeonGeneration
{
    public class Linker
    {
        private Delaunator m_delaunator = null;

        public void CreateDelenautor(List<MainRoom> _rooms)
        {
            //  Constructing an array of Delaunator points filled with rooms.
            var points = new IPoint[_rooms.Count];
            for (int i = 0; i < _rooms.Count; i++)
            {
                points[i] = _rooms[i];
            }
            
            //  Creating a delenautor from these rooms.
            m_delaunator = new(points);

            //  Connect all the rooms with each other.
            foreach (var triangle in m_delaunator.GetTriangles())
            {
                var rooms = new List<MainRoom>();

                //  Converting casting every point in the triangle back into a room.
                foreach (var point in triangle.Points) rooms.Add((MainRoom)point);

                //  Every room in the triangle is connected with two opposing rooms.
                //  Since a room link is one-sided, linking needs to be done from both ways.
                rooms[0].LinkRoom(rooms[1]);
                rooms[0].LinkRoom(rooms[2]);

                rooms[1].LinkRoom(rooms[0]);
                rooms[1].LinkRoom(rooms[2]);

                rooms[2].LinkRoom(rooms[0]);
                rooms[2].LinkRoom(rooms[1]);
            }
        }
    }
}