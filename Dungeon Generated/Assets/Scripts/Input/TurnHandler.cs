using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TurnHandler
{
    private TurnRequirements m_turnRequirements = null;
    private Action m_inputModuleCallback        = null;

    public void StartTurn(Character character, Action onFinish)
    {
        void CallCharacterEvent()
        {
            //  Calling turn event.
            character.OnStartTurn(m_turnRequirements);
        }

        Debug.Log($"New turn started! It's {character}'s turn!", character);

        //  Preparing variables.
        m_turnRequirements      = new TurnRequirements(OnTurnFinish);
        m_inputModuleCallback   = onFinish;

        //  Calling prepare event.
        GameManager.instance.events.onTurnPrepare?.Invoke(character, CallCharacterEvent);
    }

    private void OnTurnFinish()
    {
        //  Juggling memory.
        var callBack = m_inputModuleCallback;

        //  Resetting variables.
        m_turnRequirements      = null;
        m_inputModuleCallback   = null;

        //  Invoke callback to Input Module.
        callBack.Invoke();
    }

    public class TurnRequirements
    {
        //  Events:
        public Action onTurnComplete    = null;
        public Action onTurnFinish      = null;

        //  Requirements:
        private bool m_hasMoved = false;

        //  Accesors:
        public bool hasMoved
        {
            get => m_hasMoved;
            set
            {
                m_hasMoved = true;
                CheckForFinishTurn();
            }
        }

        public TurnRequirements(Action onTurnFinish)
        {
            this.onTurnFinish = onTurnFinish;
        }

        public void CheckForFinishTurn()
        {
            if (!m_hasMoved) return;

            CompleteTurn();
        }

        private void CompleteTurn()
        {
            onTurnComplete?.Invoke();
            onTurnFinish.Invoke();
        }
    }
}
