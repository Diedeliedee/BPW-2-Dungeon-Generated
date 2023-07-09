using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Joeri.Tools.Structure;

public class GameManager : Singleton<GameManager>
{
    [Header("References:")]
    public Dungeon          dungeon;
    public EntityManager    entities;
    public CameraManager    camera;
    public ControlManager   control;
    public UIManager        ui;

    //  Sub-managers:
    private EventManager m_eventManager = null;

    public EventManager events { get => m_eventManager; }

    private void Awake()
    {
        instance = this;
        m_eventManager = new EventManager();
    }

    private void Start()
    {
        dungeon     .Setup();
        entities    .Setup();
        control     .Setup();
        camera      .Setup();
        ui          .Setup();
    }

    public void StartGame()
    {
        control.StartControlLoop();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
    }

    public void OnPlayerDeath()
    {
        Pause();
        ui.gameOver.Activate();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
