using UnityEngine;

namespace Joeri.Tools.AI.Steering
{
    public interface IBehavior
    {
        /// <returns>The desired velocity of the behavior within the current frame.</returns>
        public abstract Vector3 GetDesiredVelocity(Context context);
    }
}