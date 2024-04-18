using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools.AI.Steering
{
    public abstract class FlockRule
    {
        public abstract Vector3 GetContribution(float _deltaTime, Vector3 _position, List<IBoid> _peerBoids);

        public virtual void DrawGizmos(Vector3 _position, List<IBoid> _peerBoids) { }

        protected List<IBoid> FilterToNearbyBoids(Vector3 _position, float _sqrDistance, List<IBoid> _peerBoids)
        {
            var _nearbyBoids = new List<IBoid>(_peerBoids.Count);

            for (int i = 0; i < _peerBoids.Count; i++)
            {
                if ((_peerBoids[i].position - _position).sqrMagnitude > _sqrDistance) continue;
                _nearbyBoids.Add(_peerBoids[i]);
            }
            return _nearbyBoids;
        }
    }
}
