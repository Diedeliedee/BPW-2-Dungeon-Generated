using UnityEngine;

namespace Joeri.Tools.Movement
{
    /// <summary>
    /// Class representing a movement behavior.
    /// </summary>
    public abstract class Behavior
    {
        /// <returns>The desired velocity of the behavior within the current frame.</returns>
        public abstract Vector2 GetDesiredVelocity(Context context);

        /// <summary>
        /// Draws optional gizmos the behavior has to offer.
        /// </summary>
        public virtual void DrawGizmos(Vector3 position) { }

        /// <summary>
        /// Struct used for passing down information within the behaviors.
        /// </summary>
        public struct Context
        {
            public readonly float deltaTime;
            public readonly float speed;
            public readonly Vector2 position;
            public readonly Vector2 velocity;

            public Context(float deltaTime, float speed, Vector2 position, Vector2 velocity)
            {
                this.deltaTime  = deltaTime;
                this.speed      = speed;
                this.position   = position;
                this.velocity   = velocity;
            }
        }
    }
}
