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
        base.EndTurn();
        //  Disable buttons and stuff.
    }

    public override void OnDeath()
    {
        base.OnDeath();

        GameManager.instance.events.onPlayerDeath?.Invoke();
    }

    private void OnMouseDown()  => GameManager.instance.events.onPlayerClicked?     .Invoke(Input.mousePosition);
    private void OnMouseDrag()  => GameManager.instance.events.onPlayerDragged?     .Invoke(Input.mousePosition);
    private void OnMouseUp()    => GameManager.instance.events.onPlayerReleased?    .Invoke(Input.mousePosition);
}
