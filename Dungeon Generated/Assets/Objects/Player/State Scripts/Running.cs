using UnityEngine;
using Dodelie.Tools;

[CreateAssetMenu(fileName = "Running", menuName = "States/Player/Running", order = 0)]
public class Running : State<Player>
{
    [SerializeField] private float m_walkSpeed = 3f;
    [SerializeField] private float m_walkGrip = 1f;
    [SerializeField] private float m_rotationSpeed = 1f;
    [SerializeField] private float m_rotationGrip = 1f;

    public override void OnTick(float deltaTime)
    {
        root.IterateMovement(m_walkSpeed, m_rotationSpeed, m_walkGrip, m_rotationGrip, deltaTime);

        if (!Input.GetKeyUp(KeyCode.LeftShift)) return;
        SwitchToState<Walking>();
    }

}