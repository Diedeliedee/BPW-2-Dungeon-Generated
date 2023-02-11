using UnityEngine;
using Dodelie.Tools;

[CreateAssetMenu(fileName = "Walking", menuName = "States/Player/Walking", order = 0)]
public class Walking : State<Player>
{
    [SerializeField] private float m_walkSpeed = 3f;
    [SerializeField] private float m_walkGrip = 1f;
    [SerializeField] private float m_rotationSpeed = 1f;
    [SerializeField] private float m_rotationGrip = 1f;

    public override void OnTick(float deltaTime)
    {
        root.IterateMovement(m_walkSpeed, m_rotationSpeed, m_walkGrip, m_rotationGrip, deltaTime);

        if (!Input.GetKeyDown(KeyCode.LeftShift)) return;
        SwitchToState<Running>();
    }

}