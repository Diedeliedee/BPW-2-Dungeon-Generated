using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MainMenu     mainMenu;
    public GameOver     gameOver;
    public WinScreen    winScreen;

    public void Setup()
    {
        mainMenu    .Setup();
        gameOver    .Setup();
        winScreen   .Setup();

        mainMenu.Activate();
    }
}
