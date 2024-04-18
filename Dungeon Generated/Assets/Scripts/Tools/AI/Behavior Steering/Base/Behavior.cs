using UnityEngine;

namespace Joeri.Tools.AI.Steering
{
    public abstract partial class Behavior : IBehavior
    {
        public abstract Vector3 GetDesiredVelocity(Context context);

        /// <summary>
        /// Draws optional gizmos the behavior has to offer.
        /// </summary>
        public virtual void DrawGizmos(Vector3 position) { }
    }
}
