using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float m_timeScale = 1f;

    private void Update()
    {
        if (Time.timeScale == m_timeScale) return;
        Time.timeScale = m_timeScale;
    }
}
