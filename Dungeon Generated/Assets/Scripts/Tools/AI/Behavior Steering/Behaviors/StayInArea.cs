using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools.AI.Steering
{
    public class StayInArea : Behavior
    {
        public Vector3 center = Vector3.zero;
        public float sqrRadius = 0f;
        public float opposingForce = 0f;

        public StayInArea(Vector3 _center, float _radius, float _opposingForce)
        {
            center = _center;
            sqrRadius = _radius * _radius;
            opposingForce = _opposingForce;
        }

        public override Vector3 GetDesiredVelocity(Context context)
        {
            var offsetToCenter = center - context.position;

            if (offsetToCenter.sqrMagnitude < sqrRadius) return Vector3.zero;
            return offsetToCenter.normalized * opposingForce;
        }
    }
}
