using Joeri.Tools.Utilities;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour, ITurnReceiver, ITileOccupier
{
    [Header("Properties:")]
    [SerializeField] protected int m_maxHealth          = 3;
    [SerializeField] protected int m_movementPerTurn    = 10;

    [Header("Events:")]
    [SerializeField] protected UnityEvent m_onMove;
    [SerializeField] protected UnityEvent<int, int> m_onDamage;
    [SerializeField] protected UnityEvent m_onDeath;

    //  Run-time:
    protected int m_currentMovement = 10;
    protected int m_currentHealth   = 3;

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

    public bool Move(Vector2Int _direction, out MovementCallBack _callback)
    {
        var movementRequest = new MovementRequest
        {
            requestingEntity    = this,
            originTile          = coordinates,
            targetTile          = coordinates + _direction
        };

        if (!m_dungeon.RequestMoveTo(movementRequest, out _callback)) return false;

        m_currentMovement--;
        m_onMove.Invoke();
        return true;
    }

    public void Snap()
    {
        //  This looks stupid but it works wonderful.
        coordinates = coordinates;
    }

    public void Damage(int _damage)
    {
        Debug.Log("Ouch");

        m_currentHealth -= _damage;

        if (m_currentHealth < 0) m_currentHealth = 0;
        m_onDamage.Invoke(m_currentHealth, m_maxHealth);
        if (m_currentHealth == 0) m_onDeath.Invoke();
    }
}
