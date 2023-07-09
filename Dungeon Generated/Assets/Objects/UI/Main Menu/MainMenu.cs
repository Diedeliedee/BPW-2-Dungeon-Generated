using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UIScreen
{
    public override void Activate()
    {
        gameObject.SetActive(true);
        m_animator.Play("Start-up");
    }

    public void OnProgramStart()
    {
        //  Pause the game in the game manager.
        GameManager.instance.Pause();
    }

    public void OnGameStart()
    {
        //  Unpause the game.
        GameManager.instance.UnPause();
        GameManager.instance.StartGame();
    }

    public void OnQuitGame()
    {
        GameManager.instance.QuitGame();
    }
}
