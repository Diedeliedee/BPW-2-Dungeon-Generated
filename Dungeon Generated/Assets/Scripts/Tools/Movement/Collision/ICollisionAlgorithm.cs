using UnityEngine;

namespace Joeri.Tools.Movement.TwoDee
{
    public interface ICollisionAlgorithm
    {
        public Vector2 RunCollisionCheck(Vector2 _position, Vector2 _velocity);
    }
}