using DungeonGeneration;
using Joeri.Tools.Debugging;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomTester : MonoBehaviour
{
    [SerializeField] private int m_width    = 4;
    [SerializeField] private int m_height   = 4;
    [Space]
    [SerializeField] private Color m_color          = Color.white;
    [SerializeField] private Color m_detectionColor = Color.red;

    private Room m_room                 = null;
    private List<RoomTester> m_rooms    = null;

    private bool m_overlapping = false;

    public Room room => m_room;

    private void Awake()
    {
        m_room      = new Room(m_width, m_height, transform.position);
        m_rooms     = FindObjectsOfType<RoomTester>().ToList();

        //  Remove self-reference.
        for (int i = 0; i < m_rooms.Count; i++)
        {
            if (m_rooms[i].GetInstanceID() != GetInstanceID()) continue;
            m_rooms.RemoveAt(i);
        }
    }

    private void Update()
    {
        m_room.center   = transform.position;
        m_room.width    = m_width;
        m_room.height   = m_height;

        //  Check for overlap.
        for (int i = 0; i < m_rooms.Count; i++)
        {
            if (m_room.OverlapsWith(m_rooms[i].room)){
                m_overlapping = true;
                return;
            }
        }
        m_overlapping = false;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            m_room.Draw(m_overlapping ? m_detectionColor : m_color);
            GizmoTools.DrawLabel(new Vector3(m_room.xPos, m_room.yPos, 0f), $"[{m_room.xPos}, {m_room.yPos}]", m_color, 0.75f);
        }
        else Room.Draw(m_color, m_width, m_height);
    }
}