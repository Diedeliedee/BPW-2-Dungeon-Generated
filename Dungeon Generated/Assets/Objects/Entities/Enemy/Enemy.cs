using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public class Enemy : Character
{
    public override void OnStartTurn(TurnHandler.TurnRequirements turnReq)
    {
        void OnWaitFinish()
        {
            //  Debug:
            EndTurn();
        }

        base.OnStartTurn(turnReq);
        Routines.WaitForSeconds(1f, OnWaitFinish);  //  Debug: Wait some time.

        //  Decide what moves to make.
    }

    public override void EndTurn()
    {
        base.EndTurn();
        //  No idea what to put here.
    }
}
