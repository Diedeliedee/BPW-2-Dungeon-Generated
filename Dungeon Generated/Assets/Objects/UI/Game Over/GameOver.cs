using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : UIScreen
{
    private System.Action m_choice = null;

    public override void Activate()
    {
        gameObject.SetActive(true);
        m_animator.Play("Start");
    }

    public void OnChoseRetry()
    {
        void Retry()
        {
            GameManager.instance.RestartGame();
        }

        m_choice = Retry;
    }

    public void OnChoseQuit()
    {
        void Quit()
        {
            GameManager.instance.QuitGame();
        }

        m_choice = Quit;
    }

    public void FireChoice()
    {
        m_choice.Invoke();
    }
}
