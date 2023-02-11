using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Dodelie.Tools;

public partial class Player
{
    [Header("Movement:")]
    [SerializeField] private CharacterController m_controller = null;

    //  Run-time:
    private Vector2 m_currentVelocity = Vector2.zero;
    private Vector2 m_currentSteering = Vector2.zero;

    private float m_currentRotationSpeed = 0f;

    //  Constants:
    private const float m_epsilon = 0.05f;

    #region Properties

    public Vector2 velocity { get => m_currentVelocity; }
    public Collider collider { get => m_controller; }

    #endregion

    public void IterateMovement(float movementSpeed, float rotationSpeed, float movementGrip, float rotationGrip, float deltaTime)
    {
        var input = Vector2Int.zero;

        if (Input.GetKey(KeyCode.A)) input.x--;
        if (Input.GetKey(KeyCode.D)) input.x++;
        if (Input.GetKey(KeyCode.W)) input.y++;
        if (Input.GetKey(KeyCode.S)) input.y--;

        Move(input.y, movementSpeed, movementGrip, deltaTime);
        Rotate(input.x, rotationSpeed, rotationGrip, deltaTime);
    }

    /// <summary>
    /// Iteratively relocates the player.
    /// </summary>
    /// <param name="direction">Whether the player should move forward, backward, or not at all.</param>
    /// <param name="speed">The speed of the movement in metres per second.</param>
    private void Move(int direction, float speed, float grip, float deltaTime)
    {
        var desiredVelocity = Vector2.ClampMagnitude(Calc.VectorToFlat(transform.forward) * direction, 1f) * speed;

        //  Calculating steering.
        var currentSteering = desiredVelocity - m_currentVelocity;
            currentSteering *= grip * deltaTime;

        //  Calculating velocity.
        m_currentVelocity += currentSteering;

        //  Halting movement if slowing down, and below epsilon.
        var desiredLength = desiredVelocity.sqrMagnitude;
        var currentLength = m_currentVelocity.sqrMagnitude;

        if (desiredLength < currentLength && currentLength < m_epsilon)
        {
            m_currentVelocity = Vector2.zero;
            return;
        }

        //  Applying velocity.
        m_controller.Move(Calc.FlatToVector(m_currentVelocity * deltaTime));
    }

    /// <summary>
    /// Iteratively rotates the player.
    /// </summary>
    /// <param name="direction">Whether the player should rotate left, right, or not at all.</param>
    /// <param name="speed">The roation speed in degrees per second.</param>
    private void Rotate(int direction, float speed, float grip, float deltaTime)
    {
        var eulers = transform.eulerAngles;
        var desiredRotationSpeed = direction * speed;

        //  Calculating steering.
        var currentSteering = desiredRotationSpeed - m_currentRotationSpeed;
            currentSteering *= grip * deltaTime;

        //  Calculating velocity.
        m_currentRotationSpeed += currentSteering;

        //  Halting movement if slowing down, and below epsilon.
        var desiredLength = Mathf.Abs(desiredRotationSpeed);
        var currentLength = Mathf.Abs(m_currentRotationSpeed);

        if (desiredLength < currentLength && currentLength < m_epsilon)
        {
            m_currentRotationSpeed = 0f;
            return;
        }

        //  Applying velocity.
        eulers.y += m_currentRotationSpeed * deltaTime;
        transform.rotation = Quaternion.Euler(eulers);
    }
}