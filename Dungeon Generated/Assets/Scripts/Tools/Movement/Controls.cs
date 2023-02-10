using UnityEngine;

namespace Dodelie.Tools
{
    /// <summary>
    /// Abstract class having minimal amount of necessary references to possible input.
    /// </summary>
    public class Controls
    {
        public static Vector2 leftInput { get; protected set; }
        public static Vector2 rightInput { get; protected set; }

        public static bool activeLeftInput { get => leftInput.sqrMagnitude > 0f; }
        public static bool activeRightInput { get => rightInput.sqrMagnitude > 0f; }
    }
}
