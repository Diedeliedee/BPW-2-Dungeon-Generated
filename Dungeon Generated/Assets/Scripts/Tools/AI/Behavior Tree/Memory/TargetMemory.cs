using UnityEngine;

public class TargetMemory
{
    private bool m_targetSet = false;
    private Vector3 m_target = default;

    public bool targetSet => m_targetSet;
    public Vector3 target => m_target;

    public void SetTarget(Vector3 _target)
    {
        m_targetSet = true;
        m_target = _target;
    }

    public void Reset()
    {
        m_targetSet = false;
        m_target = default;
    }
}
