using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private PlayerInput m_input;

    public override void Setup()
    {
        base    .Setup();
        m_input .Setup(this);
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

    private void OnMouseDown()  { if (activeTurn) m_input.OnClick(Input.mousePosition);     }
    private void OnMouseDrag()  { if (activeTurn) m_input.OnDrag(Input.mousePosition);      }
    private void OnMouseUp()    { if (activeTurn) m_input.OnRelease(Input.mousePosition);   }
}
