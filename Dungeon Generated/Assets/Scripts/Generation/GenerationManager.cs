using DungeonGeneration;
using Joeri.Tools;
using Joeri.Tools.Debugging;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenerationManager : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private GenerationSettings m_settings = default;
    [Space]
    [SerializeField] private float m_iterationTime  = 0.1f;

    [Header("Events:")]
    [SerializeField] private UnityEvent m_onStart;
    [SerializeField] private UnityEvent<float> m_duringGeneration;
    [SerializeField] private UnityEvent m_onFinish = null;

    [Header("Reference:")]
    [SerializeField] private Transform m_mainRoomParent;
    [SerializeField] private Transform m_liminalRoomParent;

    [Header("Debug:")]
    [SerializeField] private Color m_miscColor;

    private Timer m_iterationTimer          = null;
    private DungeonGenerator m_generator    = null;

    private void Awake()
    {
        m_iterationTimer    = new(m_iterationTime);
        m_generator         = new();
    }

    private void Start()
    {
        m_generator.Setup(m_settings, m_mainRoomParent, m_liminalRoomParent);
        m_onStart.Invoke();
    }

    public bool Iterate(out Dictionary<Vector2Int, Tile> _composite)
    {
        _composite = null;

        m_duringGeneration.Invoke(m_generator.percent);

        if (!m_iterationTimer.ResetOnReach(Time.deltaTime)) return false;
        if (!m_generator.Iterate( out _composite))          return false;

        m_onFinish.Invoke();
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            m_generator.Draw(m_miscColor);
        }
        else
        {
            GizmoTools.DrawOutlinedBox(Vector3.zero, new Vector3(m_settings.minRoomWidth, m_settings.minRoomHeight), m_miscColor, m_miscColor.a, true, 0.5f);
            GizmoTools.DrawOutlinedBox(Vector3.zero, new Vector3(m_settings.maxRoomWidth, m_settings.maxRoomHeight), m_miscColor, m_miscColor.a, true, 0.5f);
        }
    }
}
