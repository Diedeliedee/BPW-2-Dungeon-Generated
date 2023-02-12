using UnityEditor;
using UnityEngine;

namespace Dodelie.Tools
{
    public static class GizmoTools
    {
        private const float m_epsilon = 0.01f;

        /// <summary>
        /// Draws a line from point A to point B, with a disc at the end of the line
        /// </summary>
        public static void DrawLine(Vector3 position, Vector3 target, Color color, float opacity = 1)
        {
#if UNITY_EDITOR
            if ((target - position).sqrMagnitude < m_epsilon) return;

            Gizmos.color = new Color(color.r, color.g, color.b, opacity);
            Gizmos.DrawLine(position, target);
            DrawSolidDisc(target, 0.25f, color, opacity);
#endif
        }

        /// <summary>
        /// Draws a ray from the position to the offset, with a disc at the end of the ray.
        /// </summary>
        public static void DrawRay(Vector3 position, Vector3 offset, Color color, float opacity = 1)
        {
#if UNITY_EDITOR
            if (offset.sqrMagnitude < m_epsilon) return;

            DrawLine(position, position + offset, color, opacity);
#endif
        }

        /// <summary>
        /// Draw a line with width. Will be rendered as two circles connected to each other.
        /// </summary>
        public static void DrawWideLine(Vector3 position, float radius, Vector3 target, Color color, float opacity = 1)
        {
#if UNITY_EDITOR
            if ((target - position).sqrMagnitude < m_epsilon) return;

            //  Draw a circle at the beginning and end.
            DrawCircle(position, radius, color, opacity);
            DrawCircle(target, radius, color, opacity);

            //  Drawing lines accompanying the circle.
            {
                var rayDirection = (target - position).normalized;

                void DrawAccompanyingLine(float rotation)
                {
                    var offset = Quaternion.Euler(0, rotation, 0) * (rayDirection * radius);

                    Gizmos.color = new Color(color.r, color.g, color.b, opacity);
                    Gizmos.DrawLine(position + offset, target + offset);
                }

                DrawAccompanyingLine(-90);
                DrawAccompanyingLine(90);
            }
#endif
        }

        /// <summary>
        /// Draws a disc at the given position.
        /// </summary>
        public static void DrawSolidDisc(Vector3 position, float radius, Color color, float opacity = 1)
        {
#if UNITY_EDITOR
            if (radius <= 0 || opacity <= 0) return;

            Handles.color = new Color(color.r, color.g, color.b, opacity);
            Handles.DrawSolidDisc(position, Vector3.up, radius);
#endif
        }

        /// <summary>
        /// Draws a flat circle at the given position, with the given radius.
        /// </summary>
        public static void DrawCircle(Vector3 position, float radius, Color color, float opacity = 1)
        {
#if UNITY_EDITOR
            if (radius <= 0 || opacity <= 0) return;

            Handles.color = new Color(color.r, color.g, color.b, opacity);
            Handles.DrawWireDisc(position, Vector3.up, radius);
#endif
        }

        /// <summary>
        /// Draws a sphere at the given position, with the given radius.
        /// </summary>
        public static void DrawSphere(Vector3 position, float radius, Color color, float opacity = 1, bool solid = false, float solidOpacityMultiplier = 0.5f)
        {
#if UNITY_EDITOR
            if (radius <= 0 || opacity <= 0) return;

            Gizmos.color = new Color(color.r, color.g, color.b, opacity);
            Gizmos.DrawWireSphere(position, radius);

            if (!solid) return;

            Gizmos.color = new Color(color.r, color.g, color.b, opacity * solidOpacityMultiplier);
            Gizmos.DrawSphere(position, radius);
#endif
        }

        public static void DrawOutlinedBox(Vector3 position, Vector3 size, Color color, float opacity = 1, bool solid = false, float solidOpacityMultiplier = 0.5f)
        {
#if UNITY_EDITOR
            if (size.sqrMagnitude <= 0 || opacity <= 0) return;

            Gizmos.color = new Color(color.r, color.g, color.b, opacity);
            Gizmos.DrawWireCube(position, size);

            if (!solid) return;

            Gizmos.color = new Color(color.r, color.g, color.b, opacity * solidOpacityMultiplier);
            Gizmos.DrawCube(position, size);
#endif
        }

        /// <summary>
        /// Draws a sphere at the given position, with the given radius.
        /// </summary>
        public static void DrawCapsule(Vector3 start, Vector3 end, float radius, Color color, float opacity = 1)
        {
#if UNITY_EDITOR
            if (radius <= 0 || opacity <= 0) return;

            DrawSphere(start, radius, color, opacity);
            DrawSphere(end, radius, color, opacity);
            DrawLine(start, end, color, opacity);
#endif
        }

        /// <summary>
        /// Draws a solid disc, with an outline.
        /// </summary>
        public static void DrawOutlinedDisc(Vector3 position, float radius, Color innerColor, Color outerColor, float opacity = 1)
        {
#if UNITY_EDITOR
            if (radius <= 0) return;

            DrawSolidDisc(position, radius, innerColor, opacity);
            DrawCircle(position, radius, outerColor, 1);
#endif
        }

        /// <summary>
        /// Draws a label of text at the given position.
        /// </summary>
        /// <param name="label">The text displayed.</param>
        public static void DrawLabel(Vector3 position, string label, Color color, float opacity = 1)
        {
#if UNITY_EDITOR
            if (label.Length <= 0) return;

            Handles.BeginGUI();
            Handles.color = new Color(color.r, color.g, color.b, opacity);
            Handles.Label(position, label);
            Handles.EndGUI();
#endif
        }

        /// <summary>
        /// Draws a path of multiple lines, each with a disc at the end.
        /// </summary>
        public static void DrawPath(Vector3[] wayPoints, bool looped, Color color, float opacity = 1)
        {
#if UNITY_EDITOR
            for (int i = 0; i < wayPoints.Length; i++)
            {
                var firstPosition = wayPoints[0];
                var currentPosition = wayPoints[i];
                var nextPosition = wayPoints[i + 1];

                var lastIteration = i >= wayPoints.Length - 1;

                if (lastIteration && looped)
                {
                    DrawLine(currentPosition, firstPosition, color, opacity);
                    continue;
                }
                DrawLine(currentPosition, nextPosition, color, opacity);
            }
#endif
        }
    }
}
