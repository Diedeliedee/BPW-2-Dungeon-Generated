using DungeonGeneration;
using Joeri.Tools;
using UnityEngine;
using UnityEngine.Events;

public class GenerationManager : MonoBehaviour
{
    [SerializeField] private GenerationSettings settings = default;
    [Space]
    [SerializeField] private Color m_roomColor      = Color.white;
    [SerializeField] private float m_iterationTime  = 0.1f;
    [Space]
    [SerializeField] private UnityEvent m_onFinish = null;

    private Timer m_iterationTimer          = null;
    private DungeonGenerator m_generator    = null;

    private void Awake()
    {
        m_iterationTimer = new(m_iterationTime);
        m_generator = new();
    }

    private void Update()
    {
        if (!m_iterationTimer.ResetOnReach(Time.deltaTime)) return;
        m_generator.Iterate(settings);
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            m_generator.Draw(m_roomColor);
        }
        else
        {
            Room.Draw(m_roomColor, settings.minRoomWidth, settings.minRoomHeight);
            Room.Draw(m_roomColor, settings.maxRoomWidth, settings.maxRoomHeight);
        }
    }
}
