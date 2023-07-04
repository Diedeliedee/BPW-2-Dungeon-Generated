using UnityEngine;

namespace Joeri.Tools.Utilities
{
    public static class Vectors
    {
        /// <returns>The passed in 2D direction vector rotated with the passed in amount of degrees.</returns>
        public static Vector2 RotateVector2(Vector2 vector, float degrees)
        {
            var radians = degrees * Mathf.Deg2Rad;
            var newVector = Vector2.zero;

            newVector.x = vector.x * Mathf.Cos(radians) - vector.y * Mathf.Sin(radians);
            newVector.y = vector.x * Mathf.Sin(radians) + vector.y * Mathf.Cos(radians);
            return newVector;
        }

        /// <returns>The passed in 3D vector turned into a 2D vecotr with X, and Z.</returns>
        public static Vector2 VectorToFlat(Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }

        /// <returns>The passed in flat 2D vector, turned into a 3D vector applied by X, and Z.</returns>
        public static Vector3 FlatToVector(Vector2 vector, float height = 0f)
        {
            return new Vector3(vector.x, height, vector.y);
        }

        public static Vector2 ToDirection(Vector2 from, Vector2 to, float multiplier = 1f)
        {
            return (to - from).normalized * multiplier;
        }

        /// <returns>A "random" point in a 3D sphere, defined by the radius.</returns>
        public static Vector3 RandomSpherePoint(float radius = 1f)
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * radius;
        }

        /// <returns>A random point in a 2D circle, defined by the radius.</returns>
        public static Vector2 RandomCirclePoint(float radius = 1f)
        {
            var r = radius * Mathf.Sqrt(Random.Range(0f, 1f));
            var whateverthetameans = Random.Range(0f, 1f) * 2 * Mathf.PI;

            return new Vector2(r * Mathf.Cos(whateverthetameans), r * Mathf.Sin(whateverthetameans));
        }

        /// <returns>The angle between a 2D direction vector, and an upward direction, with a range of 0-360 degrees.</returns>
        public static float VectorToAngle(Vector2 direction)
        {
            var signedAngle = Vector2.SignedAngle(direction, Vector2.up);

            if (signedAngle < 0) signedAngle += 360f;
            return signedAngle;
        }

        /// <returns>A 2D direction vector based on the passed in degrees.</returns>
        public static Vector2 AngleToVector(float degrees)
        {
            //  The degrees are converted to radians, but for the sake of memory, are kept in the variable called 'degrees'.
            degrees *= Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(degrees), Mathf.Sin(degrees));
        }

        
    }
}
