using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;

namespace Joeri.Tools
{
    public struct Orbit
    {
        private float m_yAngle;
        private float m_xAngle;
        private float m_distance;

        private Vector3 m_direction;
        private Vector3 m_offset;
        private Quaternion m_rotation;

        public float yAngle
        {
            get => m_yAngle;
            set => SetAngles(value, m_xAngle, m_distance);
        }

        public float xAngle
        {
            get => m_xAngle;
            set => SetAngles(m_yAngle, value, m_distance);
        }

        public float distance
        {
            get => m_distance;
            set => SetAngles(m_yAngle, m_xAngle, value);
        }

        public Vector3 direction
        {
            get => m_direction;
            set => SetOffset(value * m_distance);
        }

        public Vector3 offset
        {
            get => m_offset;
            set => SetOffset(value);
        }

        public Quaternion rotation
        {
            get => m_rotation;
        }

        public void SetAngles(float yAngle, float xAngle, float distance)
        {
            var direction = Vector3.forward;

            ///  We apply the Y and X angles to the direction seperately.
            ///  First, the Y rotation needs to be applied, before we know what the X angle to rotate on will be.
            direction = Quaternion.AngleAxis(yAngle, Vector3.up) * direction;                           //  Rotation Horizontally.
            direction = Quaternion.AngleAxis(xAngle, Vector3.Cross(direction, Vector3.up)) * direction; //  Rotation Vertically

            var rotation = Quaternion.Euler(xAngle, yAngle, 0f);

            ApplyChanges(yAngle, xAngle, distance, direction, direction * distance, rotation);
        }

        public void SetOffset(Vector3 offset)
        {
            var flatOffset = new Vector3(offset.x, 0f, offset.z);

            var yAngle = new Vector2(offset.x, offset.z).ToAngle();
            var xAngle = Vector3.SignedAngle(flatOffset, offset, Vector3.Cross(offset, Vector3.up));

            var distance = offset.magnitude;
            var direction = offset.normalized;
            var rotation = Quaternion.Euler(xAngle, yAngle, 0f);

            ApplyChanges(yAngle, xAngle, distance, direction, offset, rotation);
        }

        private void ApplyChanges(float yAngle, float xAngle, float distance, Vector3 direction, Vector3 offset, Quaternion rotation)
        {
            m_yAngle = yAngle;
            m_xAngle = xAngle;
            m_distance = distance;
            m_direction = direction;
            m_offset = direction * distance;
            m_rotation = rotation;
        }
    }
}