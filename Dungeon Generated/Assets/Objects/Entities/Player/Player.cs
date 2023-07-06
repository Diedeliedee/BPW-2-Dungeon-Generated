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

    public override void OnStartTurn(TurnHandler.TurnRequirements turnReq)
    {
        base.OnStartTurn(turnReq);
        //  Enable buttons and stuff.
    }

    public override void EndTurn()
    {
        //  Disable buttons and stuff.
        base.EndTurn();
    }
}
