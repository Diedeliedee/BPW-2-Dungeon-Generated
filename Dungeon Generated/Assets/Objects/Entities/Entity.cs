using Joeri.Tools.Utilities;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour, ITurnReceiver, ITileOccupier
{
    [Header("Properties:")]
    [SerializeField] protected int m_movementPerTurn = 10;

    [Header("Events:")]
    [SerializeField] protected UnityEvent m_onMove;

    //  Run-time:
    protected int m_currentMovement = 10;

    //  Events:
    protected EventWrapper m_onTurnEnd = new();

    //  Reference:
    protected DungeonManager m_dungeon;

    // Properties:
    public Vector2Int coordinates
    {
        get => new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        set => transform.position = new Vector3(value.x, value.y, transform.position.z);
    }

    public EventWrapper onTurnEnd => m_onTurnEnd;

    protected virtual void Awake()
    {
            m_dungeon = GetComponentInParent<DungeonManager>();
    }

    public abstract void OnTurnStart();

    public abstract void DuringTurn();

    public bool Move(Vector2Int _direction)
    {
        if (!m_dungeon.RequestMoveTo(this, coordinates + _direction)) return false;

        m_currentMovement--;
        m_onMove.Invoke();
        return true;
    }

    protected Vector2Int ProcessDesiredInput(Vector2 _direction)
    {
        var processedDirection  = Vector2Int.zero;

        if      (_direction.x > 0.5f)   processedDirection = Vector2Int.right;
        else if (_direction.x < -0.5f)  processedDirection = Vector2Int.left;
        else if (_direction.y > 0.5f)   processedDirection = Vector2Int.up;
        else if (_direction.y < -0.5f)  processedDirection = Vector2Int.down;
        return processedDirection;
    }

    public void Snap()
    {
        //  This looks stupid but it works wonderful.
        coordinates = coordinates;
    }

    public void Damage(int _damage)
    {
        Debug.Log("Ouch!");
    }
}
