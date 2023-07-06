using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Pathfinding;

public partial class PlayerInput : MonoBehaviour
{
    //  Run-time:
    private Dictionary<System.Type, Option> m_options   = null;
    private Option m_activeOption                       = null;

    public void Setup()
    {
        m_options        = GetOptions();

        GameManager.instance.events.onObjectClicked     += OnObjectClick;
        GameManager.instance.events.onObjectDrag        += OnObjectDrag;
        GameManager.instance.events.onObjectReleased    += OnObjectRelease;
    }

    private void OnObjectClick(Object gameObject, Vector2 mousePos)
    {
        m_activeOption = m_options[gameObject.GetType()];
        m_activeOption?.OnClick(mousePos);
    }

    private void OnObjectDrag(Object gameObject, Vector2 mousePos)
    {
        if (m_activeOption == null) return;

        m_activeOption.OnDrag(mousePos);
    }

    private void OnObjectRelease(Object gameObject, Vector2 mousePos)
    {
        if (m_activeOption == null) return;

        m_activeOption?.OnRelease(mousePos);
        m_activeOption = null;
    }
}
