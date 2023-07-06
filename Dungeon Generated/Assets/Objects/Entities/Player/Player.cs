using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public override void Setup()
    {
        base.Setup();
    }

    public override void OnStartTurn(Action onFinish)
    {
        base.OnStartTurn(onFinish);

        //  Enable buttons and stuff.
    }

    public override void EndTurn()
    {
        base.EndTurn();
        //  Disable buttons and stuff.
    }
}
