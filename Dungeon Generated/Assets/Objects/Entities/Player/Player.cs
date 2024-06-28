using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int m_movementPerTurn = 10;

    [Header("Events")]
    [SerializeField] private UnityEvent m_onMove;

    private int m_currentMovement = 10;

    private DungeonManager m_dungeon;
    private InputReader m_input;

    private void Awake()
    {
        m_dungeon   = GetComponentInParent<DungeonManager>(); 
        m_input     = FindObjectOfType<InputReader>();
    }

    private void Update()
    {
        if (m_input.movementPressed)
        {
            var direction           = m_input.movementDirection;
            var processedDirection  = Vector2Int.zero;

            if      (direction.x > 0.5f)    processedDirection = Vector2Int.right;
            else if (direction.x < -0.5f)   processedDirection = Vector2Int.left;
            else if (direction.y > 0.5f)    processedDirection = Vector2Int.up;
            else if (direction.y < -0.5f)   processedDirection = Vector2Int.down;

            Move(processedDirection);
        }
    }

    public void Move(Vector2Int _direction)
    {
        if (m_currentMovement <= 0) return;
        transform.position += (Vector3)(Vector2)_direction; //  What the hell is this.
        m_currentMovement--;
        m_onMove.Invoke();
    }
}
