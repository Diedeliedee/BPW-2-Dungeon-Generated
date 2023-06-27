using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public List<Room> rooms;

    [SerializeField] private DungeonGenerator m_generator;

    private void Start()
    {
        rooms = m_generator.GetRawRooms();
    }

    private void Update()
    {
        m_generator.Iterate(rooms);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Draw();
        }
        else
        {
            Draw(m_generator.GetRawRooms());
        }
    }

    public void Draw()
    {
        if (rooms == null) return;
        for (int i = 0; i < rooms.Count; i++) rooms[i].Draw();
    }

    public static void Draw(List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count; i++) rooms[i].Draw();
    }
}
