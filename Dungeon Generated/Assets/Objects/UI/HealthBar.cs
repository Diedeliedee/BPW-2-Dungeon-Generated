using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Color m_pointFull      = Color.white;
    [SerializeField] private Color m_pointDepleted  = Color.gray;

    private Image[] m_points;

    private void Awake()
    {
        m_points = GetComponentsInChildren<Image>();
    }

    public void OnPlayerDamage(int _health, int _maxHealth)
    {
        for (int i = 0; i < m_points.Length; i++)
        {
            m_points[i].color = i + 1 > _health ? m_pointDepleted : m_pointFull;
        }
    }
}
