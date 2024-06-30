using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class MovementIndication : MonoBehaviour
{
    [SerializeField] private int m_startMovement = 10;
    [Space]
    [SerializeField] private Color m_fullColor = Color.white;
    [SerializeField] private Color m_depletedColor = Color.gray;

    private TextMeshProUGUI m_textMesh;

    private void Awake()
    {
        m_textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void OnPlayerMove(int _movement)
    {
        m_textMesh.color    = _movement > 0 ? m_fullColor : m_depletedColor;
        m_textMesh.text     = _movement.ToString();
    }

    public void Replenish()
    {
        m_textMesh.color    = m_fullColor;
        m_textMesh.text     = m_startMovement.ToString();
    }
}
