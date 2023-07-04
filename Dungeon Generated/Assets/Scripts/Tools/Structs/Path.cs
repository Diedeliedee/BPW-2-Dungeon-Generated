using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools
{
    public struct Path
    {
        public readonly Vector3[] positions;
        public readonly float length;

        private readonly Slice[] m_slices;

        public Slice this[int index] { get => m_slices[index]; }

        public Vector3 first  { get => positions[0]; }
        public Vector3 last { get => positions[^1]; }

        public Path(params Vector3[] posBundle)
        {
            m_slices    = new Slice[posBundle.Length - 1];

            positions   = posBundle;
            length      = 0f;

            for (int i = 0; i < posBundle.Length - 1; i++)
            {
                m_slices[i] = new Slice(posBundle[i], posBundle[i + 1], length);
                length += m_slices[i].length;
            }
        }

        public Vector3 Lerp(float t)
        {
            var desiredSlice = m_slices[^1];

            t *= length;
            foreach (var slice in m_slices)
            {
                if (slice.tail + slice.length < t) continue;
                desiredSlice = slice;
                break;
            }

            t -= desiredSlice.tail;     //  Cut off the excess tail length;
            t /= desiredSlice.length;   //  Convert to percentage;

            return desiredSlice.Lerp(t);
        }

        public struct Slice
        {
            public readonly Vector3 position;
            public readonly Vector3 offset;

            public readonly float length;
            public readonly float tail;

            public Slice(Vector3 origin, Vector3 next, float pathTail)
            {
                position    = origin;
                offset      = next - origin;

                length      = offset.magnitude;
                tail        = pathTail;
            }

            public Vector3 Lerp(float t)
            {
                return Vector3.Lerp(position, position + offset, t);
            }
        }
    }
}