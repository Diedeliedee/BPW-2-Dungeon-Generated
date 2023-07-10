using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class PlayerControl
{
    [System.Serializable]
    public class SelectAttack : Option
    {
        [SerializeField] private AttackMarker m_marker;

        private Attack m_selectedAttack             = null;
        private Vector2Int m_selectedCoordinates    = Vector2Int.zero;

        public void Setup()
        {
            m_marker.Setup();
        }

        public override void Activate(Action<Option> onStart)
        {
            base.Activate(onStart);
            GameManager.instance.events.onAttackSelected += OnAttackSelected;
        }

        public override void Deactivate()
        {
            base.Deactivate();

            GameManager.instance.events.onAttackSelected -= OnAttackSelected;
            Cancel();
        }

        private void OnAttackSelected(Attack attack)
        {
            Prioritize();

            m_selectedCoordinates   = GetSelectedCoordinates(Input.mousePosition);
            m_selectedAttack        = attack;

            m_marker.Activate(attack, Dungeon.CoordsToPos(m_selectedCoordinates));
        }

        public override void Cancel()
        {
            base.Cancel();

            m_selectedAttack        = null;
            m_selectedCoordinates   = Vector2Int.zero;

            m_marker.Deactivate();
        }

        public override void Tick()
        {
            //  Updating marker position:
            var coordinates = GetSelectedCoordinates(Input.mousePosition);

            if (coordinates != m_selectedCoordinates)
            {
                m_selectedCoordinates = coordinates;
                OnTileChange(coordinates);
            }

            if (Input.GetMouseButtonDown(0))
            {
                GameManager.instance.entities.player.PerformAttack(m_selectedAttack, m_selectedCoordinates, null);
            }
        }

        private void OnTileChange(Vector2Int coordinates)
        {
            m_marker.transform.position = Dungeon.CoordsToPos(coordinates);
        }
    }
}
