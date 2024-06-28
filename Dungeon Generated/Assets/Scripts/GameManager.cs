using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent m_onGameStart;

    private GenerationManager m_generation;
    private DungeonManager m_dungeon;

    private State m_state = default;

    private void Awake()
    {
        m_generation    = GetComponentInChildren<GenerationManager>();
        m_dungeon       = GetComponentInChildren<DungeonManager>();
    }

    private void Update()
    {
        switch (m_state)
        {
            case State.GENERATING:
                if (m_generation.Iterate(out Dictionary<Vector2Int, Tile> composite))
                {
                    m_dungeon.CreateFromComposite(composite);
                    m_onGameStart.Invoke();
                    m_state = State.RUNNING;
                }
                break;

            case State.RUNNING:

                break;
        }
    }

    public enum State
    {
        GENERATING  = 0,
        RUNNING     = 1,
    }
}
