using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public abstract class Entity : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private float m_speed;

    //  Run-time:
    protected Tile m_currentTile = null;
    protected Vector2Int m_coordinates;

    protected Coroutine m_activeRoutine             = null;
    protected TurnRequirements m_turnRequirements   = null;

    //  Core Events:
    public System.Action onTurnEnd = null;

    //  UI Events:
    public System.Action<Vector2> onMouseClick   = null;
    public System.Action<Vector2> onMouseDrag                   = null;
    public System.Action<Vector2> onMouseRelease                = null;

    #region Properties
    public Vector2Int coordinates
    {
        get => m_coordinates;
        set
        {
            var pos = Dungeon.CoordsToPos(value);

            m_coordinates       = value;
            transform.position  = new Vector3(pos.x, value.y + 0.5f, transform.position.z);
        }
    }
    public Vector2 position
    {
        get => transform.position;
        set
        {
            m_coordinates       = Dungeon.PosToCoords(value);
            transform.position  = new Vector3(value.x, value.y, transform.position.z);
        }
    }

    public bool activeTurn { get => m_turnRequirements != null; }
    #endregion

    public virtual void Setup()
    {
        //  Snap the entity to the grid by moving it to the current tile.
        Move(Vector2Int.zero);
    }

    public virtual void OnStartTurn(System.Action onFinish)
    {
        m_turnRequirements  = new TurnRequirements(EndTurn);
        onTurnEnd           = onFinish;
    }

    public virtual void EndTurn()
    {
        onTurnEnd?          .Invoke();
        onTurnEnd           = null;
        m_turnRequirements  = null;
    }

    public void MoveTo(LockedVector movement)
    {
        var time        = movement.value / m_speed;
        var destination = coordinates + movement.ToVector();

        var start       = position;
        var end         = Dungeon.CoordsToPos(destination);

        void OnTick(float progress)
        {
            position = Vector2.Lerp(start, end, progress);
        }

        void OnFinish()
        {
            coordinates                 = destination;
            m_activeRoutine             = null;
            m_turnRequirements.hasMoved = true;
        }

        m_activeRoutine = StartCoroutine(Routines.Progression(time, OnTick, OnFinish));
    }
    
    public void Move(Vector2Int direction)
    {
        if (!GameManager.instance.dungeon.HasTile(coordinates + direction, out Tile tile)) return;

        coordinates     += direction;
        m_currentTile   = tile;
    }

    protected class TurnRequirements
    {
        //  Event:
        public System.Action onTurnComplete = null;

        //  Requirements:
        private bool m_hasMoved = false;

        //  Accesors:
        public bool hasMoved
        {
            get => m_hasMoved;
            set
            {
                m_hasMoved = true;
                CheckForFinishTurn();
            }
        }

        public TurnRequirements(System.Action onTurnFinish)
        {
            onTurnComplete += onTurnFinish;
        }

        public void CheckForFinishTurn()
        {
            if (!m_hasMoved) return;

            CompleteTurn();
        }

        public void SkipTurn()
        {
            CompleteTurn();
        }

        private void CompleteTurn()
        {
            onTurnComplete?.Invoke();

            m_hasMoved = false;
        }
    }
}
