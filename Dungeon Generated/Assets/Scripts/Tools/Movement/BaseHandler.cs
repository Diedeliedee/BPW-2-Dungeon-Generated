using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools.Movement
{
    public abstract class BaseHandler
    {
        //  Guaranteed Component:
        protected Accel.Uncontrolled m_vertical = new Accel.Uncontrolled();

        //  Reference:
        protected LayerMask m_mask;

        //  Base Properties:
        public float speed { get; set; }
        public float grip { get; set; }
        public float gravity
        {
            get => m_vertical.acceleration;
            set => m_vertical.acceleration = value;
        }

        //  Misc Properties:
        public float verticalVelocity
        {
            get => m_vertical.velocity;
            set => m_vertical.velocity = value;
        }

        public BaseHandler(Settings settings)
        {
            speed   = settings.baseSpeed;
            grip    = settings.baseGrip;
            gravity = settings.baseGravity;

            m_mask  = settings.environmentMask;
        }

        public abstract class Settings
        {
            [Min(0f)]   public float baseSpeed;
            [Min(0f)]   public float baseGrip;
                        public float baseGravity;
            [Space]
                        public LayerMask environmentMask;
        }
    }
}
