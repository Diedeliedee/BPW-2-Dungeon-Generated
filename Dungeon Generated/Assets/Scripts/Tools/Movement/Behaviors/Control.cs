using UnityEngine;

namespace Dodelie.Tools
{
    class Control : Behavior
    {
        private float m_inputRotation = 0f;

        public Control(float inputRotation = 0f)
        {
            m_inputRotation = inputRotation;
        }

        public override Vector2 GetDesiredVelocity(Context context)
        {
            return Calc.RotateVector2(Controls.leftInput, m_inputRotation) * context.speed;
        }
    }
}
