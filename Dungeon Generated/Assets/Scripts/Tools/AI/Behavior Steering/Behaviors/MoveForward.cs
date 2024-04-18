using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools.AI.Steering
{
    public class MoveForward : Behavior
    {
        private readonly float m_speed = 0f;
        private readonly Transform m_transform = null;

        public MoveForward(float _speed, Transform _transform)
        {
            m_speed = _speed;
            m_transform = _transform;
        }

        public override Vector3 GetDesiredVelocity(Context context)
        {
            return m_transform.forward * m_speed;
        }
    }
}