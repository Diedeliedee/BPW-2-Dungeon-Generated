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
    public ControlManager   control;

    //  Sub-managers:
    private EventManager m_eventManager = null;

    public EventManager events { get => m_eventManager; }

    private void Awake()
    {
        instance        = this;
        m_eventManager  = new EventManager();
    }

    private void Start()
    {
        dungeon     .Setup();
        entities    .Setup();
        control     .Setup();
        camera      .Setup();

        StartGame();
    }

    private void StartGame()
    {
        control.StartControlLoop();
    }

    private void PrepareForTurn(Entity entity, System.Action onFinish)
    {
        camera.MoveTo(entity.coordinates, onFinish);
    }
}
