using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionIndication : MonoBehaviour
{
    [SerializeField] private Color m_fullColor = Color.white;
    [SerializeField] private Color m_depletedColor = Color.gray;

    private TextMeshProUGUI m_textMesh;

    private void Awake()
    {
        m_textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void OnPlayerAction(int _actions)
    {
        m_textMesh.color = _actions > 0 ? m_fullColor : m_depletedColor;
    }

    public void Replenish()
    {
        m_textMesh.color = m_fullColor;
    }
}
