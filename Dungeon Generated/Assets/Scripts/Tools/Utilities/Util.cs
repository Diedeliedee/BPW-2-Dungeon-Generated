using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Utilities
{
    /// <summary>
    /// Static class that holds some simple, but common calculation functions.
    /// </summary>
    public static class Util
    {
        /// <returnsThe 'current' integer, with 1 added to it. It loops back to zero once it reached it's max.></returns>
        public static int ScrollOne(int current, int max)
        {
            current++;
            if (current > max) current = 0;
            return current;
        }

        /// <returns>The 'current' integer, with 'amount' added to it, but loops around with 'max' as it's max value.</returns>
        public static int Scroll(int current, int amount, int max)
        {
            current += amount;
            current %= max;
            return current;
        }

        /// <returns>The passed in 'color', but with the passed in 'opacity'.</returns>
        public static Color SetOpacity(Color color, float opacity)
        {
            return new Color(color.r, color.g, color.b, opacity);
        }

        /// <returns>The passed in 'current' float, but swapped around according to the 'max' value.</returns>
        public static float Reverse(float current, float max)
        {
            current *= -1;
            current /= max;
            current += 1;
            current *= max;
            return current;
        }

        /// <returns>The passed in 0-1 integer, but swapped in value.</returns>
        public static float OneMinus(float current)
        {
            return current * -1 + 1;
        }

        /// <returns>Whether a list of colliders containts the desired component.</returns>
        public static bool Contains<T>(out T[] containingComponents, params Collider[] colliders)
        {
            var componentList = new List<T>();

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out T component)) componentList.Add(component);
            }
            containingComponents = componentList.ToArray();
            return containingComponents.Length > 0;
        }

        /// <returns>True if the passed in array is null, or has nothing in it.</returns>
        public static bool IsUnusableArray<T>(T[] array)
        {
            if (array == null || array.Length == 0) return true;
            return false;
        }

        /// <returns>True if the passed in list is null, or has nothing in it.</returns>
        public static bool IsUnusableList<T>(List<T> list)
        {
            if (list == null || list.Count == 0) return true;
            return false;
        }

        /// <returns>The result of a random number lower than, or equal to the given probability value.</returns>
        public static bool RandomChance(float probability)
        {
            probability = Mathf.Clamp01(probability);
            return (Random.Range(0f, 1f) <= probability);
        }

        /// <returns>A random point in a 2D circle.</returns>
        public static Vector2 RandomCirclePoint()
        {
            var r = Mathf.Sqrt(Random.Range(0f, 1f));
            var t = Random.Range(0f, 1f) * 2 * Mathf.PI;

            return new Vector2(r * Mathf.Cos(t), r * Mathf.Sin(t));
        }

        /// <returns>A random point in a 2D circle without using square root.</returns>
        public static Vector2 RandomCirclePoint(int accuracy)
        {
            var offset = Vector2.zero;

            for (int i = 0; i < accuracy; i++)
            {
                offset = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                if (offset.sqrMagnitude > 1f) continue;
                break;
            }
            return offset;
        }

        /// <returns>A "random" point in a 3D sphere, defined by the radius.</returns>
        public static Vector3 RandomSpherePoint(float radius = 1f)
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * radius;
        }

        /// <returns>The deceleration an object needs in m/s^2, to reach the passed distance, with the passed initial velocity, in the passed timeframe.</returns>
        public static float CalculateDeceleration(float _startVelocity, float _endVelocity, float _time)
        {
            return (_endVelocity - _startVelocity) / _time;
        }

        /// <returns>The initial velocity an object needs, to travel the passed in distance, decelerated by the passed in deceleration.</returns>
        public static float CalculateInitialVelocity(float _distance, float _deceleration)
        {
            return Mathf.Sqrt(2 * _deceleration * _distance);
        }

        /// <returns>Whether one of the numbers is below zero, while the other number is above zero.</returns>
        public static bool PolarOpposite(float _a, float _b)
        {
            if (_a < 0f && _b > 0f) return true;
            if (_b < 0f && _a > 0f) return true;
            return false;
        }

        /// <returns>Whether one of the numbers is either above or below zero, while the other number is also above or below zero.</returns>
        public static bool SamePole(float _a, float _b)
        {
            if (_a < 0f && _b < 0f) return true;
            if (_a > 0f && _b > 0f) return true;
            return false;
        }

        ///<summary>Does not work. :(</summary>
        /// <returns>The velocity an object needs in m/s^2 to a standstill after the passed distance, and passed time, with the calculated deceleration.</returns>
        /*
        public static float CalculateInitialVelocity(float _distance, float _time, out float _deceleration)
        {
            var velocity = (2 * _distance) / _time - (_time / 2);
            _deceleration = (2 * _distance) / (_time * _time) - (velocity * _time) / 2;
            return velocity;
        }
        */

        public static List<T> CastList<T, O>(List<O> _list) where T : O
        {
            var castedList = new List<T>();

            for (int i = 0; i < _list.Count; i++) castedList.Add((T)_list[i]);
            return castedList;
        }

        public static int StretchToInt(float _value)
        {
            var negative    = _value < 0;
            var stretched   = Mathf.CeilToInt(Mathf.Abs(_value));

            if (negative) stretched = -stretched;
            return stretched;
        }
    }
}

