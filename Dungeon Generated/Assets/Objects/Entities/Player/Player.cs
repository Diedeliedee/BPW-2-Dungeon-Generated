using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
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

    private void OnMouseDown()  { if (activeTurn) onMouseClick?.Invoke(Input.mousePosition);    }
    private void OnMouseDrag()  { if (activeTurn) onMouseDrag?.Invoke(Input.mousePosition);     }
    private void OnMouseUp()    { if (activeTurn) onMouseRelease?.Invoke(Input.mousePosition);  }
}
