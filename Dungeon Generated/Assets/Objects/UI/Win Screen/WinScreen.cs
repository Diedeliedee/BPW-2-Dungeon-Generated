using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : UIScreen
{
    public override void Activate()
    {
        gameObject.SetActive(true);
        m_animator.Play("Start");
    }

    public void Quit()
    {
        GameManager.instance.QuitGame();
    }
}
