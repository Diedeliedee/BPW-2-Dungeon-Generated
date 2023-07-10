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
        Debug.Log($"New turn started! It's {character}'s turn!", character);

        //  Preparing variables.
        m_activeCharacter       = character;
        m_turnRequirements      = new TurnRequirements(controlCallback);

        //  Calling prepare event.
        m_activeCharacter.OnStartTurn(m_turnRequirements);
        GameManager.instance.events.onTurnStart?.Invoke(character);
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
        private bool m_hasMoved     = false;
        private bool m_hasAttacked  = false;

        //  Accesors:
        public bool hasMoved
        {
            get => m_hasMoved;
            set
            {
                m_hasMoved = value;
                CheckForFinishTurn();
            }
        }

        public bool hasAttacked
        {
            get => m_hasAttacked;
            set
            {
                m_hasAttacked = value;
                CheckForFinishTurn();
            }
        }

        public TurnRequirements(Action controlCallback)
        {
            this.controlCallback = controlCallback;
        }

        public void CheckForFinishTurn()
        {
            if (!m_hasMoved)    return;
            if (!m_hasAttacked) return;

            CompleteTurn();
        }

        private void CompleteTurn()
        {
            controlCallback?.Invoke();
        }
    }
}
