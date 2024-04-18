using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools;

namespace Joeri.Tools.Movement.TwoDee
{
    public class TwoDeeMovement : BaseHandler
    {
        protected Accel.Mono m_horizontal = new Accel.Mono();

        //  Reference:
        protected Transform m_transform = null;

        //  Properties:
        public Vector2 velocity
        {
            get => new Vector2(m_horizontal.velocity, m_vertical.velocity);
            set
            {
                m_horizontal.velocity   = value.x;
                m_vertical.velocity     = value.y;
            }
        }
        public float horizontalVelocity
        {
            get => m_horizontal.velocity;
            set => m_horizontal.velocity = value;
        }

        public TwoDeeMovement(Transform transform, Settings settings) : base(settings)
        {
            m_transform = transform;
        }

        public void ApplyInput(float input, float deltaTime)
        {
            m_horizontal    .CalculateVelocity(input, speed, grip, deltaTime);
            m_vertical      .CalculateVelocity(deltaTime);

            m_transform.position += new Vector3(velocity.x, velocity.y, 0f) * deltaTime;
        }

        public void ApplyIteration(float drag = 0f, float deltaTime = 1f)
        {
            m_horizontal    .CalculateVelocity(drag, deltaTime);
            m_vertical      .CalculateVelocity(deltaTime);

            m_transform.position += new Vector3(velocity.x, velocity.y, 0f) * deltaTime;
        }

        [System.Serializable]
        public new class Settings : BaseHandler.Settings
        {

        }
    }
}
