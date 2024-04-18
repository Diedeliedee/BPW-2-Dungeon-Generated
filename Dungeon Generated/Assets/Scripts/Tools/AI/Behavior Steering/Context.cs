using UnityEngine;

namespace Joeri.Tools.AI.Steering
{
    /// <summary>
    /// Struct used for passing down information within the behaviors.
    /// </summary>
    public struct Context
    {
        public readonly float deltaTime;
        public readonly float maxSpeed;
        public readonly Vector3 position;
        public readonly Vector3 velocity;

        public Context(float _deltaTime, float _maxSpeed, Vector3 _position, Vector3 _velocity)
        {
            this.deltaTime = _deltaTime;
            this.maxSpeed = _maxSpeed;
            this.position = _position;
            this.velocity = _velocity;
        }
    }
}