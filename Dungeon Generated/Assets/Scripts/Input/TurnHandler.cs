using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TurnHandler
{
    private Character m_activeCharacter         = null;
    private TurnRequirements m_turnRequirements = null;

    public void StartTurn(Character character, Action controlCallback)
    {
        void CallCharacterEvent()
        {
            //  Calling turn event.
            m_activeCharacter.OnStartTurn(m_turnRequirements);
        }

        Debug.Log($"New turn started! It's {character}'s turn!", character);

        //  Preparing variables.
        m_activeCharacter       = character;
        m_turnRequirements      = new TurnRequirements(controlCallback);

        //  Calling prepare event.
        GameManager.instance.events.onTurnPrepare?.Invoke(character, CallCharacterEvent);
    }

    public void FinishCurrentTurn()
    {
        m_activeCharacter.EndTurn();

        m_activeCharacter   = null;
        m_turnRequirements  = null;
    }

    public class TurnRequirements
    {
        //  Events:
        public Action controlCallback    = null;

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

        public TurnRequirements(Action controlCallback)
        {
            this.controlCallback = controlCallback;
        }

        public void CheckForFinishTurn()
        {
            if (!m_hasMoved) return;

            CompleteTurn();
        }

        private void CompleteTurn()
        {
            controlCallback?.Invoke();
        }
    }
}
