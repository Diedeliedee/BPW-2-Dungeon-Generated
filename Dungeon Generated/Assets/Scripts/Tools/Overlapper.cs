using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Debugging;

namespace Joeri.Tools
{
    public class Overlapper<Target>
    {
        private float m_radius = 1f;
        private LayerMask m_mask;

        private bool m_active = false;
        private Dictionary<int, Target> m_caughtTargets = null;

        private event Action<Target> m_onEnter = null;
        private event Action<Target> m_onStay = null;

        #region Properties

        public bool active { get => m_active; }

        public Dictionary<int, Target> caughtTargets { get => m_caughtTargets; }

        #endregion

        public Overlapper(float radius, LayerMask mask)
        {
            m_radius = radius;
            m_mask = mask;
        }

        public void Activate(Action<Target> onEnter, Action<Target> onStay)
        {
            if (m_active)
            {
                Debug.LogWarning("Overlapper is already active. Won't override.");
                return;
            }

            m_onEnter = onEnter;
            m_onStay = onStay;
            m_caughtTargets = new Dictionary<int, Target>();
            m_active = true;
        }

        public void Deactivate()
        {
            if (!m_active)
            {
                Debug.LogWarning("Overlapper is already deactivated. Won't override.");
                return;
            }

            m_caughtTargets.Clear();
            m_caughtTargets = null;
            m_active = false;
        }

        public void Overlap(Vector3 position)
        {
            if (!m_active)
            {
                Debug.LogWarning("Activate the Overlapper before you start checking for colliders!..");
                return;
            }

            var colliders = Physics.OverlapSphere(position, m_radius, m_mask);

            if (colliders.Length <= 0) return;
            for (int i = 0; i < colliders.Length; i++)
            {
                //  Move to next iteration if the caught collider does not have the desired component.
                if (!colliders[i].TryGetComponent(out Target target)) continue;

                //  If the caught collider does have the desired component, and is already present in the dictionary, call OnStay(...).
                if (m_caughtTargets.ContainsKey(target.GetHashCode()))
                {
                    m_onStay?.Invoke(target);
                    continue;
                }

                //  If the caught collider has the desired component, and is not yet present, it has succesfully entered the overlapper.
                m_caughtTargets.Add(target.GetHashCode(), target);
                m_onEnter?.Invoke(target);
                m_onStay?.Invoke(target);
            }
        }

        public void DrawGizmos(Vector3 pos, Color color)
        {
            if (!m_active) return;
            GizmoTools.DrawSphere(pos, m_radius, color, 0.75f, true, 0.5f);
        }
    }
}