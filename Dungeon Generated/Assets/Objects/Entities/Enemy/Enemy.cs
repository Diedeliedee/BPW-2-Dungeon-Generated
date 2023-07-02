using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public class Enemy : Entity
{
    public override void OnStartTurn(Action onFinish)
    {
        base.OnStartTurn(onFinish);
        Routines.WaitForSeconds(1f, onFinish);  //  Debug: Wait some time.

        //  Decide what moves to make.
    }

    public override void EndTurn()
    {
        base.EndTurn();
        //  No idea what to put here.
    }
}
