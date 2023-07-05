using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public class GameManager : Singleton<GameManager>
{
    [Header("References:")]
    public Dungeon          dungeon;
    public EntityManager    entities;
    public CameraManager    camera;

    //  State machine:
    private FSM m_stateMachine          = null;

    //  Sub-managers:
    private EventManager m_eventManager = null;
    private TurnManager m_turnManager   = null;

    private void Awake()
    {
        instance        = this;
        m_stateMachine  = new FSM(typeof(Idle), new Idle(), new FreeRoam(this), new Combat(this));
        m_eventManager  = new EventManager();
    }

    private void Start()
    {
        dungeon     .Setup();
        entities    .Setup();
        camera      .Setup();

        StartGame();
    }

    private void StartGame()
    {
        m_turnManager   = new TurnManager(entities.player);
        m_stateMachine  .SwitchToState(typeof(FreeRoam));
    }

    private void PrepareForTurn(Entity entity, System.Action onFinish)
    {
        camera.MoveTo(entity.coordinates, onFinish);
    }

    #region Game States
    public class Idle : State { }

    public class FreeRoam : FlexState<GameManager>
    {
        private bool m_spotted = false;

        public FreeRoam(GameManager root) : base(root) { }

        public override void OnEnter()
        {
            void StartRecursiveTurn()
            {
                //  Switch to combat if the player has been spotted in the last turn.
                if (m_spotted) { SwitchToState(typeof(Combat)); return; }

                //  Otherwise, it's the player's turn again.
                root.m_turnManager.StartNextTurn(root.PrepareForTurn, StartRecursiveTurn);
            }

            Debug.Log("Started game. Entered freeroam.");

            //  Subscribe to the spotted function, and start a turn loop of the player.
            root.m_eventManager.onPlayerSpotted += OnPlayerSpotted;
            StartRecursiveTurn();
        }

        private void OnPlayerSpotted(Enemy enemy)
        {
            m_spotted           = true;
            root.m_turnManager  .Enqueue(enemy);
        }

        public override void OnExit()
        {
            root.m_eventManager.onPlayerSpotted -= OnPlayerSpotted;
            m_spotted                           = false;
        }
    }

    public class Combat : FlexState<GameManager>
    {
        private bool m_enemiesDefeated = false;

        public Combat(GameManager root) : base(root) { }

        public override void OnEnter()
        {
            void StartRecursiveCombatTurnLoop()
            {
                //  If all enemies have been defeated, switch back to freeroam.
                if (m_enemiesDefeated) { SwitchToState(typeof(FreeRoam)); return; }

                //  Otherwise, start the next entity's turn.
                root.m_turnManager.StartNextTurn(root.PrepareForTurn, StartRecursiveCombatTurnLoop);
            }

            Debug.Log("Combat ensued!!!.");

            //  Subscribe to the relevant events, and start the combat loop.
            root.m_eventManager.onPlayerSpotted += OnEnemyEncountered;
            root.m_eventManager.onEnemyDespawn  += OnEnemyDespawned;
            StartRecursiveCombatTurnLoop();
        }

        private void OnEnemyEncountered(Enemy enemy)
        {
            root.m_turnManager.Enqueue(enemy);
        }

        private void OnEnemyDespawned(Enemy enemy)
        {
            root.m_turnManager.Remove(enemy);
            if (root.m_turnManager.enemyCount <= 0) m_enemiesDefeated = true;
        }

        public override void OnExit()
        {
            root.m_eventManager.onPlayerSpotted -= OnEnemyEncountered;
            root.m_eventManager.onEnemyDespawn  += OnEnemyDespawned;
            m_enemiesDefeated                   = false;
        }
    }
    #endregion
}
