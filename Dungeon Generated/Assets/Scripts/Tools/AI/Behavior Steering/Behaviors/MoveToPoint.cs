using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools.AI.Steering
{
    public class MoveToPoint : Behavior
    {
        public Vector3 target = Vector3.zero;
        public float speed = 0f;

        public MoveToPoint(Vector3 _target, float _speed)
        {
            target = _target;
            speed = _speed;
        }

        public override Vector3 GetDesiredVelocity(Context context)
        {
            return (target - context.position).normalized * speed;
        }
    }
}
