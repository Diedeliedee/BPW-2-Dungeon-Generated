using UnityEngine;

namespace Joeri.Tools.Utilities
{
    public static class VectorExtensions
    {
        #region Vector2
        /// <summary>
        /// Converts the 2D vector to an angle in 0-360 degree value.
        /// </summary>
        public static float ToAngle(this Vector2 _direction)
        {
            var signedAngle = Vector2.SignedAngle(_direction, Vector2.up);

            if (signedAngle < 0) signedAngle += 360f;
            return signedAngle;
        }

        /// <summary>
        /// Converts the vector into a new direction vector based on the passed in 0-360 degree value.
        /// </summary>
        public static Vector2 ToDirection(this float _degrees)
        {
            //  The degrees are converted to radians, but for the sake of memory, are kept in the variable called 'degrees'.
            _degrees *= Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(_degrees), Mathf.Sin(_degrees));
        }

        /// <summary>
        /// Rotates the passed in direction vector with the passed in amount of degrees.
        /// </summary>
        public static Vector2 Rotate(this Vector2 _vector, float _degrees)
        {
            var radians = _degrees * Mathf.Deg2Rad;
            var newVector = Vector2.zero;

            newVector.x = _vector.x * Mathf.Cos(radians) - _vector.y * Mathf.Sin(radians);
            newVector.y = _vector.x * Mathf.Sin(radians) + _vector.y * Mathf.Cos(radians);
            return newVector;
        }

        /// <summary>
        /// Rotates the called from direction vector with the passed in amount of degrees.
        /// </summary>
        public static void ApplyRotation(ref this Vector2 _vector, float _degrees)
        {
            _vector = _vector.Rotate(_degrees);
        }

        /// <summary>
        /// Converts the passed in 3D vector into a 2D vector with X remaining as X, and Z moved to Y.
        /// </summary>
        public static Vector2 Planar(this Vector3 _vector)
        {
            return new Vector2(_vector.x, _vector.z);
        }
        #endregion

        #region Vector3
        /// /// <summary>
        /// Converts the passed in 2D vector into a 3D vector applied to X, and Z
        /// </summary>
        public static Vector3 Cubular(this Vector2 _vector, float _height = 0f)
        {
            return new Vector3(_vector.x, _height, _vector.y);
        }

        /// <summary>
        /// Converts the vector into a new direction vector pointing to the passed in vector, multiplied by the multiplier.
        /// </summary>
        public static Vector2 ToDirection(this Vector2 _from, Vector2 _to, float _multiplier = 1f)
        {
            return (_to - _from).normalized * _multiplier;
        }
        #endregion
    }
}