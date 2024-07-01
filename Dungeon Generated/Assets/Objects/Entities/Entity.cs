using Joeri.Tools.Utilities;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour, ITurnReceiver, ITileOccupier
{
    [Header("Properties:")]
    [SerializeField] protected int m_maxHealth          = 3;
    [SerializeField] protected int m_movementPerTurn    = 10;
    [SerializeField] protected int m_actionsPerTurn     = 1;
    [Space]
    [SerializeField] protected int m_damage = 1;

    [Header("Events:")]
    [SerializeField] protected UnityEvent m_onTurnStart;
    [Space]
    [SerializeField] protected UnityEvent<int> m_onMove;
    [SerializeField] protected UnityEvent<int> m_onPerformAction;
    [Space]
    [SerializeField] protected UnityEvent<int, int> m_onDamage;
    [SerializeField] protected UnityEvent m_onDeath;
    [Space]
    [SerializeField] protected UnityEvent m_onTurnEnd;

    //  Run-time:
    [HideInInspector] public int currentMovement = 10;
    [HideInInspector] public int currentHealth   = 3;
    [HideInInspector] public int currentActions  = 1;

    //  Events:
    protected EventWrapper m_endTurnCallback = new();

    //  Reference:
    protected DungeonManager m_dungeon;

    // Properties:
    public Vector2Int coordinates
    {
        get => new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        set => transform.position = new Vector3(value.x, value.y, transform.position.z);
    }

    public EventWrapper endTurnCallback => m_endTurnCallback;

    protected virtual void Awake()
    {
        m_dungeon = FindObjectOfType<DungeonManager>();
    }

    public virtual void OnTurnStart()
    {
        Debug.Log($"{gameObject.name}'s Turn!!!");

        currentMovement = m_movementPerTurn;
        currentActions  = m_actionsPerTurn;

        m_onTurnStart.Invoke();
    }

    public abstract void DuringTurn();

    public bool Move(Vector2Int _direction, out MovementCallBack _callback)
    {
        //  Immediately disallow movement if no stamina is left.
        if (currentMovement == 0)
        {
            _callback = new MovementCallBack
            {
                condition = MovementCallBack.Condition.OUT_OF_MOVEMENT
            };
            return false;
        }

        //  Create a new movement request for the dungeon manager.
        var movementRequest = new MovementRequest
        {
            requestingEntity    = this,
            originTile          = coordinates,
            targetTile          = coordinates + _direction
        };

        //  Request the dungeon manager to move us.
        if (!m_dungeon.RequestMoveTo(movementRequest, out _callback)) return false;

        //  If it succeeds, take neccesary precautions.
        currentMovement--;
        m_onMove.Invoke(currentMovement);
        return true;
    }

    public void Move(Vector2Int _direction)
    {
        Move(_direction, out MovementCallBack _callback);
    }

    public void Attack(Vector2Int _coordinates)
    {
        m_dungeon.AttackAt(_coordinates, m_damage);

        currentActions--;
        m_onPerformAction.Invoke(currentActions);
    }

    public virtual void EndTurn()
    {
        m_endTurnCallback.Invoke();
        m_onTurnEnd.Invoke();
    }

    public void Snap()
    {
        //  This looks stupid but it works wonderful.
        coordinates = coordinates;
    }

    public abstract void Damage(int _damage);
}
